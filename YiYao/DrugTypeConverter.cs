using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                //if (String.Equals("stepdown",item)) {

                //    resultValue += "降压 ; ";
                //}
                //else if (String.Equals("Hypoglycemic", item))
                //{
                //    resultValue += "降糖 ; ";
                //}
                //else if (String.Equals("Fat", item))
                //{
                //    resultValue += "降脂 ; ";
                //}
                //else if (String.Equals("antiplatelet", item))
                //{
                //    resultValue += "抗血小板 ; ";
                //}
                //else if (String.Equals("other", item))
                //{
                //    resultValue += "其他 ; ";
                //}
                string regetString = "^C10";
                Regex matchRegex;
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item)) {
                    resultValue += "降压药；";
                    continue;
                }
                regetString = "^A10B";
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "降糖药；";
                    continue;
                }
                regetString = "^A10A";
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "胰岛素；";
                    continue;
                }
                regetString = "^B01AC";
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "抗血小板；";
                    continue;
                }
                regetString = "^B03";
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "抗贫血；";
                    continue;
                }
                regetString = "^C08CA";
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "抗贫血；";
                    continue;
                }
                regetString = "^C08CA";
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "抗贫血；";
                    continue;
                }
                regetString = "^C08CA|C09A|C09C|C03|C07A|C02CA";
                
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "降压药；";
                    continue;
                }

                regetString = "^C01DA|C08D|C07A";

                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "抗心绞痛；";
                    continue;
                }
                
                regetString = "^C09A|C09C|C03|C07A";
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "抗心衰；";
                    continue;
                }
                
                regetString = "^A12AA|A11CC";
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "补钙；";
                    continue;
                }
                
                regetString = "^C09A|C09C";
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "减少尿蛋白；";
                    continue;
                }
                
                regetString = "^A11|A16AA";
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "营养补充剂；";
                    continue;
                }
                //以“B01”开头且不包含“B01AC”
                regetString = "^B01";
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    regetString = "B01AC";
                    matchRegex = new Regex(regetString);
                    if (!matchRegex.IsMatch(item))
                    {
                        resultValue += "抗凝；";
                        continue;
                    }
                }
                // 包含“C01EB15”或“A12CXXX”或“C07EB07”开头
                regetString = "C01EB15";
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "心肌营养代谢；";
                    continue;
                }

                regetString = "^A12CXXX|^C07EB07";
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "心肌营养代谢；";
                    continue;
                }

                // 包含“C04AF01”或“B01AC23”或以“A01XA”开头
                regetString = "C04AF01";
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "扩血管改善循环；";
                    continue;
                }

                regetString = "^B01AC23|^A01XA";
                matchRegex = new Regex(regetString);
                if (matchRegex.IsMatch(item))
                {
                    resultValue += "扩血管改善循环；";
                    continue;
                }

                resultValue += "未分类";
            }
            return resultValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
