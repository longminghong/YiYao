using MTM.SDK.Contract;
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
    public partial class LiCiHealthIndex : HealthIndexBase
    {
        Storyboard s1;
        public LiCiHealthIndex()
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
                var historyDrugrecord = data.data.historydrugrecord;
                int i = 0;
                text1.Text = "";
                text2.Text = "";
                foreach (var item in historyDrugrecord)
                {
                    if (i == 0)
                    {
                        text1.Text = FormatText(item);
                    }
                    else if (i == 1)
                    {
                        text2.Text = FormatText(item);
                    }
                    i++;
                }
            }
        }

        private string FormatText(Historydrugrecord record)
        {
            var startTime = DateTime.Parse(record.start);
            var endTime = DateTime.Parse(record.end);
            var time = endTime.Subtract(startTime);
            var year = (int)(time.TotalDays / 365);
            var month  = (int)(time.TotalDays % 365 / 30);
            var timeString = "";
            if (year > 0)
                timeString = $"{year}年";
            if (month > 0)
                timeString += $"{month}月";
            
            string result = $"{record.name}: {record.method.day}日{record.method.time}次    始剂量{record.method.dosage}mg/次  持续服用{timeString}";
            //一日3次     始剂量10mg/次   持续服用两年
            return result;
        }

    }
}
