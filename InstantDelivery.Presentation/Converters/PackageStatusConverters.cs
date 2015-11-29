using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using InstantDelivery.Common.Enums;
using InstantDelivery.Domain.Entities;

namespace InstantDelivery.Converters
{
    /// <summary>
    /// Konwerter statusu paczki na widoczność tabeli z wyborem pracownika
    /// </summary>
    public class StatusToEmployeesGridVisibilityConverter : IValueConverter
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

    /// <summary>
    /// Konwerter statusu paczki na widoczność pola wyboru czy przesyłka została dostarczona
    /// </summary>
    public class StatusToInDeliveryVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PackageStatus status = (PackageStatus)value;
            bool visible = status == PackageStatus.InDelivery;
            return visible ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
