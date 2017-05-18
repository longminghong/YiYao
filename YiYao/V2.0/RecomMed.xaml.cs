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
        WebService.MTMRecMedDTO  dto;
        public RecomMed()
        {
            InitializeComponent();

            this.add_west_chinese_images();
            this.add_west_HCP_images();
            this.add_west_med_images();
            
            xiyaoScrollView.ManipulationBoundaryFeedback += (s, e) =>
            {
                e.Handled = true;
            };
            zhongyaoScrollView.ManipulationBoundaryFeedback += (s, e) =>
            {
                e.Handled = true;
            };
            baojianpinScrollView.ManipulationBoundaryFeedback += (s, e) =>
            {
                e.Handled = true;
            };
            //baojian.ItemsSource = new string[] { "1.png", "2.png", "3.png", "4.png", "1.png", "2.png", "3.png" };
            //zhongyao.ItemsSource = new string[] { "1.png", "2.png", "3.png", "4.png", "1.png", "2.png", "3.png" };
            //xiyao.ItemsSource = new string[] { "1.png", "2.png", "3.png", "4.png", "1.png", "2.png", "3.png" };
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
            try
            {
                dto = (WebService.MTMRecMedDTO)data;

                Console.WriteLine("data ======== OK ");

                if (true)
                {
                    if (null != dto.healthproduct)
                    {
                        baojianpinItemControl.ItemsSource = dto.healthproduct;
                    }
                    if (null != dto.chineseherb)
                    {
                        zhongyaoItemControl.ItemsSource = dto.chineseherb;
                    }
                    if (null!= dto.westdrugs)
                    {
                        List<string> westDrug = new List<string>();

                        if (dto.westdrugs.firstdrugs.Count()>0)
                        {
                            foreach (WebService.Firstdrug item in dto.westdrugs.firstdrugs)
                            {
                                if (!String.IsNullOrEmpty(item.name))
                                {
                                    westDrug.Add(item.name);
                                }
                                else if (!String.IsNullOrEmpty(item.common_name))
                                {
                                    westDrug.Add(item.common_name);
                                }
                            }
                        }

                        if (dto.westdrugs.seconddrugs.Count() > 0)
                        {
                            foreach (WebService.Seconddrug item in dto.westdrugs.seconddrugs)
                            {
                                if (!String.IsNullOrEmpty(item.name))
                                {
                                    westDrug.Add(item.name);
                                }
                                else if (!String.IsNullOrEmpty(item.common_name))
                                {
                                    westDrug.Add(item.common_name);
                                }
                            }
                        }

                        if (westDrug.Count()>0)
                        {   
                            xiyaoItemControl.ItemsSource = westDrug;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
           
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
