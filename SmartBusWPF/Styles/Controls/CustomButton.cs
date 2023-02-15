using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace SmartBusWPF.Styles.Controls
{
    public class CustomButton : Button
    {
        public static readonly DependencyProperty IsCircularProperty = DependencyProperty.Register("IsCircular", typeof(bool), typeof(CustomButton));
        public bool IsCircular
        {
            get { return (bool)GetValue(IsCircularProperty); }
            set { SetValue(IsCircularProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(CustomButton));
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register("ImageHeight", typeof(double), typeof(CustomButton));
        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register("ImageWidth", typeof(double), typeof(CustomButton));
        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public static readonly DependencyProperty ImageMarginProperty = DependencyProperty.Register("ImageMargin", typeof(Thickness), typeof(CustomButton));
        public Thickness ImageMargin
        {
            get { return (Thickness)GetValue(ImageMarginProperty); }
            set { SetValue(ImageMarginProperty, value); }
        }

        public static readonly DependencyProperty ImageVerticalAlignmentProperty = DependencyProperty.Register("ImageVerticalAlignment", typeof(VerticalAlignment), typeof(CustomButton));
        public VerticalAlignment ImageVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(ImageVerticalAlignmentProperty); }
            set { SetValue(ImageVerticalAlignmentProperty, value); }
        }

        public static readonly DependencyProperty ImageHorizontalAlignmentProperty = DependencyProperty.Register("ImageHorizontalAlignment", typeof(HorizontalAlignment), typeof(CustomButton));
        public HorizontalAlignment ImageHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(ImageHorizontalAlignmentProperty); }
            set { SetValue(ImageHorizontalAlignmentProperty, value); }
        }

        public static readonly DependencyProperty ImageStretchProperty = DependencyProperty.Register("ImageStretch", typeof(Stretch), typeof(CustomButton));
        public Stretch ImageStretch
        {
            get { return (Stretch)GetValue(ImageStretchProperty); }
            set { SetValue(ImageStretchProperty, value); }
        }

        public static readonly DependencyProperty DisabledColorProperty = DependencyProperty.Register("DisabledColor", typeof(SolidColorBrush), typeof(CustomButton));
        public Brush DisabledColor
        {
            get { return (Brush)GetValue(DisabledColorProperty); }
            set { SetValue(DisabledColorProperty, value); }
        }

        public static readonly DependencyProperty HoverColorProperty = DependencyProperty.Register("HoverColor", typeof(SolidColorBrush), typeof(CustomButton));
        public Brush HoverColor
        {
            get { return (Brush)GetValue(HoverColorProperty); }
            set { SetValue(HoverColorProperty, value); }
        }

        public static readonly DependencyProperty ClickColorProperty = DependencyProperty.Register("ClickColor", typeof(SolidColorBrush), typeof(CustomButton));
        public Brush ClickColor
        {
            get { return (Brush)GetValue(ClickColorProperty); }
            set { SetValue(ClickColorProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CustomButton));
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty ContentMarginProperty = DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(CustomButton));
        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }
    }
}