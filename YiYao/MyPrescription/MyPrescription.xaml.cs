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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YiYao
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MyPrescription : UserControl
    {
        public MyPrescription()
        {
            InitializeComponent();
        }

        private void videoControlButton_Click(object sender, RoutedEventArgs e)
        {
            shiping_png.Play();
        }

        private void videoControlStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (shiping_png.CanPause)
            {
                shiping_png.Pause();
            }
        }

        private void shiping_png_Loaded(object sender, RoutedEventArgs e)
        {
            //shiping_png.Position = new TimeSpan(1000);
            //shiping_png.Play();
            //shiping_png.Pause();
        }
    }
}
