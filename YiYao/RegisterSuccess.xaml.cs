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
    public partial class RegisterSuccess : UserControl, INavigable
    {
        public RegisterSuccess()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
             {
                 try
                 {
                     var loadingAnimation = FindResource("Storyboard1") as Storyboard;
                     loadingAnimation.Completed += (ss, ee) =>
                     {
                         (Parent as NavigationManager).GoToPage(typeof(Dashboard));
                     };
                     loadingAnimation.Begin();
                 }
                 catch (Exception)
                 {
        
                 }
                
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
            headImage.Source = card.HeadImage;
        }

        public void Stop()
        {
            
        }
    }
}
