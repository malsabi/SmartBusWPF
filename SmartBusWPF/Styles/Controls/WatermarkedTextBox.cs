using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace SmartBusWPF.Styles.Controls
{
    public class WatermarkedTextBox : TextBox
    {
        private TextBlock watermarkTextBlock;

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(string), typeof(WatermarkedTextBox));

        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }


        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(WatermarkedTextBox));

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(WatermarkedTextBox));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PasswordBox passwordBox = GetTemplateChild("InputText") as PasswordBox;
            watermarkTextBlock = GetTemplateChild("Watermark") as TextBlock;
            if (passwordBox != null)
            {
                passwordBox.GotFocus += PasswordBox_GotFocus;
                passwordBox.LostFocus += PasswordBox_LostFocus;
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null && watermarkTextBlock != null)
            {
                watermarkTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null && passwordBox.Password.Length == 0)
            {
                watermarkTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                watermarkTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null && watermarkTextBlock != null)
            {
                Password = passwordBox.Password;
                if (Password.Length > 0)
                {
                    watermarkTextBlock.Visibility = Visibility.Collapsed;
                }
                else
                {
                    watermarkTextBlock.Visibility = Visibility.Visible;
                }
            }
        }
    }
}