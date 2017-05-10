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
    /// MedPlan.xaml 的交互逻辑
    /// </summary>
    public partial class MedPlan : UserControl, INavigable
    {
        MTMMedPlanDTO reciveDTO;
        public MedPlan()
        {
            InitializeComponent();
        }
        public void Start(object args)
        {
            if (null != args)
            {

                //customInfo = (MTMCustInfo)args;
                // to do 数据绑定
                reciveDTO = (MTMMedPlanDTO)args;

                mycontrol.ItemsSource = reciveDTO.plandrugs;

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
            Console.WriteLine("data shopping card ======== OK ");

            reciveDTO = (MTMMedPlanDTO)data;
        }
         
    }
}
