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

    public class MainWindowViewModel : ViewModelBase
    {

        #region Ctor

        public MainWindowViewModel()
        {
            
            UpdateData();
        }

        #endregion

        #region Data to bind

        public List<SalesPerformance> SalesData
        {
            get { return base.GetValue(() => this.SalesData); }
            set { base.SetValue(() => this.SalesData, value); }
        }
        
        public void setChartData(List value) {


        }

        #endregion

        #region Private metohds
 

        private void UpdateData()
        {
            // Fill SalesData
            List<SalesPerformance> salesData = new List<SalesPerformance>();
            SalesPerformance salesPerf = new SalesPerformance();
            salesPerf.SalesName = "Miller";
            salesPerf.SalesTotals = new List<SalesInfo>();
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("01/31/2009").ToString("MMM"), SalesTotal = 10000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("02/28/2009").ToString("MMM"), SalesTotal = 12000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("03/31/2009").ToString("MMM"), SalesTotal = 14000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("04/28/2009").ToString("MMM"), SalesTotal = 15000 }); 
            salesData.Add(salesPerf);
            salesPerf = new SalesPerformance();
            salesPerf.SalesName = "Smith";
            salesPerf.SalesTotals = new List<SalesInfo>();
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("01/31/2009").ToString("MMM"), SalesTotal = 9000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("02/28/2009").ToString("MMM"), SalesTotal = 12000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("03/31/2009").ToString("MMM"), SalesTotal = 13000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("04/30/2009").ToString("MMM"), SalesTotal = 11000 }); 
            salesData.Add(salesPerf);
            salesPerf = new SalesPerformance();
            salesPerf.SalesName = "James";
            salesPerf.SalesTotals = new List<SalesInfo>();
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("01/31/2009").ToString("MMM"), SalesTotal = 17000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("02/28/2009").ToString("MMM"), SalesTotal = 16000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("03/31/2009").ToString("MMM"), SalesTotal = 18000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("04/30/2009").ToString("MMM"), SalesTotal = 19400 }); 
            salesData.Add(salesPerf);
            salesPerf = new SalesPerformance();
            salesPerf.SalesName = "Matthews";
            salesPerf.SalesTotals = new List<SalesInfo>();
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("01/31/2009").ToString("MMM"), SalesTotal = 11400 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("02/28/2009").ToString("MMM"), SalesTotal = 14500 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("03/31/2009").ToString("MMM"), SalesTotal = 13000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("04/30/2009").ToString("MMM"), SalesTotal = 17000 }); 
            salesData.Add(salesPerf);
            salesPerf = new SalesPerformance();
            salesPerf.SalesName = "Simpson";
            salesPerf.SalesTotals = new List<SalesInfo>();
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("01/31/2009").ToString("MMM"), SalesTotal = 18000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("02/28/2009").ToString("MMM"), SalesTotal = 17000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("03/31/2009").ToString("MMM"), SalesTotal = 14000 });
            salesPerf.SalesTotals.Add(new SalesInfo { Date = DateTime.Parse("04/30/2009").ToString("MMM"), SalesTotal = 15000 }); 
            salesData.Add(salesPerf);


            Random random = new Random();
            
            List<SalesPerformance> tmpData = new List<SalesPerformance>();
            int randomInt = 5;
            for (int i = 0; i < randomInt; i++)
            {
                tmpData.Add(salesData[i]);
            }
            tmpData = tmpData.Distinct().ToList();
            this.SalesData = tmpData;
 
           
            return;
        }

        #endregion

    }

}
