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
using System.Globalization;
using System.Net;
using Newtonsoft.Json.Linq;

namespace YiYao
{
    /// <summary>
    /// ShoppingCar.xaml 的交互逻辑
    /// </summary>
    public partial class ShoppingCar : UserControl, INavigable
    {
        string drugId = "";
        MTMShopCarDTO reciveDTO;
        public ShoppingCar()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                Window.GetWindow(this).TextInput += ShopingCarScanDrug_TextInput;
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
                reciveDTO = (MTMShopCarDTO)args;

                mycontrol.ItemsSource = reciveDTO.buydrugs;

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

            reciveDTO = (MTMShopCarDTO)data;

            mycontrol.ItemsSource = reciveDTO.buydrugs;
        }
        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(Login));
        }

        private void mycontrol_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void med_image_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void med_detail_bg_image_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ShopingCarScanDrug_TextInput(object sender, TextCompositionEventArgs e)
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
                finally
                {

                    drugId = string.Empty;
                }
            }
        }
        private void postDataWithWebClient(DrugBarCode code)
        {
            try
            {
                WebClient wc = new WebClient();
                string requestUrl = "http://test.o4bs.com/api/members/drugbarcode";
                
                JObject carJson = JObject.FromObject(code);
                string paramStr = carJson.ToString();

                byte[] postData = Encoding.UTF8.GetBytes(paramStr);

                wc.Headers.Add("Content-Type", "application/json");
                wc.Headers.Add("appkey", "097e8751c3c183edf602f867a5326559");
                wc.Headers.Add("appid", "SyccthZn");

                byte[] responseData = wc.UploadData(requestUrl, "POST", postData); // 得到返回字符流
                String resultValue = Encoding.UTF8.GetString(responseData);  // 解码

                Console.WriteLine(resultValue);
            }
            catch (Exception e)
            {
                //MessageBox.Show("A6.xaml postDataWithWebClient Error happen"+e);
            }
        }
    }

    public class ShopCarMedNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string resultValue = "药品名: ";
            if (null != value)
            {
                resultValue += value;
            }
            return resultValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ShopCarMedSpecificationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string resultValue = "规格: ";
            if (null != value)
            {
                resultValue += value;
            }
            return resultValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ShopCarMedNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string resultValue = "购买数量: ";
            if (null != value)
            {
                resultValue += value;

                resultValue += "   (盒)";
            }
            return resultValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ShopCarMedPriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string resultValue = "单价: ";
            if (null != value)
            {
                resultValue += value;
            }
            return resultValue += "   (元)";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ShopCarMedTotalPriceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string resultValue = "总价：";
            try
            {
                if (null != values[0] && null!=values[1])
                {
                    var take_number = (int)values[0];
                    String priceString = (String)values[1];
                    double price = System.Convert.ToDouble(priceString.Trim());
                    resultValue += (take_number * price).ToString();
                }
            }
            catch (Exception)
            {

            }
            finally {

                
            }
            resultValue += "  (元)";

            return resultValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
