using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace YiYao
{
    /// <summary>
    /// Interaction logic for CircelButton.xaml
    /// </summary>
    public partial class CircelButton : UserControl
    {
        public bool IsSelected;
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(CircelButton));

        public ImageSource Source
        {
            get
            {
                return (ImageSource)GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }

        public static readonly DependencyProperty ContentPaddingProperty = DependencyProperty.Register("ContentPadding", typeof(Thickness), typeof(CircelButton));

        public Thickness ContentPadding
        {
            get
            {
                return (Thickness)GetValue(ContentPaddingProperty);
            }
            set
            {
                SetValue(ContentPaddingProperty, value);
            }
        }

        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register("Angle", typeof(int), typeof(CircelButton));

        public int Angle
        {
            get
            {
                return (int)GetValue(AngleProperty);
            }
            set
            {
                SetValue(AngleProperty, value);
            }
        }

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CircelButton));

        // Provide CLR accessors for the event
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }


        public CircelButton()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            RaiseEvent(new RoutedEventArgs(ClickEvent));
            (FindResource("select") as Storyboard).Begin();
            IsSelected = true;
        }

        public void UnSelect()
        {
            IsSelected = false;
            (FindResource("unselect") as Storyboard).Begin();
        }

        private void hitRegion_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
