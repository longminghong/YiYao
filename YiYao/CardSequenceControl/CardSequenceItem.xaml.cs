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
    /// Interaction logic for CardSequenceItem.xaml
    /// </summary>
    public partial class CardSequenceItem : UserControl
    {
        public CardSequenceItem()
        {
            InitializeComponent();
        }

        public void Select()
        {
            b2.Visibility = Visibility.Visible;
            b1.Visibility = Visibility.Collapsed;
        }

        public void UnSelect()
        {
            b2.Visibility = Visibility.Collapsed;
            b1.Visibility = Visibility.Visible;
        }


    }
}
