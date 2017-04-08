//-----------------------------------------------------------------------
// <copyright file="BoolToVisibilityValueConverter.cs" company="troncell">
//     Copyright © troncell. All rights reserved.
// </copyright>
// <author>William Wu</author>
// <email>wulixu@troncell.com</email>
// <date>2012-10-15</date>
// <summary>no summary</summary>
//-----------------------------------------------------------------------
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace YiYao
{
    public class BoolToVisibilityValueConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            if (value is bool)
            {
                Visibility visibility = (bool)value ? Visibility.Visible : Visibility.Collapsed;
                return visibility;
            }
            Type type = value.GetType();
            throw new InvalidOperationException("Unsupported type [" + type.Name + "] in the BoolToVisibilityValueConverter.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}