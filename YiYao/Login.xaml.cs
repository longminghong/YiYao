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
                 cardReader.Dispose();
             };
        }



        private async void CardReader_CardRead(object sender, EventArgs e)
        {
            AppData.CurrentIDCard = cardReader.CurrentCard;
            CheckMemberAsync();
        }

        private List<IDCard> cards = new List<IDCard>() {

            new IDCard(){
                Name = "大力神ABC",
                Sex = "男",
                IDNumber = "450981000000000022",
                Address = "北京市中关村大街道200",
                BirthDay = "20000101"
            },
            new IDCard(){
                Name = "优质男青年",
                Sex = "男",
                IDNumber = "4509812000010100018",
                Address = "北京市中关村大街道21",
                BirthDay = "20000108"
            },
            new IDCard(){
                Name = "雨神",
                Sex = "男",
                IDNumber = "4509812000010100017",
                Address = "北京市中关村大街道20",
                BirthDay = "20000109"
            },
            new IDCard(){
                Name = "天神",
                Sex = "男",
                IDNumber = "4509812000010100016",
                Address = "北京市中关村大街道19",
                BirthDay = "20000109"
            },
            new IDCard(){
                Name = "达利园",
                Sex = "男",
                IDNumber = "4509812000010100015",
                Address = "北京市中关村大街道18",
                BirthDay = "20000109"
            }
        };
        private async void CheckMemberAsync()
        {
            if (mIsChecking)
                return;
            mIsChecking = true;
            try
            {

                IDCard c = new IDCard();
                c.Name = "大力神ABC";
                c.Sex = "男";
                //c.IDNumber = "450981198701052135";
                c.IDNumber = "4509812000010100013";
                c.Address = "北京市中关村大街道200";
                c.BirthDay = "20000101";

                //Random random = new Random();
                //int randomValue = random.Next(0, (cards.Count()-1));
                //IDCard c = cards[randomValue];
                AppData.CurrentIDCard = c;

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
                        AppData.CurrentIDCard.Phone = member.data.phone;
                        (Parent as NavigationManager).GoToPage(typeof(LoginSuccess));
                    }
                    else
                    {
                        // to do 弹出提示：请联系药师处理

                        //(Parent as NavigationManager).GoToPage(typeof(Register));
                        
                    }
                }
            }
            catch (Exception)
            {


            }
            finally {
                mIsChecking = false;
            }
        }

        private void hideImages()
        {
            shuaka_png.Visibility = Visibility.Hidden;
            shukaye_png.Visibility = Visibility.Hidden;
            zi_png.Visibility = Visibility.Hidden;
            tiao_png.Visibility = Visibility.Hidden;
        }
        private void showImages()
        {
            shuaka_png.Visibility = Visibility.Visible;
            shukaye_png.Visibility = Visibility.Visible;
            zi_png.Visibility = Visibility.Visible;
            tiao_png.Visibility = Visibility.Visible;
        }

        private void showContectMedMessage() {

            userNotFound.Visibility = Visibility.Visible;
        }
        private void dismissContectMedMessage()
        {
            userNotFound.Visibility = Visibility.Collapsed ;
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
            //WebSocketSingleton websocketInstance = WebSocketSingleton.GetInstance();
            //MTMIssueCollectDTO reciveDTO;
            //reciveDTO = websocketInstance.callForA8();

            //(Parent as NavigationManager).GoToPageWithArgs(typeof(A8), reciveDTO);
            //CheckMemberAsync();
        }

        private void PC_Register_Click(object sender, RoutedEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(A3));
        }
    }
}
