using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CardReader;
using WebService;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using YiYao.Events;
using System.Windows.Media.Animation;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace YiYao
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class A3 : UserControl, INavigable
    {
        private IDCardReader cardReader;
        private bool mIsChecking;

        private MTMCustInfo customInfo;
        public A3()
        {
            InitializeComponent();

            WebSocketSingleton websocketInstance = WebSocketSingleton.GetInstance();

            this.Loaded += (s, e) =>
            {
                var loadingAnimation = FindResource("A3Storyboard1") as Storyboard;
                loadingAnimation.Begin();

                cardReader = new IDCardReader();
                cardReader.CardRead += CardReader_CardRead;
                cardReader.Start();
            };
            this.Unloaded += (s, e) =>
            {
                cardReader.CardRead -= CardReader_CardRead;
                cardReader.Dispose();
            };
        }

        private async void CardReader_CardRead(object sender, EventArgs e)
        {
            AppData.CurrentIDCard = cardReader.CurrentCard;

            postDataWithWebClient();
        }


        public void Start(object args)
        {
            if (true)
            {
                postDataWithWebClient();
            }
            if (null != args)
            {
                try
                {
                    customInfo = (MTMCustInfo)args;

                    if (String.Equals("no",customInfo.isfirst)) {
                        textBlock.Text = "修改信息";

                        textBlock4.Visibility = Visibility.Hidden;

                        canvas14.Margin = new Thickness(729, 746.5, 853, 247.5);
                    }
                }
                catch (Exception)
                {

                }

                EventAggregator eventAggragator = ServiceLocator.Current.GetInstance<EventAggregator>();
                eventAggragator.GetEvent<WebSocketEvent>().Subscribe(OnWebSocketEvent);
            }
        }

        public void Stop()
        {
            EventAggregator eventAggragator = ServiceLocator.Current.GetInstance<EventAggregator>();
            eventAggragator.GetEvent<WebSocketEvent>().Unsubscribe(OnWebSocketEvent);
        }

        private void OnWebSocketEvent(object data)
        {
            Console.WriteLine("data ======== OK ");
            
            if (null != data)
            {
                customInfo = data as MTMCustInfo;

                xinxi2_png.Text = "姓名: " + customInfo.name;

                textBlock6.Text = "性别: " + customInfo.gender;

                nationalityText.Text = "生日：" + customInfo.birthday;

                if ("phone" == customInfo.pattern)
                    xinxi4_png.Text = "联系方式（手机）：" + customInfo.phone;
                else
                    xinxi4_png.Text = "联系方式（座机）：" + customInfo.phonezone + customInfo.phonenumber + customInfo.extension;
                xinxi5_png.Text = "身份证 ：" + customInfo.ssn;
                xinxi6_png.Text = "邮件 ：" + customInfo.email;
                textBlock3.Text = "会员卡号 ：" + customInfo.cardno;
                 
                textBlock4.Text = "开卡时间 ：" + customInfo.carddate;

                textBlock2.Text = "地址 ：" + customInfo.province + customInfo.city + customInfo.district + customInfo.detailaddress;

            }
        }

        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(A4));
        }

        private void postData()
        {

            string requestUrl = "http://test.o4bs.com/api/members/identitycard";
            //WebRequest request = WebRequest.Create("http://test.o4bs.com/api/members/identitycard");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);

            request.Method = "POST";
            request.ContentType = "application/json";
            //request.Headers.Add("Content-Type", "application/json");
            request.Headers.Add("appkey", "097e8751c3c183edf602f867a5326559");
            request.Headers.Add("appid", "SyccthZn");
            request.Timeout = 20000;

            //IDCard c = cardReader.CurrentCard;
            IDCard c = new IDCard();

            c.Name = "MTM22";
            c.Sex = "男";
            c.IDNumber = "450981000000000022";
            c.Address = "南京仙鹤门";
            c.BirthDay = "2000-01-01";
            MCard card = new MCard(c);

            JObject carJson = JObject.FromObject(card);

            Console.WriteLine(carJson.ToString());

            string postData = carJson.ToString();

            byte[] btBodys = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = btBodys.Length;
            request.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)request.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();
            request.Abort();
            httpWebResponse.Close();
        }

        private void postDataWithWebClient()
        {

            try
            {
                WebClient wc = new WebClient();
                string requestUrl = "http://test.o4bs.com/api/members/identitycard";
                // 采取POST方式必须加的Header

                IDCard c = new IDCard();

                //c.Name = "MTM22";
                //c.Sex = "男";
                //c.IDNumber = "450981000000000022";
                //c.Address = "北京市中关村大街道200";
                //c.BirthDay = "20000101";

                //MessageBox.Show("get id "+ AppData.CurrentIDCard.Name+ AppData.CurrentIDCard.IDNumber+ AppData.CurrentIDCard.Sex+ AppData.CurrentIDCard.BirthDay+ AppData.CurrentIDCard.Address);

                MCard card = new MCard(AppData.CurrentIDCard);

                //MCard card = new MCard(c);
                JObject carJson = JObject.FromObject(card);
                string paramStr = carJson.ToString();

                byte[] postData = Encoding.UTF8.GetBytes(paramStr);

                wc.Headers.Add("Content-Type", "application/json");
                wc.Headers.Add("appkey", "097e8751c3c183edf602f867a5326559");
                wc.Headers.Add("appid", "SyccthZn");

                byte[] responseData = wc.UploadData(requestUrl, "POST", postData); // 得到返回字符流
                String resultValue = Encoding.UTF8.GetString(responseData);// 解码                
                Console.WriteLine(resultValue);
            }
            catch (Exception e)
            {
            }
            finally {
                mIsChecking = false;
            }
            
        }

        public class MCard
        {
            public string sid { get; set; }
            public string idnumber { get; set; }
            public string name { get; set; }
            public string gender { get; set; }
            public string dateofbirth { get; set; }
            public string other { get; set; }
            public Address address { get; set; }
            public MCard(IDCard iCard)
            {
                this.sid = "Vk7Tc-PJx";

                this.idnumber = iCard.IDNumber;

                this.name = iCard.Name;

                this.gender = iCard.Sex;

                string birth = "";
                birth += iCard.BirthDay.Substring(0,4); 
                birth += "-";

                birth += iCard.BirthDay.Substring(4, 2);
                birth += "-";

                birth += iCard.BirthDay.Substring(6, 2);
                
                this.dateofbirth = birth;

                this.other = iCard.Address;

                this.address = new Address
                {
                    province = "",
                    city = "",
                    district = "",
                    other = ""
                };
            }



            public class Address
            {
                public string province { get; set; }
                public string city { get; set; }
                public string district { get; set; }
                public string other { get; set; }
            }


        }

    }
}


