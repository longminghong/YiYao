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
    public partial class QuanButton : Grid
    {
        public bool IsSelected;
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(QuanButton));

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

        public static readonly DependencyProperty ContentPaddingProperty = DependencyProperty.Register("ContentPadding", typeof(Thickness), typeof(QuanButton));

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

        public static readonly DependencyProperty TitleSourceProperty = DependencyProperty.Register("TitleSource", typeof(ImageSource), typeof(QuanButton));

        public ImageSource TitleSource
        {
            get
            {
                return (ImageSource)GetValue(TitleSourceProperty);
            }
            set
            {
                SetValue(TitleSourceProperty, value);
            }
        }

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(QuanButton));

        // Provide CLR accessors for the event
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }


        public QuanButton()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            IsSelected = !IsSelected;
            RaiseEvent(new RoutedEventArgs(ClickEvent));

            if (IsSelected)
            {
                (FindResource("select") as Storyboard).Begin();
            }
            else
            {
                (FindResource("unselect") as Storyboard).Begin();
            }
        }

        public void Select()
        {
            IsSelected = true;
            (FindResource("select") as Storyboard).Begin();
        }

        public void UnSelect()
        {
            IsSelected = false;
            (FindResource("unselect") as Storyboard).Begin();
        }

    }
}
