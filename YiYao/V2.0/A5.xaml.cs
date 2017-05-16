using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json.Linq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// A5.xaml 的交互逻辑
    /// </summary>
    public partial class A5 : UserControl, INavigable
    {
        private MTMHealthCollectDTO healthDTO;

        public A5()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                var loadingAnimation = FindResource("A5Storyboard1") as Storyboard;
                loadingAnimation.Completed += (ss, ee) =>
                {

                };
                loadingAnimation.Begin();
            };
        }

        public void Start(object args)
        {
            if (null != args)
            {
                //customInfo = (MTMCustInfo)args;
                // to do 数据绑定
                healthDTO = (MTMHealthCollectDTO)args;
                Prism.Events.EventAggregator eventAggragator = ServiceLocator.Current.GetInstance<EventAggregator>();
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
            Console.WriteLine("A5 ======== OK ");

            if (null != data)
            {
                healthDTO = data as MTMHealthCollectDTO;
                String foodString = "";
                if (1 == healthDTO.eatingpreference)
                    foodString += "饮食偏好：高盐 ";
                else if (2 == healthDTO.eatingpreference)
                    foodString += "饮食偏好：低盐 ";
                else
                    foodString += "饮食偏好：未知 ";
                textBlock2.Text = foodString;


                String smoking = "";
                if (String.Equals("no", healthDTO.smoking))
                    smoking += "抽烟：否";
                else if (String.Equals("yes", healthDTO.smoking))
                    smoking += "抽烟：是";
                else
                    smoking += "抽烟：未知";
                textBlock11.Text = smoking;


                String drink = "";
                if (String.Equals("no", healthDTO.drinking))
                    drink += "酗酒：否";
                else if (String.Equals("yes", healthDTO.drinking))
                    drink += "酗酒：是";
                else
                    drink += "酗酒：未知";

                textBlock12.Text = drink;

                textBlock3.Text = "身高:" + healthDTO.height;
                textBlock13.Text = "体重:" + healthDTO.weight;
                textBlock14.Text = "腹围:" + healthDTO.waist;

                textBlock5.Text = "收缩压:" + healthDTO.systolicpressure;
                textBlock15.Text = "舒张压:" + healthDTO.diastolicpressure;

                textBlock7.Text = "空腹血糖:" + healthDTO.fastsugar;
                textBlock16.Text = "随机血糖:" + healthDTO.randomsugar;
                textBlock17.Text = "糖化血红蛋白:" + healthDTO.HbA1c;

                textBlock18.Text = "甘油三脂:" + healthDTO.triglyceride;
                textBlock9.Text = "总胆固醇:" + healthDTO.cholesterol;
                textBlock19.Text = "LDL-C:" + healthDTO.ldl;
                textBlock10.Text = "HDL-C :" + healthDTO.hdl;

            }
        }

        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e) {


        }
    }
}
