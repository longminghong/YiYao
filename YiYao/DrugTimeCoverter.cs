using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WebService;

namespace YiYao
{
    public class DrugTimeCoverter : IValueConverter
    {   
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {   
            Drugtime sourceData = (Drugtime)value;
            string resultValue = String.Empty;

            if (String.Equals("yes", sourceData.time1)) {

                resultValue += "晨起早餐前 ;";
            } if (String.Equals("yes", sourceData.time2))
            {
                resultValue += "早餐后 ;";
            }
             if (String.Equals("yes", sourceData.time3))
            {
                resultValue += "午餐后 ;";
            }
             if (String.Equals("yes", sourceData.time4))
            {
                resultValue += "晚餐后 ;";
            }
             if (String.Equals("yes", sourceData.time5))
            {
                resultValue += "睡前 ;";
            }

            return resultValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
