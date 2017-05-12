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

namespace YiYao
{
    /// <summary>
    /// RecomMed.xaml 的交互逻辑
    /// </summary>
    public partial class RecomMed : UserControl, INavigable
    {

        int demoImageCount = 5;

        public RecomMed()
        {
            InitializeComponent();

            this.add_west_chinese_images();
            this.add_west_HCP_images();
            this.add_west_med_images();
            xiyao.ManipulationBoundaryFeedback += (s, e) =>
            {
                e.Handled = true;
            };
            zhongyao.ManipulationBoundaryFeedback += (s, e) =>
            {
                e.Handled = true;
            };
            baojian.ManipulationBoundaryFeedback += (s, e) =>
            {
                e.Handled = true;
            };
            baojian.ItemsSource = new string[] { "1.png", "2.png", "3.png", "4.png", "1.png", "2.png", "3.png" };
            zhongyao.ItemsSource = new string[] { "1.png", "2.png", "3.png", "4.png", "1.png", "2.png", "3.png" };
            xiyao.ItemsSource = new string[] { "1.png", "2.png", "3.png", "4.png", "1.png", "2.png", "3.png" };
        }


        public void Start(object args)
        {
            if (null != args)
            {

                //customInfo = (MTMCustInfo)args;
                // to do 数据绑定

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
            Console.WriteLine("data ======== OK ");
        }
        private void add_west_med_images()
        {

        }

        private void add_west_chinese_images()
        {
            for (int i = 0; i < 5; i++)
            {
                Image med_image = new Image();


            }
        }

        private void add_west_HCP_images()
        {

        }

        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(ShoppingCar));
        }

    }
}
