using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService;
using System.Windows.Data;
using System.Globalization;

namespace YiYao
{
    public class DrugUsedCoverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Useddosage sourceData = (Useddosage)value;
            string resultValue = String.Empty;
            resultValue = "用药用量：";

            resultValue += sourceData.day+="天 ";

            resultValue += sourceData.time += "次 ";

            resultValue += " 每次  ";

            resultValue += sourceData.dosage;

            return resultValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DrugRegularCovert : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string sourceData = (string)value;
            string resultValue = String.Empty;
            resultValue = "否";
            if (String.Equals("yes", sourceData)) {
                resultValue = "是";
            }

            return resultValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DrugAllergicdrugCovert : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Allergicdrug[] sourceData = (Allergicdrug[])value;
            string resultValue = String.Empty;

            foreach (Allergicdrug item in sourceData)
            {
                resultValue += item.drugname;
            }

            return resultValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
