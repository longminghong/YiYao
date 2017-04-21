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


namespace YiYao
{
    /// <summary>
    /// A3.xaml 的交互逻辑
    /// </summary>
    public partial class A3 : UserControl, INavigable
    {
        private IDCardReader cardReader;
        private bool mIsChecking;
        public A3()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                cardReader = new IDCardReader();
                cardReader.CardRead += CardReader_CardRead;
                cardReader.Start();
            };
            this.Unloaded += (s, e) =>
            {
                cardReader.CardRead -= CardReader_CardRead;
            };

           // (Parent as NavigationManager).SimulateImageClick(jiantou1_png, ImageOnClick);
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
            /*
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
            */
            mIsChecking = false;
        }

        public void Start(object args)
        {

        }

        public void Stop()
        {

        }

      

        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(A4));
        }
    }
}
