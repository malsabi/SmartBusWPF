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
            if (value is bool isVisible)
            {
                return isVisible ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Visible;
            }
            throw new ArgumentException("The value must be a Visibility.", nameof(value));
        }
    }
}