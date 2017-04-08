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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ScanDrug : UserControl
    {
        const int Scan = 1;
        const int Push = 2;
        string drugId;
        Storyboard scan2;
        Storyboard scan1;
        Storyboard scan3;
        Storyboard scan4;
        Storyboard scan5;
        int state = Scan;
        
        public ScanDrug()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
             {
                 Window.GetWindow(this).TextInput += ScanDrug_TextInput;
                 drugId = "";
                 FindStoryboard();

             };
            this.Unloaded += (s, e) =>
             {

             };
        }


        private void FindStoryboard()
        {
            scan1 = FindResource("Scan1") as Storyboard;
            scan2 = FindResource("Scan2") as Storyboard;
            scan3 = FindResource("Scan3") as Storyboard;

            scan5 = FindResource("Scan5") as Storyboard;

            scan1.Duration = TimeSpan.Parse("0:0:2.4");

            scan2.BeginTime = TimeSpan.FromSeconds(-3);
            scan2.Duration = TimeSpan.FromSeconds(7);

            scan3.BeginTime = TimeSpan.FromSeconds(-7);
            scan3.Duration = TimeSpan.FromSeconds(9);

            scan5.BeginTime = TimeSpan.FromSeconds(-14);
            scan5.Duration = TimeSpan.FromSeconds(20);

            scan1.Completed += (s, e) =>
            {
                scan2.Begin();
            };
            scan2.Completed += (s, e) =>
            {
                scan2.Begin();
            };
            scan3.Completed += (s, e) =>
             {
                 scan5.Begin();
             };

             scan1.Begin();
            //scan2.Begin();
            //scan3.Begin();
            // scan5.Begin();
           // scan4.Begin();
        }

        private void ScanDrug_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "\r")
                DoWork();
            drugId += e.Text;
        }


        private void DoWork()
        {
            if (state == Scan)
            {
                if(drugId ==  "6950641900181")
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri("pack://application:,,,/Images/ScanDrug/yao2.png", UriKind.Absolute);
                    bitmap.EndInit();
                    yao.Source = bitmap;
                }
                scan2.Stop();
                scan3.Begin();
                state++;
            }
        }

        protected override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);
            PointerDown();
        }


        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            PointerDown();
        }

        private void PointerDown()
        {
            if (state == Scan)
            {
                scan2.Stop();
                scan3.Begin();
                state++;
            }
           
        }
    }

}
