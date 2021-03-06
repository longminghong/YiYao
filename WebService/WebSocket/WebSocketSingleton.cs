﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

using System.IO;

public enum MEMBERType
{
    MEMBBASIC = 1,//新建会员时，采集基本信息
    MEMBHEALTH = 2,//新建会员时，采集健康信息
    MEMBDRUG = 3,//新建会员时，采集用药信息
    MEMBDISEASE = 4,//新建会员时，采集疾病风险信息
    MEMBQR = 5,//供会员关注和绑定的二维码
    MEMBRISK = 6,//会员评估结果数据
    MEMBRECOMM = 7,//推荐用药数据
    MEMBCART = 8,//药品购物车
    MEMBPLAN = 9,//会员用药计划
    MEMBCLOSE = 10,
    MEMBEDIT = 11,
    MEMBEDITDISEASE = 12,
    MEMERROR = 999,
}

namespace WebService
{

    public class WebSocketSingleton
    {
        public delegate void SocketPageCommandHandleEvent(MEMBERType sender,object obj);
        public delegate void SocketConnectCloseHandleEvent();

        public SocketPageCommandHandleEvent pageCommandHandle;
        public SocketConnectCloseHandleEvent connectCloseHandle;

        private static WebSocketSingleton instance;
        private static object _lock = new object();

        private WebSocketSingleton()
        {

        }

        public void SetConnectCloseHandleCallback(SocketConnectCloseHandleEvent callback) {

            connectCloseHandle = callback;
        }

        public void SetCallback(SocketPageCommandHandleEvent callback)
        {
            pageCommandHandle = callback;
        }

        public static WebSocketSingleton GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new WebSocketSingleton();
                    }
                }
            }
            return instance;
        }
        private static CamelCasePropertyNamesContractResolver s_defaultResolver = new CamelCasePropertyNamesContractResolver();
        
        private static JsonSerializerSettings s_settings = new JsonSerializerSettings()
        {   
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = s_defaultResolver,
        
        };

        /**
         * 
         * Mqtt call back
         * 
         * **/
        static void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Console.WriteLine("Subscribed for id = " + e.MessageId);
        }
        // 接受消息后的操作
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string responseContent = null;

  
            byte[] messageContent = e.Message;
            
            responseContent = Encoding.UTF8.GetString(messageContent);
            Console.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);
    
            try
            {
                if (!string.IsNullOrWhiteSpace(responseContent))
                {
                    
                    var responseContentAfterReplaceString = responseContent.Replace("null", "\"\"");
                    JObject jObject = JObject.Parse(responseContentAfterReplaceString);

                    JToken jTypeToken = jObject.GetValue("type");
                    JToken jDataToken = jObject.GetValue("data");

                    JObject jDataObject = JObject.Parse(jDataToken.ToString());

                    JToken jOperateToken = jDataObject.GetValue("operatetype");

                    if (String.Equals("open", jOperateToken.ToString()))
                    {
                        Console.WriteLine("json finish    " + jTypeToken.ToString());

                        MEMBERType pageType;

                        pageType = deserializeDataType(jTypeToken.ToString());

                        Console.WriteLine(jDataToken.ToString());

                        object obj;
                        string stringRemoveNullValue = jDataToken.ToString().Replace("null", "");

                        obj = invokeDataReciveCallBack(pageType, stringRemoveNullValue);
                        if (null == obj)
                        {

                        }
                        else {
                            pageCommandHandle?.Invoke(pageType, obj);
                        }
                        
                    }
                    else {
                        // 关闭操作
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("webSocket:" + ex.Message);
            }
        }
        // 发布消息后的操作
        void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Console.WriteLine("MessageId = " + e.MessageId + " Published = " + e.IsPublished);
        }
        // 关闭连接后的操作
        void client_ConnectionClosed(object sender, EventArgs e)
        {
            Console.WriteLine("client_ConnectionClosed"+ e);
            connectCloseHandle?.Invoke();
        }
        // 取消sub后的操作
        void client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            Console.WriteLine("client_MqttMsgUnsubscribed" + e);
        }
        const String radisTopic = "MTM_WEB_TEST/Vk7Tc-PJx";
        const String oldTopic = "MTM_WEB_TEST/pypub";
        public void start()
        {

            Console.WriteLine("web socket run.");

            string enpoint = "mqf-bym08ztgwf.mqtt.aliyuncs.com";
            int port = 80;
            string user = "LTAIyNRr5QPLury7";
            string pwd = "fxKK7bAPc7J15qwQ/YoNocsGBso=";
            string clientid = "GID_MTM_WEB_TEST_MQTT@@@NEWDAY";//Guid.NewGuid().ToString(); // 获取一个独一无二的id
            string[] topic = new string[] { radisTopic };

            byte[] qosLevels = new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE }; // qos=1

            MqttClient client = new MqttClient(enpoint);

            //byte code = client.Connect(clientid,
            //                            user,
            //                            pwd,
            //                            true, // cleanSession
            //                            240); // keepAlivePeriod

            byte code = client.Connect(
                clientid, user, pwd);

            Console.WriteLine(code);
            Console.WriteLine(client.IsConnected);
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            client.MqttMsgSubscribed += client_MqttMsgSubscribed;
            client.MqttMsgUnsubscribed += client_MqttMsgUnsubscribed;
            client.MqttMsgPublished += client_MqttMsgPublished;
            client.ConnectionClosed += client_ConnectionClosed;
            client.Subscribe(topic, qosLevels); // sub 的qos=1
        }

        static MEMBERType deserializeDataType(String responseType) {

            MEMBERType memberType;
            memberType = MEMBERType.MEMERROR;
            if (!string.IsNullOrWhiteSpace(responseType))
            {
                if (responseType.Equals("MEMBER-BASIC"))
                {
                    memberType = MEMBERType.MEMBBASIC;
                }
                else if (responseType.Equals("MEMBER-HEALTH"))
                {
                    memberType = MEMBERType.MEMBHEALTH;
                }
                else if (responseType.Equals("MEMBER-DRUG"))
                {
                    memberType = MEMBERType.MEMBDRUG;
                }
                else if (responseType.Equals("MEMBER-DISEASE"))
                {
                    memberType = MEMBERType.MEMBDISEASE;
                }
                else if (responseType.Equals("MEMBER-QR"))
                {
                    memberType = MEMBERType.MEMBQR;
                }
                else if (responseType.Equals("MEMBER-RISK"))
                {
                    memberType = MEMBERType.MEMBRISK;
                }
                else if (responseType.Equals("MEMBER-RECOMM"))
                {
                    memberType = MEMBERType.MEMBRECOMM;
                }
                else if (responseType.Equals("MEMBER-CART"))
                {
                    memberType = MEMBERType.MEMBCART;
                }
                else if (responseType.Equals("MEMBER-PLAN"))
                {
                    memberType = MEMBERType.MEMBPLAN;
                }else if (responseType.Equals("MEMBER-EDITBASIC"))
                {
                    memberType = MEMBERType.MEMBEDIT;
                }
                else if (responseType.Equals("MEMBER-EDITDISEASE"))
                {
                    memberType = MEMBERType.MEMBEDITDISEASE;
                }
                
            }
            return memberType;
        }

        static object invokeDataReciveCallBack(MEMBERType pageType,String jsonContent) {

            object obj = null;
            switch (pageType)
            {
                case MEMBERType.MEMBBASIC:
                    {// 新建会员采集基本信息
                        //pageType = typeof(A3);
                        MTMCustInfo info = JsonConvert.DeserializeObject<MTMCustInfo>(jsonContent, s_settings);
                        obj = info;
                    }
                    break;
                case MEMBERType.MEMBHEALTH:
                    {//新建会员时，采集健康信息
                        //pageType = typeof(A5);
                        MTMHealthCollectDTO info = JsonConvert.DeserializeObject<MTMHealthCollectDTO>(jsonContent, s_settings);
                        obj = info;
                    }
                    break;
                case MEMBERType.MEMBDRUG:
                    {//新建会员时，采集用药信息
                         
                        MTMMedCollectDTO info = JsonConvert.DeserializeObject<MTMMedCollectDTO>(jsonContent, s_settings);
                        obj = info;
                    }
                    break;
                case MEMBERType.MEMBDISEASE:
                    {//新建会员时，采集疾病风险信息
                        //pageType = typeof(A7);
                        MTMDisDTO info = JsonConvert.DeserializeObject<MTMDisDTO>(jsonContent, s_settings);
                        obj = info;
                    }
                    break;
                case MEMBERType.MEMBQR:
                    {//供会员关注和绑定的二维码
                        //pageType = typeof(A4);
                        MTMQRDTO info = JsonConvert.DeserializeObject<MTMQRDTO>(jsonContent, s_settings);
                        obj = info;
                    }
                    break;
                case MEMBERType.MEMBRISK:
                    {//会员评估结果数据
                        //pageType = typeof(A8);
                        MTMIssueCollectDTO info = JsonConvert.DeserializeObject<MTMIssueCollectDTO>(jsonContent, s_settings);
                        if (null != info.measuredata)
                        {
                            obj = info;
                        }else if (0 == info.risklevel)
                        {
                            obj = null;
                        }
                    }
                    break;
                case MEMBERType.MEMBRECOMM:
                    {//推荐用药数据
                        //pageType = typeof(RecomMed);
                        MTMRecMedDTO info = JsonConvert.DeserializeObject<MTMRecMedDTO>(jsonContent, s_settings);
                        obj = info;
                    }
                    break;
                case MEMBERType.MEMBCART:
                    {//药品购物车
                        //pageType = typeof(ShoppingCar);
                        MTMShopCarDTO info = JsonConvert.DeserializeObject<MTMShopCarDTO>(jsonContent, s_settings);
                        obj = info;
                    }
                    break;
                case MEMBERType.MEMBPLAN:
                    {//会员用药计划
                     //pageType = typeof(A3);

                        MTMMedPlanDTO info = JsonConvert.DeserializeObject<MTMMedPlanDTO>(jsonContent, s_settings);
                        obj = info;
                    }
                    break;
                case MEMBERType.MEMBEDIT:
                    {//会员用药计划

                        MTMCustInfo info = JsonConvert.DeserializeObject<MTMCustInfo>(jsonContent, s_settings);
                        obj = info;
                    }
                    break;
                case MEMBERType.MEMBEDITDISEASE:
                    {//老会员 疾病信息随访

                        MTMDisDTO info = JsonConvert.DeserializeObject<MTMDisDTO>(jsonContent, s_settings);
                        obj = info;
                    }
                    break;
                    
                default:
                    //
                    break;
            }
            return obj;
        }

        public MTMIssueCollectDTO callForA8() {
            
            MTMIssueCollectDTO info = null;
            try
            {
                string str = System.Environment.CurrentDirectory + "\\Json.txt";
                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(str))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    
                    info = JsonConvert.DeserializeObject<MTMIssueCollectDTO>(line, s_settings);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return info;
        }
    }
}
