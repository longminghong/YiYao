﻿using System;
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
using System.Globalization;
using System.Collections.ObjectModel;
using MultiChartDemo;
using System.Diagnostics;

namespace YiYao
{
    /// <summary>
    /// A8.xaml 的交互逻辑
    /// </summary>
    public partial class A8 : UserControl, INavigable
    {   
        MTMIssueCollectDTO reciveDTO;
        
        MultiChartDataModel dataModel = new MultiChartDataModel();

        public A8()
        {   
            InitializeComponent();
           // baojian.ItemsSource = new string[] { "/YiYao;component/1.png", "/YiYao;component/2.png", "/YiYao;component/3.png", "/YiYao;component/4.png", "/YiYao;component/1.png" };
            baojian.ManipulationBoundaryFeedback += (s, e) =>
            {
                e.Handled = true;
            };
            yaoscrollviewer.ManipulationBoundaryFeedback += (s, e) =>
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

                this.DataContext = dataModel;
                drawChart();

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

            //if (0 == reciveDTO.risklevel) return;

            reciveDTO = (MTMIssueCollectDTO)data;

            Uri riskImageUri = null;
            String ristValueString = "";
            String riskImagePath = "";

            username.Text = "用户名："+ reciveDTO.name;
            usergender.Text = "性别："+reciveDTO.gender;
            userbirthday.Text = "生日："+reciveDTO.birthday;
            userage.Text = "年龄："+reciveDTO.age +" 岁";
            useraddress.Text = "地址："+reciveDTO.location;
            
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

            drawChart();

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
                
                List<string> chinaDiseaseData = new List<string>();
                
                if (reciveDTO.chinadisease.brain.Count()>0)
                {
                    foreach (string item in reciveDTO.chinadisease.brain)
                    {
                        chinaDiseaseData.Add(item);
                    }
                }
                if (reciveDTO.chinadisease.eye.Count() > 0)
                {
                    foreach (string item in reciveDTO.chinadisease.eye)
                    {
                        chinaDiseaseData.Add(item);
                    }
                }
                if (reciveDTO.chinadisease.heart.Count() > 0)
                {
                    foreach (string item in reciveDTO.chinadisease.heart)
                    {
                        chinaDiseaseData.Add(item);
                    }
                }
                if (reciveDTO.chinadisease.blood.Count() > 0)
                {
                    foreach (string item in reciveDTO.chinadisease.blood)
                    {
                        chinaDiseaseData.Add(item);
                    }
                }
                if (reciveDTO.chinadisease.pancreas.Count() > 0)
                {
                    foreach (string item in reciveDTO.chinadisease.pancreas)
                    {
                        chinaDiseaseData.Add(item);
                    }
                }
                if (reciveDTO.chinadisease.kidney.Count() > 0)
                {
                    foreach (string item in reciveDTO.chinadisease.kidney)
                    {
                        chinaDiseaseData.Add(item);
                    }
                }
                
                issueControl.ItemsSource = chinaDiseaseData;

                if (null != reciveDTO.nowdrugs)
                {
                    baojian.ItemsSource = reciveDTO.nowdrugs;
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void drawChart() {

            List<SalesPerformance> xueyaSalesData = new List<SalesPerformance>();
            List<SalesPerformance> xuetangSalesData = new List<SalesPerformance>();
            List<SalesPerformance> xuezhiSalesData = new List<SalesPerformance>();

            try
            {
                if (reciveDTO.chart_data.xueya.x.Count() > 0)
                {

                    SalesPerformance shousuoya = new SalesPerformance();
                    shousuoya.SalesTotals = new List<SalesInfo>();
                    shousuoya.SalesName = "收缩压";

                    SalesPerformance shuzhangya = new SalesPerformance();
                    shuzhangya.SalesTotals = new List<SalesInfo>();
                    shuzhangya.SalesName = "舒张压";
                    
                    if (null != reciveDTO.chart_data.xueya.systolicpressure)
                    {
                        var count = reciveDTO.chart_data.xueya.systolicpressure.Count();
                        for (int i = 0; i < count; i++)
                        {
                            var temDate = reciveDTO.chart_data.xueya.systolicpressure[i][0];
                            var temData = reciveDTO.chart_data.xueya.systolicpressure[i][1];

                            float value = float.Parse(temData);

                            shousuoya.SalesTotals.Add(new SalesInfo
                            {
                                Date = temDate,

                                SalesTotal = value > 0 ? value : 0
                            });
                        }
                    }

                    if (null != reciveDTO.chart_data.xueya.diastolicpressure)
                    {
                        var count = reciveDTO.chart_data.xueya.diastolicpressure.Count();
                        for (int i = 0; i < count; i++)
                        {
                            var temDate = reciveDTO.chart_data.xueya.diastolicpressure[i][0];
                            var temData = reciveDTO.chart_data.xueya.diastolicpressure[i][1];

                            float value = float.Parse(temData);

                            shuzhangya.SalesTotals.Add(new SalesInfo
                            {
                                Date = temDate,

                                SalesTotal = value > 0 ? value : 0
                            });
                        }
                    }

                    xueyaSalesData.Add(shousuoya);
                    xueyaSalesData.Add(shuzhangya);
                }

                if (reciveDTO.chart_data.xuetang.x.Count() > 0)
                {
                    SalesPerformance kongfu = new SalesPerformance();
                    kongfu.SalesTotals = new List<SalesInfo>();
                    kongfu.SalesName = "空腹血糖";

                    SalesPerformance suiji = new SalesPerformance();
                    suiji.SalesTotals = new List<SalesInfo>();
                    suiji.SalesName = "随机血糖";

                    /////////

                    if (null != reciveDTO.chart_data.xuetang.fastBloodSugar)
                    {
                        var count = reciveDTO.chart_data.xuetang.fastBloodSugar.Count();
                        for (int i = 0; i < count; i++)
                        {
                            var temDate = reciveDTO.chart_data.xuetang.fastBloodSugar[i][0];
                            
                            var temData = reciveDTO.chart_data.xuetang.fastBloodSugar[i][1].ToString();

                            float value = float.Parse(temData);

                            kongfu.SalesTotals.Add(new SalesInfo
                            {
                                Date = temDate,

                                SalesTotal = value > 0 ? value : 0
                            });
                        }
                    }



                    if (null != reciveDTO.chart_data.xuetang.randomBloodSugar)
                    {
                        var count = reciveDTO.chart_data.xuetang.randomBloodSugar.Count();
                        for (int i = 0; i < count; i++)
                        {
                            var temDate = reciveDTO.chart_data.xuetang.randomBloodSugar[i][0];
                            var temData = reciveDTO.chart_data.xuetang.randomBloodSugar[i][1];

                            float value = float.Parse(temData);

                            suiji.SalesTotals.Add(new SalesInfo
                            {
                                Date = temDate,

                                SalesTotal = value > 0 ? value : 0
                            });
                        }
                    }
                    ///////////////////
                    
                    xuetangSalesData.Add(kongfu);
                    xuetangSalesData.Add(suiji);
                }

                if (reciveDTO.chart_data.xuezhi.x.Count() > 0)
                {

                    SalesPerformance cholesteroly = new SalesPerformance();
                    cholesteroly.SalesTotals = new List<SalesInfo>();
                    cholesteroly.SalesName = "胆固醇";

                    SalesPerformance triglyceridey = new SalesPerformance();
                    triglyceridey.SalesTotals = new List<SalesInfo>();
                    triglyceridey.SalesName = "甘油三脂";

                    SalesPerformance ldlcy = new SalesPerformance();
                    ldlcy.SalesTotals = new List<SalesInfo>();
                    ldlcy.SalesName = "ldlcy";

                    SalesPerformance hdlcy = new SalesPerformance();
                    hdlcy.SalesTotals = new List<SalesInfo>();
                    hdlcy.SalesName = "hdlcy";

                    //////

                    if (null != reciveDTO.chart_data.xuezhi.cholesteroly)
                    {
                        var count = reciveDTO.chart_data.xuezhi.cholesteroly.Count();
                        for (int i = 0; i < count; i++)
                        {
                            var temDate = reciveDTO.chart_data.xuezhi.cholesteroly[i][0];
                            var temData = reciveDTO.chart_data.xuezhi.cholesteroly[i][1];
                            float value = float.Parse(temData);
                            cholesteroly.SalesTotals.Add(new SalesInfo
                            {
                                Date = temDate,

                                SalesTotal = value>0 ? value : 0
                            });
                        }
                    }

                    //////// 2 

                    if (null != reciveDTO.chart_data.xuezhi.triglyceridey)
                    {
                        var count = reciveDTO.chart_data.xuezhi.triglyceridey.Count();
                        for (int i = 0; i < count; i++)
                        {
                            var temDate = reciveDTO.chart_data.xuezhi.triglyceridey[i][0];
                            var temData = reciveDTO.chart_data.xuezhi.triglyceridey[i][1];
                            float value = float.Parse(temData);
                            triglyceridey.SalesTotals.Add(new SalesInfo
                            {
                                Date = temDate,

                                SalesTotal = value > 0 ? value : 0
                            });
                        }
                    }

                    ///////3
                    if (null != reciveDTO.chart_data.xuezhi.ldlcy)
                    {
                        var count = reciveDTO.chart_data.xuezhi.ldlcy.Count();
                        for (int i = 0; i < count; i++)
                        {
                            var temDate = reciveDTO.chart_data.xuezhi.ldlcy[i][0];
                            var temData = reciveDTO.chart_data.xuezhi.ldlcy[i][1];
                            float value = float.Parse(temData);
                            ldlcy.SalesTotals.Add(new SalesInfo
                            {
                                Date = temDate,

                                SalesTotal = value>0 ? value : 0
                            });
                        }
                    }


                    //////4
                    if (null != reciveDTO.chart_data.xuezhi.hdlcy)
                    {
                        var count = reciveDTO.chart_data.xuezhi.hdlcy.Count();
                        for (int i = 0; i < count; i++)
                        {
                            var temDate = reciveDTO.chart_data.xuezhi.hdlcy[i][0];
                            var temData = reciveDTO.chart_data.xuezhi.hdlcy[i][1];
                            float value = float.Parse(temData);
                            hdlcy.SalesTotals.Add(new SalesInfo
                            {
                                Date = temDate,

                                SalesTotal = value > 0 ? value : 0
                            });
                        }
                    }

                    //////
                    
                    xuezhiSalesData.Add(cholesteroly);
                    xuezhiSalesData.Add(triglyceridey);

                    xuezhiSalesData.Add(ldlcy);
                    xuezhiSalesData.Add(hdlcy);
                }
                
                dataModel.xueyaSalesData = xueyaSalesData;
                dataModel.xuetangSalesData = xuetangSalesData;
                dataModel.xuezhiSalesData = xuezhiSalesData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("详情页：画图失败"+ex.Message);
            }
           
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

    public class NowDrugNameConverter :  IMultiValueConverter
    {   
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string resultValue = "-";
            try
            {
                if (values.Count()>=2)
                {
                    string drug_name = "";
                    string drug_common_name = "";

                    if (null != values[0])
                    {
                        var temValue = values[0];
                        drug_name = temValue.ToString();
                    }
                     
                    if (null!=values[1])
                    {
                        var temValue = values[1];
                        drug_common_name = temValue.ToString();
                    }
                    
                    if (!String.IsNullOrEmpty(drug_name.Trim()))
                    {
                        resultValue = drug_name.Trim();
                    }
                    else if (!String.IsNullOrEmpty(drug_common_name.Trim()))
                    {

                        resultValue = drug_common_name.Trim();
                    }
                    else {

                        resultValue = "-";
                    }
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine("A8 NowDrugNameConverter Error" + e.Message);
            }
            finally
            {
                
            }
            return resultValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
