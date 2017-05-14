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

namespace YiYao
{
    /// <summary>
    /// ShoppingCar.xaml 的交互逻辑
    /// </summary>
    public partial class ShoppingCar : UserControl, INavigable
    {

        MTMShopCarDTO reciveDTO;
        public ShoppingCar()
        {
            InitializeComponent();
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
            }
            return resultValue += "   (盒)";
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
            string resultValue = "";
            try
            {
                var take_number = (int)values[0];
                var priceString = values[1];
                double price = 6.6;//System.Convert.ToDouble(priceString.Trim());
                resultValue = (take_number * price).ToString();
            }
            catch (Exception)
            {


            }
            finally {

                resultValue += "  (元)";
            }
            return resultValue.ToArray();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
