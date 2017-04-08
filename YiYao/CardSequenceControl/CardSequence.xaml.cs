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

namespace YiYao
{
    /// <summary>
    /// Interaction logic for CardSequence.xaml
    /// </summary>
    public partial class CardSequence : UserControl
    {
        public static readonly DependencyProperty GapProperty = DependencyProperty.Register("Gap", typeof(int), typeof(CardSequence), new PropertyMetadata(10, new PropertyChangedCallback(OnGapChanged)));

        public int Gap
        {
            get { return (int)GetValue(GapProperty); }
            set
            {
                SetValue(GapProperty, value);
            }
        }

        private static void OnGapChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = d as CardSequence;
            self.DoLayout();
        }

        IEnumerable<CardSequenceItem> mCards;
        int mCardWidth = 158;
        TranslateTransform mTranslate;
        bool mIsClick;
        public CardSequence()
        {
            InitializeComponent();
            this.Loaded += CardSequence_Loaded;
            
        }

        private void CardSequence_Loaded(object sender, RoutedEventArgs e)
        {
            mTranslate = (TranslateTransform)(container.RenderTransform as TransformGroup).Children[3];

            DoLayout();
        }

        private void DoLayout()
        {
            mCards = container.Children.OfType<CardSequenceItem>();
            int x = 0;
            foreach (var card in mCards)
            {
                Canvas.SetLeft(card, x);
                x = x + Gap + mCardWidth;
            }
        }

        private void container_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = root;
        }


        private void container_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            mTranslate.X += e.DeltaManipulation.Translation.X;
            if(e.IsInertial)
            {
                bool shoudStop = e.Velocities.LinearVelocity.X < 0 &&  mTranslate.X < -(mCards.Count() - 2) * (mCardWidth + Gap);
                shoudStop |= e.Velocities.LinearVelocity.X > 0 && mTranslate.X > mCardWidth * 2;
                if(shoudStop)
                {
                    e.Complete();
                }
            }
            
        }

        private void container_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            mIsClick = Math.Abs( e.TotalManipulation.Translation.X )< 5;
          
            if (mIsClick)
            {
                HitTest(p);
            }
        }

        private void container_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
           
        }

        private void container_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            if(mTranslate.X < - (mCards.Count() - 2) * (mCardWidth +Gap))
            {
                e.TranslationBehavior = new InertiaTranslationBehavior
                {
                    InitialVelocity = new Vector(0.8,0) ,
                    DesiredDisplacement =2 * mCardWidth,
                };
            }
            else if(mTranslate.X > mCardWidth * 2)
            {
                e.TranslationBehavior = new InertiaTranslationBehavior
                {
                    InitialVelocity = new Vector(-0.8, 0),
                    DesiredDisplacement = 2 * mCardWidth,
                };
            }
            else
            {
                e.TranslationBehavior = new InertiaTranslationBehavior
                {
                    InitialVelocity = e.InitialVelocities.LinearVelocity,
                    DesiredDeceleration = 0.01
                };
            }

            
        }


        private void HitTest(Point p)
        {
            int selectIndex = -1;
            for (int i = mCards.Count() - 1;i >=0;i--)
            {
                if(i * (mCardWidth + Gap)  + mTranslate.X  < p.X)
                {
                    selectIndex = i;
                    break;
                }
            }
            if(selectIndex >=0)
            {
                for (int i = mCards.Count() - 1; i >= 0; i--)
                {
                    if(i == selectIndex)
                    {
                        mCards.ElementAt(i).Select();
                    }
                    else
                    {
                        mCards.ElementAt(i).UnSelect();
                    }
                }
                AnimateTo(selectIndex);
            }
            Debug.WriteLine(selectIndex);
        }

        private void AnimateTo(int selectIndex)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = TimeSpan.FromMilliseconds(300);
            da.To = -selectIndex * (mCardWidth + Gap) + 365;
            da.FillBehavior = FillBehavior.Stop;
            da.Completed += (s, e) =>
             {
                 mTranslate.X = da.To.Value;
             };
            mTranslate.BeginAnimation(TranslateTransform.XProperty, da);
        }

        Point p;
        protected override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);
            p = e.GetTouchPoint(root).Position;
        }



    }
}
