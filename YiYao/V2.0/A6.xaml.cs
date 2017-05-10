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
    /// A6.xaml 的交互逻辑
    /// </summary>
    public partial class A6 : UserControl, INavigable
    {
        MTMMedCollectDTO medCollectDTO;
        public A6()
        {
            InitializeComponent();

            //scrollviewer.ManipulationBoundaryFeedback += (s, e) =>
            //{   
            //    e.Handled = true;
            //};
        }
        public void Start(object args)
        {
            if (null != args)
            {   
                //customInfo = (MTMCustInfo)args;
                // to do 数据绑定
                medCollectDTO = (MTMMedCollectDTO)args;

                //mycontrol.ItemsSource = medCollectDTO.drugs;

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
            Console.WriteLine("data A6 ======== OK ");
            return;
            //medCollectDTO = (MTMMedCollectDTO)data;

            //String value = "收缩压：";
            //value += medCollectDTO.systolicpressure;
            //systolicpressure.Text = value;

            //String diastolicpressureString = "舒张压：";
            //diastolicpressureString += medCollectDTO.diastolicpressure;
            //diastolicpressure.Text = diastolicpressureString;

            //if (medCollectDTO.allergicdrug.Count() > 0) {

            //    String resultValue = "";

            //    foreach (Allergicdrug item in medCollectDTO.allergicdrug) {

            //        resultValue += item.drugname;
            //        resultValue += "  ;";
            //    }
            //    isallergy.Text = resultValue;

            //}

            //mycontrol.ItemsSource = medCollectDTO.drugs;
            //mycontrol.Items.Refresh();

        }
        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(A7));
        }
    }
}
