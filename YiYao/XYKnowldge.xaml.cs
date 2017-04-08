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

namespace YiYao
{
    /// <summary>
    /// Interaction logic for XYKnowldge.xaml
    /// </summary>
    public partial class XYKnowldge : UserControl
    {
        public XYKnowldge()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
             {
                 buttons.AddHandler(Button.ClickEvent, new RoutedEventHandler(OnKnowldgeClick));
             };
        }

        private void OnKnowldgeClick(object sender, RoutedEventArgs e)
        {
            Storyboard show = new Storyboard();
            
            DoubleAnimation d1 = new DoubleAnimation(1, TimeSpan.FromMilliseconds(300));
            show.Children.Add(d1);
            Storyboard.SetTarget(d1, pop);
            Storyboard.SetTargetProperty(d1, new PropertyPath("Opacity"));

            ObjectAnimationUsingKeyFrames d2 = new ObjectAnimationUsingKeyFrames();
            ObjectKeyFrame k1 = new DiscreteObjectKeyFrame
            {
                KeyTime = TimeSpan.FromMilliseconds(0),
                Value = Visibility.Visible
            };
            d2.KeyFrames.Add(k1);
            show.Children.Add(d2);
            Storyboard.SetTarget(d2, pop);
            Storyboard.SetTargetProperty(d2, new PropertyPath("Visibility"));
            show.Begin();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Storyboard show = new Storyboard();

            DoubleAnimation d1 = new DoubleAnimation(0, TimeSpan.FromMilliseconds(300));
            show.Children.Add(d1);
            Storyboard.SetTarget(d1, pop);
            Storyboard.SetTargetProperty(d1, new PropertyPath("Opacity"));

            ObjectAnimationUsingKeyFrames d2 = new ObjectAnimationUsingKeyFrames();
            ObjectKeyFrame k1 = new DiscreteObjectKeyFrame
            {
                KeyTime = TimeSpan.FromMilliseconds(300),
                Value = Visibility.Collapsed
            };
            d2.KeyFrames.Add(k1);
            show.Children.Add(d2);
            Storyboard.SetTarget(d2, pop);
            Storyboard.SetTargetProperty(d2, new PropertyPath("Visibility"));
            show.Begin();
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
            if (healthPop is Button)
            {
                e.Cancel();
                return;
            }

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
