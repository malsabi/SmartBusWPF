using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace SmartBusWPF.Styles.Convertors
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                throw new ArgumentException("The value must be a boolean.", nameof(value));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibilityValue)
            {
                return visibilityValue == Visibility.Visible;
            }
            else
            {
                throw new ArgumentException("The value must be a Visibility.", nameof(value));
            }
        }
    }
}