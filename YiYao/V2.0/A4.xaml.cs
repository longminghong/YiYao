using Microsoft.Practices.ServiceLocation;
using Prism.Events;
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
using YiYao.Events;

namespace YiYao
{
    /// <summary>
    /// A4.xaml 的交互逻辑
    /// </summary>
    public partial class A4 : UserControl, INavigable
    {
        //Storyboard A4Storyboard;
        MTMQRDTO qrDTO;
        public A4()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                //A4Storyboard = FindResource("A4Storyboard1") as Storyboard;
                
                //A4Storyboard.Duration = TimeSpan.FromSeconds(2);
                //A4Storyboard.Begin();
            };
        }

        public void Start(object args)
        {
            if (null != args)
            {
                try
                {
                    setDataAndRefresh(args);
                }
                catch (Exception)
                {

                }

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

            if (null != data)
            {
                setDataAndRefresh(data);
            }
        }

        private void setDataAndRefresh(object data) {

            qrDTO = (MTMQRDTO)data;
            Uri qrImagePath = new Uri(qrDTO.src);
            BitmapImage qrImageSource = new BitmapImage(qrImagePath);
            imageQR.Source = qrImageSource;
        }

        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(A5));
        }

        private void imageQR_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Storyboard qrStoryboard;

                qrStoryboard = this.Resources["QRStoryboard"] as Storyboard;
                if (null != qrStoryboard)
                {
                    qrStoryboard.Begin();
                }
            }
            catch (Exception)
            {

            }
           

        }
    }
}
