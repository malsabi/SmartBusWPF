using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SmartBusWPF.Styles.Convertors
{
    public class Base64ImageConverterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is not string s)
                return null;

            BitmapImage bi = new();

            try
            {
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(System.Convert.FromBase64String(s));
                bi.EndInit();

                return bi;
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}