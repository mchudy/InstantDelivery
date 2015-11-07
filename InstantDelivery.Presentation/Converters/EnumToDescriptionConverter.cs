using InstantDelivery.Helpers;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace InstantDelivery.Converters
{
    public class EnumToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return DependencyProperty.UnsetValue;
            Enum @enum = (Enum)value;
            return @enum.GetDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }


}