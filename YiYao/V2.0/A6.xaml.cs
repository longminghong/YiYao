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
using System.Net;
using Newtonsoft.Json.Linq;

namespace YiYao
{
    /// <summary>
    /// A6.xaml 的交互逻辑
    /// </summary>
    public partial class A6 : UserControl, INavigable
    {
        MTMMedCollectDTO medCollectDTO;

        string drugId = "";

        int drugCount = 0;
        public A6()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                Window.GetWindow(this).TextInput += ScanDrug_TextInput;
                
            };
            scrollviewer.ManipulationBoundaryFeedback += (s, e) =>
            {
                e.Handled = true;
            };
        }
        public void Start(object args)
        {
            if (null != args)
            {
                // to do 数据绑定
                medCollectDTO = (MTMMedCollectDTO)args;

                if (String.Equals("no", medCollectDTO.isfirst))
                {
                    if (medCollectDTO.drugs.Count() > 0)
                    {
                        // 只显示药物信息
                        allergyCanvas.Visibility = Visibility.Hidden;

                        bloodPressureCanvas.Visibility = Visibility.Hidden;

                        usedrugCanvas.Margin = new Thickness(700, 246, 102, 106);

                    } else if (medCollectDTO.drugs.Count() == 0) {

                    }
                }

                drugCount = medCollectDTO.drugs.Count();
                mycontrol.ItemsSource = medCollectDTO.drugs;

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

            medCollectDTO = (MTMMedCollectDTO)data;

            String value = "收缩压：";
            value += medCollectDTO.systolicpressure;
            systolicpressure.Text = value;

            String diastolicpressureString = "舒张压：";
            diastolicpressureString += medCollectDTO.diastolicpressure;
            diastolicpressure.Text = diastolicpressureString;

            if (medCollectDTO.allergicdrug.Count() > 0)
            {

                String resultValue = "";

                foreach (Allergicdrug item in medCollectDTO.allergicdrug)
                {

                    resultValue += item.drugname;
                    resultValue += "  ;\n";
                }
                isallergy.Text = resultValue;

            }
            else
            {
                isallergy.Text = "\n\n     无过敏药物";
            }

            mycontrol.ItemsSource = medCollectDTO.drugs;
            mycontrol.Items.Refresh();

            if (drugCount<medCollectDTO.drugs.Count())
            {
                scrollviewer.ScrollToBottom();
            }
            drugCount = medCollectDTO.drugs.Count();
            //drugId = "6924147659034";
            //DoWork();
        }
        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(A7));
        }
 
        private void ScanDrug_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "\r")
            {
                DoWork();
            }
            else
            {
                drugId += e.Text;
            }
        }


        private void DoWork()
        {

            if (drugId != null)
            {
                try
                {
                    DrugBarCode code = new DrugBarCode();
                    string barcode = drugId;
                    code.sid = "Vk7Tc-PJx";
                    code.barcode = barcode;

                    postDataWithWebClient(code);
                }
                catch (Exception)
                {

                }
                finally {

                    drugId = string.Empty;
                }
            }

            
        }
        private void postDataWithWebClient(DrugBarCode code)
        {
            try
            {
                //MessageBox.Show("A6.xaml postDataWithWebClient---"+code.barcode);
                WebClient wc = new WebClient();
                string requestUrl = "http://test.o4bs.com/api/members/drugbarcode";
                // 采取POST方式必须加的Header
                
                JObject carJson = JObject.FromObject(code);
                string paramStr = carJson.ToString();

                byte[] postData = Encoding.UTF8.GetBytes(paramStr);

                wc.Headers.Add("Content-Type", "application/json");
                wc.Headers.Add("appkey", "097e8751c3c183edf602f867a5326559");
                wc.Headers.Add("appid", "SyccthZn");

                byte[] responseData = wc.UploadData(requestUrl, "POST", postData); // 得到返回字符流
                String resultValue = Encoding.UTF8.GetString(responseData);// 解码

                Console.WriteLine(resultValue);
            }
            catch (Exception e)
            {
                //MessageBox.Show("A6.xaml postDataWithWebClient Error happen"+e);
            }
            
        }
        
    }
    public class DrugBarCode
    {

        public string sid;
        public string barcode;
    }
}
