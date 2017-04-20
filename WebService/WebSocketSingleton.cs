using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace WebService
{
    class WebSocketSingleton
    {
        private static WebSocketSingleton instance;
        private static object _lock = new object();

        private WebSocketSingleton()
        {

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
        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);
            byte[] messageContent = e.Message;

            string str = System.Text.Encoding.Default.GetString(messageContent);
            Console.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);
        }
        // 发布消息后的操作
        static void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Console.WriteLine("MessageId = " + e.MessageId + " Published = " + e.IsPublished);
        }
        // 关闭连接后的操作
        static void client_ConnectionClosed(object sender, EventArgs e)
        {
            Console.WriteLine("connect closed");
        }
        // 取消sub后的操作
        static void client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            Console.WriteLine("connect closed");
        }

        public void start() {

            string enpoint = "mqf-bym08ztgwf.mqtt.aliyuncs.com";
            int port = 80;
            string user = "LTAIyNRr5QPLury7";
            string pwd = "fxKK7bAPc7J15qwQ/YoNocsGBso=";
            string clientid = "GID_MTM_WEB_TEST_MQTT@@@NEWDAY";//Guid.NewGuid().ToString(); // 获取一个独一无二的id
            string[] topic = new string[] { "MTM_WEB_TEST/pypub" };

            byte[] qosLevels = new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE }; // qos=1

            MqttClient client = new MqttClient(enpoint);
            //client.ProtocolVersion = MqttProtocolVersion.Version_3_1;
            byte code = client.Connect(clientid,
                                        user,
                                        pwd,
                                        true, // cleanSession
                                        60); // keepAlivePeriod
            Console.WriteLine(code);
            Console.WriteLine(client.IsConnected);
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            client.MqttMsgSubscribed += client_MqttMsgSubscribed;
            client.MqttMsgUnsubscribed += client_MqttMsgUnsubscribed;
            client.MqttMsgPublished += client_MqttMsgPublished;
            client.ConnectionClosed += client_ConnectionClosed;
            client.Subscribe(topic, qosLevels); // sub 的qos=1
        }

    }

}
