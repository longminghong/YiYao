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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebService;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using YiYao.Events;
using System.Windows.Media.Animation;

namespace YiYao
{
    /// <summary>
    /// A8.xaml 的交互逻辑
    /// </summary>
    public partial class A8 : UserControl, INavigable
    {
        MTMIssueCollectDTO reciveDTO;

        public A8()
        {
            InitializeComponent();
            baojian.ItemsSource = new string[] { "/YiYao;component/1.png", "/YiYao;component/2.png", "/YiYao;component/3.png", "/YiYao;component/4.png", "/YiYao;component/1.png" };
            baojian.ManipulationBoundaryFeedback += (s, e) =>
            {
                e.Handled = true;
            };

            yaoscrollviewer.ManipulationBoundaryFeedback += Yaoscrollviewer_ManipulationBoundaryFeedback;

        }

        private void Yaoscrollviewer_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        public void Start(object args)
        {
            if (null != args)
            {
                reciveDTO = (MTMIssueCollectDTO)args;

                EventAggregator eventAggragator = ServiceLocator.Current.GetInstance<EventAggregator>();
                eventAggragator.GetEvent<WebSocketEvent>().Subscribe(OnWebSocketEvent);
            }
        }

        public void Stop()
        {
            EventAggregator eventAggragator = ServiceLocator.Current.GetInstance<EventAggregator>();
            eventAggragator.GetEvent<WebSocketEvent>().Unsubscribe(OnWebSocketEvent);
        }
        private void OnWebSocketEvent(object data)
        {   
            Console.WriteLine("data A8" +
                "" +
                " ======== OK ");

            if (0 == reciveDTO.risklevel) return;

            reciveDTO = (MTMIssueCollectDTO)data;

            Uri riskImageUri = null;
            String ristValueString = "";
            String riskImagePath = "";

            switch (reciveDTO.risklevel)
            {
                case 1:
                    {
                        riskImagePath= "pack://application:,,,/人体绿色.png";
                        ristValueString = "健康";
                    }
                    break;
                case 2:
                    {
                        riskImagePath = "pack://application:,,,/人体黄色.png";
                        
                        ristValueString = "中危";
                    }
                    break;
                case 3:
                    {
                        riskImagePath = "pack://application:,,,/人体红色.png"; 

                        ristValueString = "高危";
                    }

                    break;
            }
 
            
            try
            {
                bool imageExist;
                imageExist = System.IO.File.Exists(riskImagePath);

                riskImageUri = new Uri(riskImagePath);
                textblock_risk.Text = ristValueString;

                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = riskImageUri;
                bi3.EndInit();
                image_risk.Source = bi3;

                textblock_heigh.Text = reciveDTO.measuredata.height.ToString();
                textblock_weigh.Text = reciveDTO.measuredata.weight.ToString();
                textblock_bmi.Text = reciveDTO.measuredata.BMI;
                textblock_waist.Text = reciveDTO.measuredata.waist.ToString();
            }
            catch (Exception)
            {
                
            }
            
            //int w = riskImage.PixelHeight;
        }
        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(ShoppingCar));
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(Dashboard));
        }

        private void threehight_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (null!=reciveDTO&&null!= reciveDTO.controllable_risk)
            {
                show_controllable_risk(reciveDTO.controllable_risk.threehight);
            }
        }

        private void smoking_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (null != reciveDTO && null != reciveDTO.controllable_risk)
            {
                show_controllable_risk(reciveDTO.controllable_risk.smoking);
            }
        }

        private void drinking_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (null != reciveDTO && null != reciveDTO.controllable_risk)
            {
                show_controllable_risk(reciveDTO.controllable_risk.drinking);
            }
        }

        private void fat_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (null != reciveDTO && null != reciveDTO.controllable_risk)
            {
                show_controllable_risk(reciveDTO.controllable_risk.fat);
            }
        }

        private void eat_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (null != reciveDTO && null != reciveDTO.controllable_risk)
            {
                show_controllable_risk(reciveDTO.controllable_risk.eat);
            }
        }

        private void usedrug_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (null != reciveDTO && null != reciveDTO.controllable_risk)
            {
                show_controllable_risk(reciveDTO.controllable_risk.usedrug);
            }
        }

        private void show_controllable_risk(string riskDetail)
        {

        }

        private void image_risk_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard riskStoryboard;
            riskStoryboard = this.Resources["risk_image_show_Storyboard"] as Storyboard;
            if (null != riskStoryboard)
            {
                riskStoryboard.Begin();
            }
        }
    }
}
