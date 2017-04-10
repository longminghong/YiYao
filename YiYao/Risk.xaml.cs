using MTM.SDK.Contract;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using Newtonsoft.Json.Linq;
using Microsoft.Practices.ServiceLocation;
using WebService;

namespace YiYao
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Risk : UserControl
    {
        Storyboard s1;
        Storyboard s2;
        InquiredForm form;
        public Risk()
        {
            InitializeComponent();
            scrollviewer.ManipulationBoundaryFeedback += (s, e) =>
             {
                 e.Handled = true;
             };
            this.Loaded += (s, e) =>
             {
                 form = new InquiredForm
                 {
                     weight = "",
                     height = "",
                     agodiastolicpressure = "",
                     agosystolicpressure = "",
                     smoking = "",
                     drinking = "",
                     cholesterol = "",
                     hdl = "",
                     ldl = "",
                     heartrate = "",
                     waist = "",
                     firstacquisition = "no",
                 };
                 this.DataContext = form;
                 FindStoryboard();
                 LoadInfo();
             };
        }

        private void LoadInfo()
        {
            var card = AppData.CurrentIDCard;
            nameText.Text = card.Name;
            sexText.Text = card.Sex;
            card.BirthDay = card.BirthDay.Replace("/", "-");
            ageText.Text = (DateTime.Today.Year - int.Parse(card.BirthDay.Substring(0, 4))).ToString();
            timeText.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }


        private void FindStoryboard()
        {
            s1 = FindResource("Storyboard1") as Storyboard;
            s2 = FindResource("Storyboard2") as Storyboard;

            s1.Duration = TimeSpan.FromSeconds(8);


            s1.Begin();

        }

        private async void baocun_png_Click(object sender, RoutedEventArgs e)
        {
            
            progressBar.Visibility = Visibility.Visible;
            baocun_png.IsEnabled = false;
            var healthDataService = ServiceLocator.Current.GetInstance<HealthDataService>();

            var healthData =  await healthDataService.InquiredHealthDataAsync(form);
            progressBar.Visibility = Visibility.Collapsed;
            baocun_png.IsEnabled = true;

            if (healthData != null)
            {
                var systolicpressure = healthData.data.historyhealthrecord.First(r => r.type == "systolicpressure");
                var diastolicpressure = healthData.data.historyhealthrecord.First(r => r.type == "diastolicpressure");
                chart.Intialize(systolicpressure.value, diastolicpressure.value, 20);
                var target = healthData.data.calculation.target;
                timeText.Text = DateTime.Now.ToString("yyyy.MM.dd");
                pressureText.Text = target.systolicpressure + "/" + target.diastolicpressure;
                testResult.Text = healthData.data.calculation.risklevel >= 2 ? "偏高" : "正常";
                var riskLevel = healthData.data.calculation.risklevel;
                riskLevel = Math.Min(riskLevel, 3);
                riskLevel = Math.Max(1, riskLevel);
                healthPointer.Margin = new Thickness(0, -90 * riskLevel + 45, 0, 0);

                StringBuilder sb = new StringBuilder();
                foreach (var tip in healthData.data.healthtip)
                {
                    sb.Append(tip.Value);
                }
                healthTipText.Text = sb.ToString();
                s2.Begin();
            }
        }


        private void zi23_png_Click(object sender, RoutedEventArgs e)
        {
            tip.Visibility = tip.Visibility != Visibility.Visible ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
