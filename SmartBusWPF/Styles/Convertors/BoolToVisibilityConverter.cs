using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace SmartBusWPF.Styles.Convertors
{
    public class BoolToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool hasText =  !(bool)values[0];
            bool hasFocus = (bool)values[1];
            
            if (hasText || hasFocus)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}