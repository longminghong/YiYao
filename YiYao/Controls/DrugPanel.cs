using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace YiYao
{
    public class DrugPanel : Canvas
    {
        private ScaleTransform mScaleTransform;
        private TranslateTransform mTranslateTransform;
        private bool mExpand;
        private DateTime mClickExpaired = DateTime.Now;
        private int mClickThreshold = 80;
        private int currentZIndex;
        public DrugPanel()
        {
            TransformGroup trans = new TransformGroup();
            mTranslateTransform = new TranslateTransform();
            mScaleTransform = new ScaleTransform();
            trans.Children.Add(mScaleTransform);
            trans.Children.Add(mTranslateTransform);
            this.RenderTransform = trans;
            currentZIndex = GetZIndex(this);
        }

        protected override void OnManipulationStarting(ManipulationStartingEventArgs e)
        {
            base.OnManipulationStarting(e);
            e.ManipulationContainer = (e.OriginalSource as FrameworkElement).Parent as FrameworkElement;
        }

        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            base.OnManipulationDelta(e);
            
            var delta = e.DeltaManipulation.Translation;
            mTranslateTransform.X += delta.X;
            mTranslateTransform.Y += delta.Y;
        }

        protected override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);
            mClickExpaired = DateTime.Now.AddMilliseconds(mClickThreshold);
        }

        protected override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);
            
            if (DateTime.Now < mClickExpaired)
            {
                mExpand = !mExpand;
                if (mExpand)
                {
                    BeginTransform();
                }
                else
                {
                    EndTransform();
                }
                IsManipulationEnabled = mExpand;
            }
           
        }



        public  void BeginTransform()
        {

            Storyboard sb = new Storyboard();
            TimeSpan duration = TimeSpan.FromMilliseconds(300);
            DoubleAnimation scalex = new DoubleAnimation(1.5, duration);
            DoubleAnimation scaley = new DoubleAnimation(1.5, duration);

            sb.Children.Add(scalex);
            sb.Children.Add(scaley);

            Storyboard.SetTarget(scalex, this);
            Storyboard.SetTarget(scaley, this);

            Storyboard.SetTargetProperty(scalex, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            Storyboard.SetTargetProperty(scaley, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));

            sb.Completed += (s, e) =>
            {
                mScaleTransform.ScaleX = scalex.To.Value;
                mScaleTransform.ScaleY = scaley.To.Value;
            };
            sb.FillBehavior = FillBehavior.Stop;

            var zindex = GetZIndex(this);

            SetZIndex(this, 10);
            sb.Begin();

        }

        public  void EndTransform()
        {
            Storyboard sb = new Storyboard();
            TimeSpan duration = TimeSpan.FromMilliseconds(300);
            DoubleAnimation scalex = new DoubleAnimation(1, duration);
            DoubleAnimation scaley = new DoubleAnimation(1, duration);
            DoubleAnimation translatex = new DoubleAnimation(0, duration);
            DoubleAnimation translatey = new DoubleAnimation(0, duration);

            sb.Children.Add(scalex);
            sb.Children.Add(scaley);
            sb.Children.Add(translatex);
            sb.Children.Add(translatey);

            Storyboard.SetTarget(scalex, this);
            Storyboard.SetTarget(scaley, this);
            Storyboard.SetTarget(translatex, this);
            Storyboard.SetTarget(translatey, this);

            Storyboard.SetTargetProperty(scalex, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            Storyboard.SetTargetProperty(scaley, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
            Storyboard.SetTargetProperty(translatex, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.X)"));
            Storyboard.SetTargetProperty(translatey, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.Y)"));

            sb.Completed += (s, e) =>
            {
                mScaleTransform.ScaleX = 1;
                mScaleTransform.ScaleY = 1;
                mTranslateTransform.X = 0;
                mTranslateTransform.Y = 0;
            };
            sb.FillBehavior = FillBehavior.Stop;
            SetZIndex(this, currentZIndex);
            sb.Begin();

        }

    }
}
