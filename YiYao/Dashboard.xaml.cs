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
using YiYao;
using YiYao.Util;

namespace YiYao
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Dashboard : UserControl, INavigable
    {
        private int StartX;
        private int StartY;
        private CircelButton mLastSelectCircle;
        private List<CircelButton> mcircle;
        Storyboard mShowCircle;
        Storyboard mRotate1;
        Storyboard mRotate2;
        Storyboard mHideHead;
        Storyboard mShowHead;
        public Dashboard()
        {
            InitializeComponent();
            this.Loaded +=  (s, e) =>
            {
                mcircle = new List<CircelButton> { _1_png , _2_png, _3_png, _4_png, _5_png, _6_png };
                var story = FindResource("Storyboard1") as Storyboard;
                mShowCircle = FindResource("ShowCircle") as Storyboard;
                mShowCircle.BeginTime = TimeSpan.FromSeconds(3);
                mShowCircle.Begin();

                mRotate1 = FindResource("Rotate1") as Storyboard;
                mRotate2 = FindResource("Rotate2") as Storyboard;
                mRotate2.RepeatBehavior = RepeatBehavior.Forever;
                mRotate1.Completed += (ss, ee) =>
                 {
                     mRotate2.Begin();
                 };
                mRotate1.Begin();

                mHideHead = FindResource("HideHead") as Storyboard;
                mShowHead = FindResource("ShowHead") as Storyboard;

                int x = (int)Canvas.GetLeft(Board);
                int y = (int)Canvas.GetTop(Board);
                StartX = (int)Canvas.GetLeft(Board);
                StartY = (int)Canvas.GetTop(Board);

            };

        }



        private void SelectCircle(CircelButton button)
        {
            if (button == mLastSelectCircle)
                return;
            mLastSelectCircle = button;
            foreach (var circle in mcircle)
            {
                if(circle != button)
                {
                    circle.UnSelect();
                }
            }
        }


        private void OnProfileClick(object sender, RoutedEventArgs e)
        {
            SelectCircle(sender as CircelButton);
            Point target = new Point(StartX + 605, StartY + 17);
            Profile profile = new Profile();
            var animation = TranslateBoard(target, profile);
            animation.Begin();

            (FindResource("ScaleBoard") as Storyboard).Begin();
        }

        private void OnKnowClick(object sender, RoutedEventArgs e)
        {
            SelectCircle(sender as CircelButton);

            Point target = new Point(StartX - 505, StartY - 162);
            Know profile = new Know();
            var animation = TranslateBoard(target, profile);
            animation.Begin();
            (FindResource("ScaleBoard") as Storyboard).Begin();
        }

        private void OnHealthClick(object sender, RoutedEventArgs e)
        {
            huiyuanming_png.Visibility = Visibility.Collapsed;
            backButton.Visibility = Visibility.Visible;
            homeButton.Visibility = Visibility.Collapsed;
            Point target = new Point(StartX, StartY);
            HealthIndex health = new HealthIndex();
            var button = sender as CircelButton;
            SelectCircle(button);
            var animation = TranslateBoard(target, health);
            animation.Completed += (ss, ee) =>
             {
                 var hide = FindResource("HideCircle") as Storyboard;
                 hide.Begin();
             };
            animation.Begin();
            (FindResource("UnScaleBoard") as Storyboard).Begin();


        }

        private void OnRiskClick(object sender, RoutedEventArgs e)
        {
            SelectCircle(sender as CircelButton);

            Point target = new Point(StartX - 500, StartY + 24);
            Risk risk = new Risk();
            var animation = TranslateBoard(target, risk);
            animation.Begin();
            (FindResource("ScaleBoard") as Storyboard).Begin();

        }

        private void OnDrugUsage(object sender, RoutedEventArgs e)
        {
            huiyuanming_png.Visibility = Visibility.Collapsed;
            backButton.Visibility = Visibility.Visible;
            homeButton.Visibility = Visibility.Collapsed;
            Point target = new Point(StartX, StartY);

            ScanDrug scan = new ScanDrug();
            var animation = TranslateBoard(target, scan);
            animation.Completed += (ss, ee) =>
            {
                var hide = FindResource("HideCircle") as Storyboard;
                hide.Begin();
            };
            animation.Begin();
            (FindResource("UnScaleBoard") as Storyboard).Begin();
            mHideHead.Begin();

        }

        private void MyPrescription(object sender, RoutedEventArgs e)
        {
            SelectCircle(sender as CircelButton);

            Point target = new Point(StartX - 543, StartY + 165);
            MyPrescription prescription = new MyPrescription();
            var animation = TranslateBoard(target, prescription);
            animation.Begin();
            (FindResource("ScaleBoard") as Storyboard).Begin();

        }

        private Storyboard TranslateBoard(Point target, UserControl circle)
        {
            Point current = new Point(Canvas.GetLeft(Board), Canvas.GetTop(Board));
            var length = (target - current).Length;
            Debug.WriteLine(length);
            TimeSpan duration = TimeSpan.FromSeconds(0.5);
            if (length > 1000 )
              duration = TimeSpan.FromSeconds(1);

            PowerEase ease = new PowerEase();
            ease.Power = 2;
            ease.EasingMode = EasingMode.EaseInOut;

            circleContainer.Children.Clear();
            Storyboard story = new Storyboard();
            story.FillBehavior = FillBehavior.Stop;
            
            DoubleAnimation x = new DoubleAnimation();
            x.EasingFunction = ease;
            x.Duration = duration;
            x.To = target.X;

            DoubleAnimation y = new DoubleAnimation();
            y.EasingFunction = ease;
            y.Duration = duration;
            y.To = target.Y;

            story.Children.Add(x);
            story.Children.Add(y);
            Storyboard.SetTarget(x, Board);
            Storyboard.SetTarget(y, Board);
            Storyboard.SetTargetProperty(x, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(y, new PropertyPath("(Canvas.Top)"));
            

            story.Completed += (s, ee) =>
            {
                Canvas.SetLeft(Board, target.X);
                Canvas.SetTop(Board, target.Y);
                if (circle != null)
                {
                    circleContainer.Children.Add(circle);
                }
            };

            return story;
        }

        public void Start(object args)
        {
            try
            {
                var card = AppData.CurrentIDCard;
                //nameText.Text = card.Name;
                //sexText.Text = card.Sex;
                //nationalityText.Text = card.Nationality;
                //birthdayText.Text = card.BirthDay;
                //addressText.Text = card.Address;
                //idNumberText.Text = card.IDNumber;

                headImage.Source = AppData.GetAvatar();

                string sex = card.Sex == "男" ? "先生" : "女士";
                huiyuanming_png.Text = $"会员：{card.Name}{sex}";
                
            }
            catch (Exception)
            {

                Console.Error.WriteLine("DAshboard.xaml error happen.");
            }
            
        }

        public void Stop()
        {
            
        }

        private void homeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(Login));
        }

        private void backButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            huiyuanming_png.Visibility = Visibility.Visible;
            homeButton.Visibility = Visibility.Visible;
            backButton.Visibility = Visibility.Collapsed;
            circleContainer.Children.Clear();
            mShowCircle.BeginTime = TimeSpan.FromSeconds(0);
            
            mShowCircle.Begin();
            foreach (var circle in mcircle)
            {
                circle.UnSelect();
            }
            mShowHead.Begin();
        }

        private void centerButton_Click(object sender, RoutedEventArgs e)
        {
            if (circleContainer.Children.OfType<Profile>().FirstOrDefault() == null)
            {
                _5_png.RaiseEvent(new RoutedEventArgs(CircelButton.ClickEvent));
            }
            else
            {
                foreach (var circle in mcircle)
                {
                   circle.UnSelect();
                    
                }
                Point target = new Point(StartX, StartY);
                var animation = TranslateBoard(target, null);
                animation.Begin();
                (FindResource("UnScaleBoard") as Storyboard).Begin();
            }
        }
    }
}

