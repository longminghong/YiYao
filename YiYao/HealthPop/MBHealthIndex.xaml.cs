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
    public partial class MBHealthIndex : HealthIndexBase
    {
        HealthDataService mHealthDataService;
        public MBHealthIndex()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
             {
                 FindStoryboard();
             };
        }

        public async void LoadData()
        {
            var healthDataService = ServiceLocator.Current.GetInstance<HealthDataService>();
            var data = await healthDataService.GetHealthDataAsync();
            if (data != null)
            {
                var historyDrugrecord = data.data.historydrugrecord;
                int i = 0;
               
            }
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

        public override void BeginTransform()
        {
            base.BeginTransform();
            //DoubleAnimation da = new DoubleAnimation(0, TimeSpan.FromMilliseconds(200));
            //xian.BeginAnimation(OpacityProperty, da);
        }

        public override void EndTransform()
        {
            base.EndTransform();
            //DoubleAnimation da = new DoubleAnimation(1, TimeSpan.FromMilliseconds(200));
            //xian.BeginAnimation(OpacityProperty, da);
        }
    }
}
