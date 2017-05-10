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
            InitializeComponent();
            mEventAggregator = eventAggregator;
            ImageSourceConverter imageConveter = new ImageSourceConverter();
            //AppData.CurrentIDCard = new IDCard
            //{
            //    Name = "屈乐",
            //    Sex = "男",
            //    Nationality = "汉",
            //    Address = "江苏省无锡市新区长江路111号",
            //    BirthDay = "19891014",
            //    IDNumber = "120103196007222159",
            //    Phone = "15895326302",
            //    HeadImage = (BitmapSource)imageConveter.ConvertFrom("屈乐.bmp")
            //};
            this.Loaded += MainWindow_Loaded;
            mEventAggregator.GetEvent<WebErrorEvent>().Subscribe(OnWebError);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
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
