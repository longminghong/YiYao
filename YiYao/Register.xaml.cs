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
    public partial class Register : UserControl, INavigable
    {
        public Register()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
             {
                 keyboard.AddHandler(Button.ClickEvent, new RoutedEventHandler(NumberClick));
                 FindStoryboard();
                 keyboardInput.Focus();
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

        private void FindStoryboard()
        {
            Storyboard s1 = FindResource("Storyboard1") as Storyboard;
            Storyboard s2 = FindResource("Storyboard2") as Storyboard;
            Storyboard s3 = FindResource("Storyboard3") as Storyboard;

            s1.Duration = TimeSpan.FromSeconds(8);

            s2.Completed += (s, e) =>
            {
                s3.Begin();
            };
            s3.Completed += (s, e) =>
            {
                s3.Begin();
            };

            s1.Begin();
            s2.Begin();

        }

        private void NumberClick(object sender, RoutedEventArgs args)
        {
            var button = args.OriginalSource as Button;
            if (button != null)
            {
                var tag = button.Tag?.ToString() ?? "";
                if (tag == "del")
                {
                    var t = keyboardInput.Text;
                    if (t.Length > 0)
                    {
                        keyboardInput.Text = t.Substring(0, t.Length - 1);
                    }
                }
                else if (tag == "done")
                {

                }
                else
                {
                    keyboardInput.Text += button.Content as string;
                }
            }
        }

        private void zhuce_png_Click(object sender, RoutedEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(RegisterSuccess));
        }
    }
}
