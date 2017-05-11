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
using WebService;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using YiYao.Events;

namespace YiYao
{


    /// <summary>
    /// A7.xaml 的交互逻辑
    /// </summary>
    public partial class A7 : UserControl, INavigable
    {

        Dictionary<string, string> diseDictionary;

        List<String> disDataList;
        MTMDisDTO diseDTO;
        public A7()
        {
            InitializeComponent();
            diseDictionary = new Dictionary<string, string>();
            initData();
            disDataList = new List<String>();
        }

        public void Start(object args)
        {
            if (null != args)
            {
                mycontrol.ItemsSource = disDataList;
                //customInfo = (MTMCustInfo)args;
                // to do 数据绑定

                EventAggregator eventAggragator = ServiceLocator.Current.GetInstance<EventAggregator>();
                eventAggragator.GetEvent<WebSocketEvent>().Subscribe(OnWebSocketEvent);
            }
        }

        public void Stop()
        {
            EventAggregator eventAggragator = ServiceLocator.Current.GetInstance<EventAggregator>();
            eventAggragator.GetEvent<WebSocketEvent>().Unsubscribe(OnWebSocketEvent);
        }
        private void OnWebSocketEvent(object data)
        {
            disDataList.Clear();

            diseDTO = (MTMDisDTO)data;

            foreach (String diskey in diseDTO.diseasedata.disease1)
            {
                if (diseDictionary.ContainsKey(diskey))
                {
                    String value = diseDictionary[diskey];
                    disDataList.Add(value);
                }
            }
            foreach (String diskey in diseDTO.diseasedata.disease2)
            {
                if (diseDictionary.ContainsKey(diskey))
                {
                    String value = diseDictionary[diskey];
                    disDataList.Add(value);
                }
            }
            foreach (String diskey in diseDTO.diseasedata.disease3)
            {
                if (diseDictionary.ContainsKey(diskey))
                {
                    String value = diseDictionary[diskey];
                    disDataList.Add(value);
                }
            }
            foreach (String diskey in diseDTO.diseasedata.disease4)
            {
                if (diseDictionary.ContainsKey(diskey))
                {
                    String value = diseDictionary[diskey];
                    disDataList.Add(value);
                }
            }
            foreach (String diskey in diseDTO.diseasedata.disease6)
            {
                if (diseDictionary.ContainsKey(diskey))
                {
                    String value = diseDictionary[diskey];
                    disDataList.Add(value);
                }
            }
            foreach (String diskey in diseDTO.diseasedata.disease7)
            {
                if (diseDictionary.ContainsKey(diskey))
                {
                    String value = diseDictionary[diskey];
                    disDataList.Add(value);
                }
            }
            foreach (String diskey in diseDTO.diseasedata.disease8)
            {
                if (diseDictionary.ContainsKey(diskey))
                {
                    String value = diseDictionary[diskey];
                    disDataList.Add(value);
                }
            }
            foreach (String diskey in diseDTO.diseasedata.disease9)
            {
                if (diseDictionary.ContainsKey(diskey))
                {
                    String value = diseDictionary[diskey];
                    disDataList.Add(value);
                }
            }
            foreach (String diskey in diseDTO.diseasedata.disease10)
            {
                if (diseDictionary.ContainsKey(diskey))
                {
                    String value = diseDictionary[diskey];
                    disDataList.Add(value);
                }
            }
            foreach (String diskey in diseDTO.diseasedata.disease11)
            {
                if (diseDictionary.ContainsKey(diskey))
                {
                    String value = diseDictionary[diskey];
                    disDataList.Add(value);
                }
            }
            foreach (String diskey in diseDTO.diseasedata.disease12)
            {

                if (diseDictionary.ContainsKey(diskey))
                {
                    String value = diseDictionary[diskey];
                    disDataList.Add(value);
                }
            }
            foreach (String diskey in diseDTO.diseasedata.disease13)
            {
                if (diseDictionary.ContainsKey(diskey))
                {
                    String value = diseDictionary[diskey];
                    disDataList.Add(value);
                }
            }
            mycontrol.ItemsSource = disDataList;
            mycontrol.Items.Refresh();
            Console.WriteLine("A7 page finish.");

        }
        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(A8));
        }

        private void initData()
        {

            diseDictionary.Add("1", "早发心血管病家族史");
            diseDictionary.Add("E78.501", "高脂血症");
            diseDictionary.Add("E78.001", "高胆固醇血症");
            diseDictionary.Add("E78.151/78.053", "高甘油三酯血症");
            diseDictionary.Add("E78.203", "混合性高脂血症");
            diseDictionary.Add("E78.451", "家族性混合型高脂血症");
            diseDictionary.Add("3", "左心室肥厚");
            diseDictionary.Add("I65.202", "颈动脉狭窄");
            diseDictionary.Add("I65.201", "颈动脉硬化");
            diseDictionary.Add("5", "血肌酐轻度升高");
            diseDictionary.Add("I64.X04", "缺血性脑卒中");
            diseDictionary.Add("I69.452", "脑卒中后遗症");
            diseDictionary.Add("I61.902", "出血性脑卒中");
            diseDictionary.Add("I69.101", "脑出血后遗症");
            diseDictionary.Add("G45.901", "短暂性脑缺血发作");

            diseDictionary.Add("I74.306", "下肢动脉血栓形成");
            diseDictionary.Add("E14.503", "糖尿病性周围血管病变");
            diseDictionary.Add("I73.904", "其他周围血管病");
            diseDictionary.Add("I99.X03", "周围血管并发症");

            diseDictionary.Add("H35.952", "视网膜病变");
            diseDictionary.Add("H35.006", "视网膜病");
            diseDictionary.Add("E14.304+", "糖尿病性视网膜病");
            diseDictionary.Add("H35.001", "高血压性视网膜病");

            diseDictionary.Add("I20", "心绞痛");
            diseDictionary.Add("I25.551", "心肌缺血");
            diseDictionary.Add("I25.101", "冠心病");
            diseDictionary.Add("I25.652", "无症状性冠心病");
            diseDictionary.Add("E14.551", "糖尿病性冠心病");
            diseDictionary.Add("I11.901", "高血压性心脏病");
            diseDictionary.Add("I11.951", "高血压性心脏病不伴充血性心力衰竭");
            diseDictionary.Add("I50", "心力衰竭");
            diseDictionary.Add("I11.051", "高血压性心脏病伴充血性心力衰竭");
            diseDictionary.Add("I11.052", "高血压性心力衰竭");

            diseDictionary.Add("N03", "慢性肾炎综合征");
            diseDictionary.Add("N03.952", "慢性肾病NOS");
            diseDictionary.Add("N04.903", "肾病综合症");
            diseDictionary.Add("E14.203+", "糖尿病性肾病");
            diseDictionary.Add("N03.903", "慢性肾炎");
            diseDictionary.Add("I12.903", "高血压性肾病");
            diseDictionary.Add("I12.001", "高血压性肾衰竭");

            diseDictionary.Add("E10-E14", "糖尿病");

            //diseDictionary.Add(血糖监测,["血糖监测_20170120_1.jpg"]);
            //diseDictionary.Add(糖尿病食物金字塔,["糖尿病食物金字塔.jpg"]);
            //diseDictionary.Add(预防二型糖尿病,["预防第二型糖尿病_0119_3.jpg"]);
            //diseDictionary.Add(高血压的危险因子,["高血压的危險因子_20170118-2.jpg"]);

            diseDictionary.Add("E14.408+", "糖尿病性周围神经病");
            diseDictionary.Add("E14.503", "糖尿病性周围血管病变");
            diseDictionary.Add("E14.606", "糖尿病性足病");
            diseDictionary.Add("E14.304+", "糖尿病性视网膜病变");
            diseDictionary.Add("E14.551", "糖尿病性冠心病");
            diseDictionary.Add("E14.203", "糖尿病肾病");

            diseDictionary.Add("I10.X02", "高血压");
            diseDictionary.Add("I10.X11", "原发性高血压");
            diseDictionary.Add("I10-I15", "高血压病");

            //diseDictionary.Add(防止血压剧烈波动,["防止血压剧烈波动定稿_20170120_3.jpg"]);
            //diseDictionary.Add(高血压的危险因子,["高血压的危險因子_20170118-2.jpg"]);
            //diseDictionary.Add(高血压患者的生活保健1 - 3,["高血压患者的生活保健1_20170118-2.jpg""高血压患者的生活保健2_20170118-2.jpg""高血压患者的生活保健3_20170118-2.jpg"]);
            //diseDictionary.Add(量血压的注意事项,["量血压時可以注意以下6個小技巧_20170118-2.jpg"]);
            //diseDictionary.Add(预防二型糖尿病,["预防第二型糖尿病_0119_3.jpg"]);
            diseDictionary.Add("R73.002", "糖耐量异常");

        }
    }
}
