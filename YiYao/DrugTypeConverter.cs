using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace YiYao
{
    public class DrugTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string[] sourceData = (string[])value;
            string resultValue = String.Empty;

            resultValue = "药物类型 : ";
            /*
             * "stepdown",
                "Hypoglycemic",
                "Fat",
                "antiplatelet",
                "other"
        **/
            foreach (String item in sourceData)
            {   
                if (String.Equals("stepdown",item)) {

                    resultValue += "降压 ; ";
                }
                else if (String.Equals("Hypoglycemic", item))
                {
                    resultValue += "降糖 ; ";
                }
                else if (String.Equals("Fat", item))
                {
                    resultValue += "降脂 ; ";
                }
                else if (String.Equals("antiplatelet", item))
                {
                    resultValue += "抗血小板 ; ";
                }
                else if (String.Equals("other", item))
                {
                    resultValue += "其他 ; ";
                }
            }
            return resultValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
