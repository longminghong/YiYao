using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

            postDataWithWebClient();
        }

        private void postDataWithWebClient()
        {

            try
            {
                WebClient wc = new WebClient();
                string requestUrl = "http://test.o4bs.com/api/members/";
                // 采取POST方式必须加的Header
                var healthDataService = ServiceLocator.Current.GetInstance<HealthDataService>();
                var mid = healthDataService.GetMemberMid();
                var cardNo = healthDataService.GetMemberCardNo();
                requestUrl += mid;


                Dictionary<string,string> modifyDictionary = new Dictionary<string, string>();
                modifyDictionary.Add("card_no", cardNo);
                modifyDictionary.Add("phone", phone.Text);

             
                JObject carJson = JObject.FromObject(modifyDictionary);
                string paramStr = carJson.ToString();

                byte[] postData = Encoding.UTF8.GetBytes(paramStr);

                wc.Headers.Add("Content-Type", "application/json");
                wc.Headers.Add("appkey", "097e8751c3c183edf602f867a5326559");
                wc.Headers.Add("appid", "SyccthZn");

                byte[] responseData = wc.UploadData(requestUrl, "PUT", postData); 
                String resultValue = Encoding.UTF8.GetString(responseData);
                Debug.WriteLine(resultValue);
                JObject jObject = JObject.Parse(resultValue);

                JToken jErrorToken = jObject.GetValue("error");
                JToken jDataToken = jObject.GetValue("data");

                String errorCode = jErrorToken.First().First().ToString();

                if (string.Equals(errorCode,"0"))
                {
                    // 保存成功
                    healthDataService.SavePhoneNumber(phone.Text);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                
            }

        }
    }
}
