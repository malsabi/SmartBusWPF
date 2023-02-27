using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
using SmartBusWPF.Common.Enums;
using SmartBusWPF.Common.Consts;

namespace SmartBusWPF.Styles.Convertors
{
    public class TripStatusToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TripStatus status && TripStatusConsts.BackgroundColors.TryGetValue(status, out var brush))
            {
                return brush;
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}