using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace SmartBusWPF.Styles.Convertors
{
    public class Base64ImageConverterMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is string base64Image && !string.IsNullOrEmpty((string)values[0]))
            {
                return ConvertBase64ToImage(base64Image);
            }
            else if (values[1] is string defaultImage && !string.IsNullOrEmpty((string)values[1]))
            {
                return ConvertBase64ToImage(defaultImage);
            }
            else
            {
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private ImageSource ConvertBase64ToImage(string base64)
        {
            BitmapImage bi = new();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(System.Convert.FromBase64String(base64));
            bi.EndInit();
            return bi;
        }
    }
}