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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YiYao
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class LoginSuccess : UserControl, INavigable
    {
        public LoginSuccess()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
             {
                var loadingAnimation = FindResource("Storyboard1") as Storyboard;
                 loadingAnimation.Completed += (ss, ee) =>
                  {
                      if (null != Parent)
                      {
                          (Parent as NavigationManager).GoToPage(typeof(Dashboard));
                      }
                      
                  };
                 loadingAnimation.Begin();
             };
        }

        public void Start(object args)
        {
            var card = AppData.CurrentIDCard;
            nameText.Text = card.Name;
            sexText.Text = card.Sex;
            nationalityText.Text = card.Nationality;
            birthdayText.Text = card.BirthDay;
            addressText.Text = card.Address;
            idNumberText.Text = card.IDNumber;

            headImage.Source = AppData.GetAvatar();

            //if (null == card.HeadImage)
            //{

            //    string userDefaultHeadImage = card.Sex == "男" ? "/man.png" : "/woman.png";
            //    Uri riskImageUri = null;
            //    String riskImagePath = Environment.CurrentDirectory + userDefaultHeadImage;
            //    //String riskImagePath =  userDefaultHeadImage;
            //    bool imageExist;
            //    imageExist = System.IO.File.Exists(riskImagePath);
            //    riskImageUri = new Uri(riskImagePath, UriKind.Absolute);
            //    BitmapImage bi3 = new BitmapImage();
            //    bi3.BeginInit();
            //    bi3.UriSource = riskImageUri;
            //    bi3.EndInit();
            //    card.HeadImage = bi3;
            //    headImage.Source = bi3;
            //}
            //else
            //    headImage.Source = card.HeadImage;
        }

        public void Stop()
        {
            
        }
    }
}
