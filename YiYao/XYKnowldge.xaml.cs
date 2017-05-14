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
    /// Interaction logic for XYKnowldge.xaml
    /// </summary>
    public partial class XYKnowldge : UserControl
    {
        List<String> buttonContent;
        public XYKnowldge()
        {
            InitializeComponent();
            buttons.ManipulationBoundaryFeedback += (s, e) =>
            {
                e.Handled = true;
            };
            this.Loaded += (s, e) =>
            {
                buttons.AddHandler(Button.ClickEvent, new RoutedEventHandler(OnKnowldgeClick));


            };
        }

        public void LoadDiasses(string disease)
        {
            if (disease == "XG")
            {
                buttonContent = new List<string> { "1.「高血脂症」和「血管硬化」有什么相关呢.png",
                                                "10.高血脂与心脏的关系(一) .png",
                                                "11.高血脂症日常生活照顾原则 .png",
                                                "12.高血脂症-生活饮食原则.png",
                                                "13.降低胆固醇的生活饮食原则.png",
                                                "14.降低甘油三酯的饮食原则.png",
                                                "15.降血脂要先保肝.png",
                                                "16.哪些原因会造成高血脂 .png",
                                                "18.让您远离高血脂-饮食控制.png",
                                                "19.如何降血液内脂肪.png",
                                                "2.高血脂档案.png",
                                                "20.什么是高血脂.png",
                                                "21.远离高血脂的方式.png",
                                                "22.血脂异常能运动吗 什么运动好.png",
                                                "3.高血脂的饮食禁忌.png",
                                                "4.高血脂的运动养身.png",
                                                "5.高血脂会造成哪些危险.png",
                                                "6.高血脂如何产生.png",
                                                "7.高血脂症-生活运动原则.png",
                                                "8.高血脂饮食宜忌.png",
                                                "9.高血脂与心脏的关系(二) .png",};

            }
            else if (disease == "SZ")
            {
                buttonContent = new List<string> { "37.慢性肾病的十大高危群.png", "38.慢性肾脏病饮食篇：清淡烹调.png", "39.慢性肾脏病饮食篇_适当减盐.png", "44.肾脏的健康宝典.png", };

            }
            else if (disease == "XZ")
            {
                buttonContent = new List<string> {  "52.什么是冠心病（一）.png"
                                                ,"54.冠心病的高危人群.png"
                                                ,"55.容易引发冠心病的行为与环境.png"
                                                ,"56.冠心病发作.png"
                                                ,"58.冠心病的活动金字塔.png"
                                                ,"60.气温骤降，慢性病患者防护心血管疾病并发症.png"
                                                ,"61.气温骤降时注意心脏病发作症状.png"
                                                ,"62. 均衡的饮食 冠心病的饮食原则1.png"
                                                ,"63.控制“钠”摄取量 冠心病的饮食原则2.png"
                                                ,"64.胆固醇摄取量冠心病的饮食原则3.png"
                                                ,"65.采取『低盐饮食』烹调方法冠心病的饮食原则4.png"};

            }

            else if (disease == "DN")
            {
                buttonContent = new List<string> { "17.脑中风的预防 .png",
                                                "40.您知道脑中风吗.png",
                                                "45.试试以下的活动帮助你放松心情 .png",
                                                "53.你以后得心脑血管病的风险大不大.png",
                                                "57.健康就是这样被您吃掉的.png",
                                                "59.气温骤降，慢性病患者如何照护自己 .png",
                                                 };

            }
            mycontrol.ItemsSource = buttonContent;
        }

        private void OnKnowldgeClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = (e.OriginalSource as Button).Tag as string;
                string imagePath = "pack://application:,,,/Images/Reading/" + name;
                Uri imageURI = new Uri(imagePath, UriKind.Absolute);
                BitmapImage bitmap = new BitmapImage(imageURI);
                popimage.Source = bitmap;


                Storyboard show = new Storyboard();

                DoubleAnimation d1 = new DoubleAnimation(1, TimeSpan.FromMilliseconds(300));
                show.Children.Add(d1);
                Storyboard.SetTarget(d1, pop);
                Storyboard.SetTargetProperty(d1, new PropertyPath("Opacity"));

                ObjectAnimationUsingKeyFrames d2 = new ObjectAnimationUsingKeyFrames();
                ObjectKeyFrame k1 = new DiscreteObjectKeyFrame
                {
                    KeyTime = TimeSpan.FromMilliseconds(0),
                    Value = Visibility.Visible
                };
                d2.KeyFrames.Add(k1);
                show.Children.Add(d2);
                Storyboard.SetTarget(d2, pop);
                Storyboard.SetTargetProperty(d2, new PropertyPath("Visibility"));
                show.Begin();
            }
            catch (Exception)
            {


            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Storyboard show = new Storyboard();

            DoubleAnimation d1 = new DoubleAnimation(0, TimeSpan.FromMilliseconds(300));
            show.Children.Add(d1);
            Storyboard.SetTarget(d1, pop);
            Storyboard.SetTargetProperty(d1, new PropertyPath("Opacity"));

            ObjectAnimationUsingKeyFrames d2 = new ObjectAnimationUsingKeyFrames();
            ObjectKeyFrame k1 = new DiscreteObjectKeyFrame
            {
                KeyTime = TimeSpan.FromMilliseconds(300),
                Value = Visibility.Collapsed
            };
            d2.KeyFrames.Add(k1);
            show.Children.Add(d2);
            Storyboard.SetTarget(d2, pop);
            Storyboard.SetTargetProperty(d2, new PropertyPath("Visibility"));
            show.Begin();
        }

        protected override void OnManipulationStarting(ManipulationStartingEventArgs e)
        {
            base.OnManipulationStarting(e);
            e.ManipulationContainer = this;
        }

        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            base.OnManipulationDelta(e);
            var healthPop = e.OriginalSource as FrameworkElement;
            if (healthPop is Button)
            {
                e.Cancel();
                return;
            }

            var transformGroup = healthPop.RenderTransform as TransformGroup;
            var translate = transformGroup.Children[1] as TranslateTransform;
            translate.X += e.DeltaManipulation.Translation.X;
            translate.Y += e.DeltaManipulation.Translation.Y;
            var scale = transformGroup.Children[0] as ScaleTransform;
            scale.ScaleX *= e.DeltaManipulation.Scale.X;
            scale.ScaleY *= e.DeltaManipulation.Scale.Y;

        }
    }
}
