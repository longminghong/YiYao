using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace YiYao
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Profile : UserControl, INotifyPropertyChanged
    {
        private bool mEditingPhone;
        public bool EditingPhone
        {
            get { return mEditingPhone; }
            set
            {
                mEditingPhone = value;
                if(PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(EditingPhone)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Profile()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
             {
                 var card = AppData.CurrentIDCard;
                 nameText.Text = card.Name;
                 sexText.Text = card.Sex;
                 nationalityText.Text = card.Nationality;
                 birthdayText.Text = card.BirthDay;
                 addressText.Text = card.Address;
                 idNumberText.Text = card.IDNumber;
                 headImage.Source = card.HeadImage;
                 phone.Text = card.Phone;
                 this.DataContext = this;
                 keyboard.AddHandler(Button.ClickEvent, new RoutedEventHandler(onKeypadClick));

             };
        }

        private void onKeypadClick(object sender, RoutedEventArgs args)
        {
            var button = args.OriginalSource as Button;
            if (button != null)
            {
                var tag = button.Tag?.ToString() ?? "";
                if (tag == "del")
                {
                    var t = phone.Text;
                    if (t.Length > 0)
                    {
                        phone.Text = t.Substring(0, t.Length - 1);
                    }
                }
                else if (tag == "done")
                {
                    AppData.CurrentIDCard.Phone = phone.Text;
                }
                else
                {
                    phone.Text += button.Content as string;
                }
            }
        }

        private void updatePhoneBtn_Click(object sender, RoutedEventArgs e)
        {
            EditingPhone = true;
        }

        private void savePhoneBtn_Click(object sender, RoutedEventArgs e)
        {
            EditingPhone = false;
        }
    }
}
