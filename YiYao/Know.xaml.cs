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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YiYao.Util;

namespace YiYao
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Know : UserControl
    {
        Storyboard s1;
        Storyboard s2;
        Image currentXian = null;
        private string currentDisease;

        public Know()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                dnxian.Visibility = Visibility.Hidden;
                xzxian.Visibility = Visibility.Hidden;
                szxian.Visibility = Visibility.Hidden;
                xgxian.Visibility = Visibility.Hidden;

                FindStoryboard();

                var xyknow = new XYKnowldge();
                //   root.Children.Add(xyknow);

            };
        }

        private void FindStoryboard()
        {
            s1 = FindResource("Storyboard1") as Storyboard;
            s2 = FindResource("Storyboard2") as Storyboard;

            s2.Completed += (s, e) =>
            {
                ShowXian(currentXian);

                pop.Children.Clear();
                var xyknow = new XYKnowldge();
                xyknow.LoadDiasses(currentDisease);
                pop.Children.Add(xyknow);
                s2.Duration = TimeSpan.FromMilliseconds(0);

                dnxian.Visibility = Visibility.Visible;
                xzxian.Visibility = Visibility.Visible;
                szxian.Visibility = Visibility.Visible;
                xgxian.Visibility = Visibility.Visible;
            };

            s1.Duration = TimeSpan.FromSeconds(8.700);
            s1.Begin();

        }

        private void dian_png_Click(object sender, RoutedEventArgs e)
        {
            currentXian = dnxian;
            currentDisease = "DN";
            s2.Begin();
        }

        private void xzbutton_Click(object sender, RoutedEventArgs e)
        {
            currentXian = xzxian;
            currentDisease = "XZ";
            s2.Begin();

        }

        private void xgbutton_Click(object sender, RoutedEventArgs e)
        {
            currentXian = xgxian;
            currentDisease = "XG";
            s2.Begin();

        }

        private void szbutton_Click(object sender, RoutedEventArgs e)
        {
            currentXian = szxian;
            currentDisease = "SZ";
            s2.Begin();
        }

        private void ShowXian(FrameworkElement xian)
        {
            var xians = new Image[] { dnxian, xzxian, szxian, xgxian };
            foreach (var item in xians)
            {
                if (xian != item)
                {
                    DoubleAnimation da = new DoubleAnimation(0, TimeSpan.FromMilliseconds(300));
                    item.BeginAnimation(Image.OpacityProperty, da);
                }
                else
                {
                    DoubleAnimation da = new DoubleAnimation(1, TimeSpan.FromMilliseconds(300));
                    item.BeginAnimation(Image.OpacityProperty, da);
                }
            }


        }


    }
}
