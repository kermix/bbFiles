﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace bbFiles.Converters
{
    /// <summary>
    /// Used to change boolean value to a Visibility enum but in reverse way. True -> Colapsed, False -> Visible
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    class reverseBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = false;
            if (value is bool)
            {
                flag = (bool)value;
            }
            else if (value is bool?)
            {
                bool? nullable = (bool?)value;
                flag = nullable.HasValue ? nullable.Value : false;
            }
            return (flag ? Visibility.Collapsed : Visibility.Visible);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
