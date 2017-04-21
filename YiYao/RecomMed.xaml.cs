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
    /// RecomMed.xaml 的交互逻辑
    /// </summary>
    public partial class RecomMed : UserControl, INavigable
    {

        int demoImageCount = 5;

        public RecomMed()
        {
            InitializeComponent();

            this.add_west_chinese_images();
            this.add_west_HCP_images();
            this.add_west_med_images();
        }

        public void Start(object args)
        {

        }

        public void Stop()
        {

        }

        private void add_west_med_images() {
            
        }

        private void add_west_chinese_images()
        {
            for (int i = 0; i < 5; i++)
            { 
                Image med_image = new Image();

                
            }
        }

        private void add_west_HCP_images()
        {

        }

        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(ShoppingCar));
        }

    }
}
