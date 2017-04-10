using Microsoft.Practices.ServiceLocation;
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
using WebService;

namespace YiYao
{
    /// <summary>
    /// Interaction logic for MBHealthIndex.xaml
    /// </summary>
    public partial class XYHealthIndex : HealthIndexBase
    {
        public XYHealthIndex()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
             {
                 FindStoryboard();
             };
        }

        private void FindStoryboard()
        {
            Storyboard s1 = FindResource("sb") as Storyboard;
            Storyboard xianstoryboard = FindResource("xianstoryboard") as Storyboard;
            s1.BeginTime = TimeSpan.FromSeconds(1);
            dialogFrame.Show();
            s1.Begin();
            xianstoryboard.Begin();
        }

        public async void LoadData()
        {
            var healthDataService = ServiceLocator.Current.GetInstance<HealthDataService>();
            var data = await healthDataService.GetHealthDataAsync();
            if(data != null)
            {
                var diastolicpressure = data.data.historyhealthrecord.FirstOrDefault(h => h.type == "diastolicpressure");
                var systolicpressure = data.data.historyhealthrecord.FirstOrDefault(h => h.type == "systolicpressure");
                baobiao_png.Intialize(diastolicpressure.value, systolicpressure.value, 20);
                if (systolicpressure.value.Length == 0)
                {
                    shuju1_png.Visibility = Visibility.Collapsed;
                    shuju2_png.Visibility = Visibility.Collapsed;
                    shuju3_png.Visibility = Visibility.Collapsed;
                    return;
                }
                shuju1_png.Text = "血压:   " + systolicpressure.value.Last()[1] + "/" + diastolicpressure.value.Last()[1];
                shuju2_png.Text = "测量时间:   " + DateTime.Now.ToString("yyyy.MM.dd");
            }
        }
          
    }
}
