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
    /// Interaction logic for ImageButton.xaml
    /// </summary>
    public partial class ImageButton : Button
    {

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(BitmapSource), typeof(ImageButton));

      
        public BitmapSource Source
        {
            get
            {
                return ((BitmapSource)(base.GetValue(ImageButton.SourceProperty)));
            }
            set
            {
                base.SetValue(ImageButton.SourceProperty, value);
            }
        }

        public ImageButton()
        {
            InitializeComponent();
        }

        protected override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);
            (FindResource("ZoomIn") as Storyboard).Begin();
        }

        protected override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);
            (FindResource("ZoomOut") as Storyboard).Begin();
        }
    }
}
