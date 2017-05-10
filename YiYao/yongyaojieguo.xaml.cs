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
    /// ShoppingCar.xaml 的交互逻辑
    /// </summary>
    public partial class yongyaojieguo : UserControl, INavigable
    {
        public yongyaojieguo()
        {
            //InitializeComponent();
        }
        public void Start(object args)
        {

        }

        public void Stop()
        {

        }

        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(Login));
        }
    }
}
