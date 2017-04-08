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
    /// Interaction logic for MBHealthIndex.xaml
    /// </summary>
    public partial class ShangCiHealthIndex : HealthIndexBase
    {
        Storyboard s1;
        public ShangCiHealthIndex()
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
            var data = await App.Instance.HealthDataCenter.GetHealthDataAsync();
            if(data != null)
            {
                var lastCheckInfo = data.data.lastcheckinfo;
                bloodpressure.Text = $"收缩压{lastCheckInfo.systolicpressure}mmHg 舒张压{lastCheckInfo.diastolicpressure}mmHg";
                bloodSuger.Text = $"{lastCheckInfo.randomBloodSugar}mmol/L";
                cholesterol.Text = $"{lastCheckInfo.cholesterol}mmol/L";
                heartRate.Text = $"{lastCheckInfo.heartrate}次/m";
            }
        }


    }
}
