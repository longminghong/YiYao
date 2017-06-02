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

namespace MultiChartDemo
{
    public class SeriesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SalesTemplate { get; set; }
        public DataTemplate MedianTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {

            if (item is SalesPerformance)
            {
                SalesPerformance salesPerf = item as SalesPerformance;

                if (salesPerf.SalesName == "Median")
                {
                    return MedianTemplate;
                }
                else
                {
                    return SalesTemplate;
                }
            }

            // default
            return null;

        }
    }
}
