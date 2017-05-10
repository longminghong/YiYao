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
                cardReader = new IDCardReader();
                cardReader.CardRead += CardReader_CardRead;
                cardReader.Start();

                var loadingAnimation = FindResource("A3Storyboard1") as Storyboard;
                loadingAnimation.Begin();
            };
            this.Unloaded += (s, e) =>
            {
                cardReader.CardRead -= CardReader_CardRead;
            };
        }

        private async void CardReader_CardRead(object sender, EventArgs e)
        {   
            AppData.CurrentIDCard = cardReader.CurrentCard;
            /*
             * 这里调用rest接口，发送数据给服务器*
             */
            PostUserSSN();
        }

        private async void PostUserSSN()
        {
            if (mIsChecking)
                return;
            mIsChecking = true;
            
            var healthDataService = ServiceLocator.Current.GetInstance<HealthDataService>();

            var member = await healthDataService.GetMemberInfoBySsnAsync(AppData.CurrentIDCard.IDNumber);

            if (member != null)
            {
                if (member.error.code == 0)
                {
                    AppData.CurrentIDCard.Name = member.data.name;
                    var address = member.data.address;
                    AppData.CurrentIDCard.BirthDay = member.data.birthday;
                    AppData.CurrentIDCard.Sex = member.data.gender;
                    (Parent as NavigationManager).GoToPage(typeof(LoginSuccess));
                }
                else
                {
                    (Parent as NavigationManager).GoToPage(typeof(Register));
                }
            }
            
            mIsChecking = false;
        }

        public void Start(object args)
        {
            if (null != args)
            {
                try
                {
                    customInfo = (MTMCustInfo)args;
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

                //if (String.Equals("close", customInfo.operatetype))
                //{

                //    (Parent as NavigationManager).GoToPage(typeof(Dashboard));
                //}

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



                //    xinxi2_png.Text = "姓名: " + customInfo.name + "               性别：" + customInfo.gender;

                //    //input_textblock_userinfo_genderr.Text = "性别: " + customInfo.gender;

                //    xinxi3_png.Text = "生日：" + customInfo.birthday;

                //    if ("phone" == customInfo.pattern)
                //        xinxi4_png.Text = "联系方式（手机）：" + customInfo.phone;
                //    else
                //        xinxi4_png.Text = "联系方式（座机）：" + customInfo.phonezone + customInfo.phonenumber + customInfo.extension;
                //    xinxi5_png.Text = "身份证 ：" + customInfo.ssn;
                //    xinxi6_png.Text = "邮件 ：" + customInfo.email;

                //    textBlock3.Text = "会员卡号 ：" + customInfo.cardno;
                //    textBlock4.Text = "开卡时间 ：" + customInfo.carddate;
                //    textBlock2.Text = "地址 ：" + customInfo.province + customInfo.city + customInfo.district + customInfo.detailaddress;
            }
        }

        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(A4));
        }
    }
}
