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
        
        public Know()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
             {
                 FindStoryboard();

                 var xyknow = new XYKnowldge();
              //   root.Children.Add(xyknow);
                 Window.GetWindow(this).KeyDown += (ss, ee) =>
                 {
                     Canvas.SetLeft(xyknow.k1, GVar.x);
                     Canvas.SetTop(xyknow.k1, GVar.y);
                 };
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
                 pop.Children.Add(xyknow);
                 s2.Duration = TimeSpan.FromMilliseconds(0);
             };

            s1.Duration = TimeSpan.FromSeconds(8.700);
            s1.Begin();

        }

        private void dian_png_Click(object sender, RoutedEventArgs e)
        {
            currentXian = dnxian;
            s2.Begin();
        }

        private void xzbutton_Click(object sender, RoutedEventArgs e)
        {
            currentXian = xzxian;
            s2.Begin();

        }

        private void xgbutton_Click(object sender, RoutedEventArgs e)
        {
            currentXian = xgxian;
            s2.Begin();
           
        }

        private void szbutton_Click(object sender, RoutedEventArgs e)
        {
            currentXian = szxian;
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
