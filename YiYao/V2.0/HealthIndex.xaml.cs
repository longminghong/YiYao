using Microsoft.Practices.ServiceLocation;
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
using WebService;
using YiYao.Util;

namespace YiYao
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class HealthIndex : UserControl
    {
        Storyboard s1;
        bool mInitlized;
        HealthDataService mHealthDataService;
        
        public HealthIndex()
        {
            InitializeComponent();
            this.Loaded += async (s, e) =>
             {
                 mInitlized = false;
                 FindStoryboard();
                 mHealthDataService = ServiceLocator.Current.GetInstance<HealthDataService>();
                var data = await mHealthDataService.GetHealthDataAsync();
             };
        }


        private void FindStoryboard()
        {
            s1 = FindResource("Storyboard1") as Storyboard;
     

            s1.Duration = TimeSpan.FromSeconds(4.9);

            s1.Completed += (s, e) =>
             {
                 mInitlized = true;
             };
            s1.Begin();
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            e.Handled = !mInitlized; 
        }

        protected override void OnPreviewTouchDown(TouchEventArgs e)
        {
            base.OnPreviewTouchDown(e);
            e.Handled = !mInitlized;
        }

      

        private void onMBClick(object sender, RoutedEventArgs e)
        {
            var mb = leftPart.Children.OfType<MBHealthIndex>().FirstOrDefault();
            if (mb == null)
            {
                mb = new MBHealthIndex();
                mb.LoadData();
                Canvas.SetLeft(mb, 95);
                Canvas.SetTop(mb, 349);
                leftPart.Children.Add(mb);
            }
            else
            {
                leftPart.Children.Remove(mb);
            }
        }
        /// <summary>
        /// 历史用药曲线图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onLiCiClick(object sender, RoutedEventArgs e)
        {
            var lici = leftPart.Children.OfType<LiCiHealthIndex>().FirstOrDefault();
            if(lici == null)
            {
                var mb = new LiCiHealthIndex();
                mb.LoadData();
                Canvas.SetLeft(mb, 97);
                Canvas.SetTop(mb, 600);
                leftPart.Children.Add(mb);
            }
            else
            {
                leftPart.Children.Remove(lici);
            }
        }

        private void onShangCiClick(object sender, RoutedEventArgs e)
        {

            var shangci = leftPart.Children.OfType<ShangCiHealthIndex>().FirstOrDefault();
            if (shangci == null)
            {
                var mb = new ShangCiHealthIndex();
                mb.LoadData();
                Canvas.SetLeft(mb, 180);
                Canvas.SetTop(mb, 102);
                leftPart.Children.Add(mb);
            }
            else
            {
                leftPart.Children.Remove(shangci);
            }
        }
        /// <summary>
        /// 血糖曲线图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onXTClick(object sender, RoutedEventArgs e)
        {

            var xz = rightPart.Children.OfType<XTHealthIndex>().FirstOrDefault();
            if (xz == null)
            {
                var mb = new XTHealthIndex();
                mb.LoadData();
                Canvas.SetLeft(mb, 1299);
                Canvas.SetTop(mb, 320);
                rightPart.Children.Add(mb);
            }
            else
            {
                rightPart.Children.Remove(xz);
            }
        }
        /// <summary>
        /// 血脂曲线图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onXZClick(object sender, RoutedEventArgs e)
        {

            var xz = rightPart.Children.OfType<XZHealthIndex>().FirstOrDefault();
            if (xz == null)
            {
                var mb = new XZHealthIndex();
                mb.LoadData();
                Canvas.SetLeft(mb, 1345);
                Canvas.SetTop(mb, 560);
                rightPart.Children.Add(mb);
            }
            else
            {
                rightPart.Children.Remove(xz);
            }
        }
        /// <summary>
        /// 血压曲线图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onXYClick(object sender, RoutedEventArgs e)
        {
            var xy = rightPart.Children.OfType<XYHealthIndex>().FirstOrDefault();
            if (xy == null)
            {
                var mb = new XYHealthIndex();
                mb.LoadData();
                Canvas.SetLeft(mb, 1179);
                Canvas.SetTop(mb, 75);
                rightPart.Children.Add(mb);
            }
            else
            {
                rightPart.Children.Remove(xy);
            }
        }
        /// <summary>
        /// 心率曲线图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onXLClick(object sender, RoutedEventArgs e)
        {
            var xl = rightPart.Children.OfType<XLHealthIndex>().FirstOrDefault();
            if (xl == null)
            {
                var mb = new XLHealthIndex();
                mb.LoadData();
                Canvas.SetLeft(mb, 1184);
                Canvas.SetTop(mb, 812);
                rightPart.Children.Add(mb);
            }
            else
            {
                rightPart.Children.Remove(xl);
            }
        }

        private void centerBtn_Click(object sender, RoutedEventArgs e)
        {
            var quans = root.Children.OfType<QuanButton>();

            if (rightPart.Children.Count == 4 && leftPart.Children.Count == 3)
            {
                leftPart.Children.Clear();
                rightPart.Children.Clear();
                foreach (var quan in quans)
                {
                    quan.UnSelect();
                }
            }
            else
            {
                foreach (var quan in quans)
                {
                    if(!quan.IsSelected)
                    {
                        quan.RaiseEvent(new RoutedEventArgs(QuanButton.ClickEvent));
                        quan.Select();
                    }
                }
            }

        }

        protected override void OnManipulationStarting(ManipulationStartingEventArgs e)
        {
            base.OnManipulationStarting(e);
            e.ManipulationContainer = this;
        }

        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            base.OnManipulationDelta(e);
            var healthPop = e.OriginalSource as FrameworkElement;
            var transformGroup = healthPop.RenderTransform as TransformGroup;
            var translate = transformGroup.Children[1] as TranslateTransform;
            translate.X += e.DeltaManipulation.Translation.X;
            translate.Y += e.DeltaManipulation.Translation.Y;
            var scale = transformGroup.Children[0] as ScaleTransform;
            scale.ScaleX *= e.DeltaManipulation.Scale.X;
            scale.ScaleY *= e.DeltaManipulation.Scale.Y;
            
        }

     
    }
}
