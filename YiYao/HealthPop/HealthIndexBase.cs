using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YiYao
{
    public class HealthIndexBase : UserControl
    {
        public TranslateTransform TranslateTransform;
        public ScaleTransform ScaleTransform;
        public BitmapSource ExpandBitmap;
        public HealthIndexBase()
        {
            this.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            TranslateTransform = new TranslateTransform();
            ScaleTransform = new ScaleTransform();
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(ScaleTransform);
            transformGroup.Children.Add(TranslateTransform);
            this.RenderTransform = transformGroup;

            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://application:,,,/HealthPop/expand.png", UriKind.Absolute);
            bitmap.EndInit();
            ExpandBitmap = bitmap;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;
            drawingContext.DrawImage(ExpandBitmap, new System.Windows.Rect(this.Width * 0.9, 50, ExpandBitmap.PixelWidth , ExpandBitmap.PixelHeight));
        }

        protected override void OnPreviewTouchDown(TouchEventArgs e)
        {
            base.OnPreviewTouchDown(e);
            var expandRect = new System.Windows.Rect(this.Width * 0.9, 50, ExpandBitmap.PixelWidth, ExpandBitmap.PixelHeight);
            if (expandRect.Contains(e.GetTouchPoint(this).Position))
            {
                this.IsManipulationEnabled = !this.IsManipulationEnabled;
                if (IsManipulationEnabled)
                {
                    BeginTransform();
                }
                else
                {
                    EndTransform();
                }
                e.Handled = true;
            }
        }


        public virtual void BeginTransform()
        {
            var xian = FindName("xian") as Image;

            Storyboard sb = new Storyboard();
            TimeSpan duration = TimeSpan.FromMilliseconds(300);
            DoubleAnimation scalex = new DoubleAnimation(1.5, duration);
            DoubleAnimation scaley = new DoubleAnimation(1.5, duration);
            DoubleAnimation xianOpacity = new DoubleAnimation(0, duration);

            sb.Children.Add(scalex);
            sb.Children.Add(scaley);
            sb.Children.Add(xianOpacity);

            Storyboard.SetTarget(scalex, this);
            Storyboard.SetTarget(scaley, this);
            Storyboard.SetTarget(xianOpacity, xian);

            Storyboard.SetTargetProperty(scalex, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            Storyboard.SetTargetProperty(scaley, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
            Storyboard.SetTargetProperty(xianOpacity, new PropertyPath("Opacity"));

            sb.Completed += (s, e) =>
            {
                ScaleTransform.ScaleX = scalex.To.Value;
                ScaleTransform.ScaleY = scaley.To.Value;
                xian.Opacity = xianOpacity.To.Value;
            };
            sb.FillBehavior = FillBehavior.Stop;
            sb.Begin();
        
        }

        public virtual void EndTransform()
        {
            var xian = FindName("xian") as Image;
            Storyboard sb = new Storyboard();
            TimeSpan duration = TimeSpan.FromMilliseconds(300);
            DoubleAnimation scalex = new DoubleAnimation(1, duration);
            DoubleAnimation scaley = new DoubleAnimation(1, duration);
            DoubleAnimation translatex = new DoubleAnimation(0, duration);
            DoubleAnimation translatey = new DoubleAnimation(0, duration);
            DoubleAnimation xianOpacity = new DoubleAnimation(1, duration);

            sb.Children.Add(scalex);
            sb.Children.Add(scaley);
            sb.Children.Add(translatex);
            sb.Children.Add(translatey);
            sb.Children.Add(xianOpacity);

            Storyboard.SetTarget(scalex, this);
            Storyboard.SetTarget(scaley, this);
            Storyboard.SetTarget(translatex, this);
            Storyboard.SetTarget(translatey, this);
            Storyboard.SetTarget(xianOpacity, xian);

            Storyboard.SetTargetProperty(scalex, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            Storyboard.SetTargetProperty(scaley, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
            Storyboard.SetTargetProperty(translatex, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.X)"));
            Storyboard.SetTargetProperty(translatey, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.Y)"));
            Storyboard.SetTargetProperty(xianOpacity, new PropertyPath("Opacity"));

            sb.Completed += (s, e) =>
             {
                 ScaleTransform.ScaleX = 1;
                 ScaleTransform.ScaleY = 1;
                 TranslateTransform.X = 0;
                 TranslateTransform.Y = 0;
                 xian.Opacity = 1;
             };
            sb.FillBehavior = FillBehavior.Stop;
            sb.Begin();

        }


    }


}
