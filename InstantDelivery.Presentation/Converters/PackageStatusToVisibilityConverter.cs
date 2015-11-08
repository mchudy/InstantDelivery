using InstantDelivery.Core.Entities;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace InstantDelivery.Converters
{
    public class PackageStatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PackageStatus status = (PackageStatus)value;
            bool visible = status == PackageStatus.New;
            return visible ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
