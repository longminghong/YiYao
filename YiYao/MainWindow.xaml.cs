using CardReader;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WebService.Event;
using YiYao.Util;
using WebService;
using YiYao.Events;
using System.Text.RegularExpressions;

namespace YiYao
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IEventAggregator mEventAggregator;

        WebSocketSingleton websocketInstance;

          public MainWindow(IEventAggregator eventAggregator)
        {
            websocketInstance = WebSocketSingleton.GetInstance();
            websocketInstance.start();
            websocketInstance.pageCommandHandle += new WebSocketSingleton.SocketPageCommandHandleEvent(webSocketPageCommandEvents);
            websocketInstance.connectCloseHandle += new WebSocketSingleton.SocketConnectCloseHandleEvent(webSocketConnectClose);
            
            InitializeComponent();
            mEventAggregator = eventAggregator;
            ImageSourceConverter imageConveter = new ImageSourceConverter();
            NavigationManager manager = new NavigationManager();

            Uri riskImageUri = null;
            
            String riskImagePath = "pack://application:,,,/屈乐.bmp";
            bool imageExist;
            imageExist = System.IO.File.Exists(riskImagePath);

            riskImageUri = new Uri(riskImagePath);

            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = riskImageUri; 
            bi3.EndInit();
            

            try
            {
                AppData.CurrentIDCard = new IDCard
                {
                    Name = "屈乐",
                    Sex = "男",
                    Nationality = "汉",
                    Address = "江苏省无锡市新区长江路111号",
                    BirthDay = "19891014",
                    IDNumber = "120103196007222159",
                    Phone = "15895326302",
                    HeadImage = bi3//(BitmapSource)imageConveter.ConvertFrom("屈乐.bmp")
                };
            }
            catch (Exception)
            {
                Console.WriteLine("Error Happen ");
            }
            
            this.Loaded += MainWindow_Loaded;
            mEventAggregator.GetEvent<WebErrorEvent>().Subscribe(OnWebError);
        }

        void webSocketConnectClose()
        {

            websocketInstance.start();
        }

        void webSocketPageCommandEvents(MEMBERType sender, object deSerDataObject)
        {

            Console.WriteLine("recive page command");
            Type pageType = null;
            switch (sender)
            {
                case MEMBERType.MEMBBASIC:
                    {// 新建会员采集基本信息

                        Console.WriteLine("go to page A3");
                        pageType = typeof(A3);
                    }
                    break;
                case MEMBERType.MEMBHEALTH:
                    {//新建会员时，采集健康信息
                        pageType = typeof(A5);
                    }
                    break;
                case MEMBERType.MEMBDRUG:
                    {//新建会员时，采集用药信息
                        pageType = typeof(A6);
                    }
                    break;
                case MEMBERType.MEMBDISEASE:
                    {//新建会员时，采集疾病风险信息
                        pageType = typeof(A7);
                    }
                    break;
                case MEMBERType.MEMBQR:
                    {//供会员关注和绑定的二维码
                        pageType = typeof(A4);
                    }
                    break;
                case MEMBERType.MEMBRISK:
                    {//会员评估结果数据
                        pageType = typeof(A8);
                    }
                    break;
                case MEMBERType.MEMBRECOMM:
                    {//推荐用药数据
                        pageType = typeof(RecomMed);
                    }
                    break;
                case MEMBERType.MEMBCART:
                    {//药品购物车
                        pageType = typeof(ShoppingCar);
                    }
                    break;
                case MEMBERType.MEMBPLAN:
                    {//会员用药计划
                        pageType = typeof(MedPlan);
                    }
                    break;
                case MEMBERType.MEMBEDIT:
                    { // 修改用户基本信息
                        pageType = typeof(UserInfoEdit);
                    }
                    break;
                case MEMBERType.MEMBEDITDISEASE:
                    { // 修改用户基本信息
                        pageType = typeof(A7);
                    }
                    break;
                    
                default:
                    //
                    break;
            }
            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                root.GoToPageWithArgs(pageType, deSerDataObject);

                mEventAggregator.GetEvent<WebSocketEvent>().Publish(deSerDataObject);
            }));
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //root.GoToPage(typeof(Dashboard));
            root.GoToPage(typeof(Login));
        }

        private void OnWebError(string message)
        {
            ShowMessage(message);
        }

        private void ShowMessage(string message)
        {
            // messageText.Text = message;
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames d1 = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame k1 = new EasingDoubleKeyFrame();
            k1.KeyTime = TimeSpan.FromMilliseconds(300);
            k1.Value = 1;
            d1.KeyFrames.Add(k1);
            EasingDoubleKeyFrame k2 = new EasingDoubleKeyFrame();
            k2.KeyTime = TimeSpan.FromMilliseconds(600);
            k2.Value = 1;
            d1.KeyFrames.Add(k2);
            EasingDoubleKeyFrame k3 = new EasingDoubleKeyFrame();
            k3.KeyTime = TimeSpan.FromMilliseconds(900);
            k3.Value = 0;
            d1.KeyFrames.Add(k3);

            ObjectAnimationUsingKeyFrames d2 = new ObjectAnimationUsingKeyFrames();
            d2.KeyFrames.Add(new DiscreteObjectKeyFrame
            {
                KeyTime = TimeSpan.FromMilliseconds(0),
                Value = Visibility.Visible
            });
            d2.KeyFrames.Add(new DiscreteObjectKeyFrame
            {
                KeyTime = TimeSpan.FromMilliseconds(900),
                Value = Visibility.Collapsed
            });



            storyboard.Children.Add(d1);
            storyboard.Children.Add(d2);
            Storyboard.SetTarget(d1, messageText);
            Storyboard.SetTarget(d2, messageText);
            Storyboard.SetTargetProperty(d1, new PropertyPath("Opacity"));
            Storyboard.SetTargetProperty(d2, new PropertyPath("Visibility"));
            storyboard.Begin();
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            bool shift = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;
            int speed = shift ? 5 : 1;
            if (e.Key == Key.Left)
                GVar.x -= speed;
            if (e.Key == Key.Right)
                GVar.x += speed;
            if (e.Key == Key.Up)
                GVar.y -= speed;
            if (e.Key == Key.Down)
                GVar.y += speed;
            // debug.Text = $"x {GVar.x} y {GVar.y}";
        }




    }
}
