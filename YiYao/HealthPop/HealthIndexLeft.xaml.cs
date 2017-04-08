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
    /// Interaction logic for HealthIndexLeft.xaml
    /// </summary>
    public partial class HealthIndexLeft : UserControl,IHealthLeft
    {
        public HealthIndexLeft()
        {
            InitializeComponent();
        }

        public void Show()
        {
            (FindResource("Storyboard1") as Storyboard).Begin();
        }
    }

    public interface IHealthLeft
    {

    }
}
