using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using System.Diagnostics;

namespace MultiChartDemo
{

    public class MultiChartDataModel : ViewModelBase
    {

        #region Ctor

        //List<SalesPerformance> xueyaSalesData = new List<SalesPerformance>();
        //List<SalesPerformance> xuetangSalesData = new List<SalesPerformance>();
        //List<SalesPerformance> xuezhiSalesData = new List<SalesPerformance>();

        public MultiChartDataModel()
        {   
            
        }

        #endregion

        #region Data to bind

        //public List<SalesPerformance> SalesData
        //{
        //    get { return base.GetValue(() => this.SalesData); }
        //    set { base.SetValue(() => this.SalesData, value); }
        //}

        public List<SalesPerformance> xueyaSalesData
        {
            get { return base.GetValue(() => this.xueyaSalesData); }
            set { base.SetValue(() => this.xueyaSalesData, value); }
        }

        public List<SalesPerformance> xuetangSalesData
        {
            get { return base.GetValue(() => this.xuetangSalesData); }
            set { base.SetValue(() => this.xuetangSalesData, value); }
        }

        public List<SalesPerformance> xuezhiSalesData
        {
            get { return base.GetValue(() => this.xuezhiSalesData); }
            set { base.SetValue(() => this.xuezhiSalesData, value); }
        }

        public void setChartData(List value) {


        }

        #endregion
 
    }

}
