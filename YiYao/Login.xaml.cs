using CardReader;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
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
using WebService;

namespace YiYao
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Login : UserControl, INavigable
    {
        private IDCardReader cardReader;
        private bool mIsChecking;
        public Login()
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
        
            
        }



        private async void CardReader_CardRead(object sender, EventArgs e)
        {
            AppData.CurrentIDCard = cardReader.CurrentCard;
            CheckMemberAsync();
        }


        private async void CheckMemberAsync()
        {
            if (mIsChecking)
                return;
            mIsChecking = true;
            var healthDataService = ServiceLocator.Current.GetInstance<HealthDataService>();

            var member = await healthDataService.GetMemberInfoBySsnAsync(AppData.CurrentIDCard.IDNumber);
            if(member != null)
            {
                AppData.CurrentIDCard.Name = member.data.name;
                var address = member.data.address;
              //  AppData.CurrentIDCard.Address = address.province + address.city + address.district;
                AppData.CurrentIDCard.BirthDay = member.data.birthday;
                AppData.CurrentIDCard.Sex = member.data.gender;
                (Parent as NavigationManager).GoToPage(typeof(LoginSuccess));
            }
            else
            {
                (Parent as NavigationManager).GoToPage(typeof(Register));
            }
            mIsChecking = false;
        }


        public void Start(object args)
        {

        }

        public void Stop()
        {
           
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            CheckMemberAsync();
        }

    }
}
