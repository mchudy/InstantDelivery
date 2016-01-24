using InstantDelivery.Common.Enums;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace InstantDelivery.Converters
{
    /// <summary>
    /// Konwerter statusu paczki na widoczność tabeli z wyborem pracownika
    /// </summary>
    public class StatusToEmployeesGridVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Konwertuje status paczki do widoczności.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PackageStatus status = (PackageStatus)value;
            bool visible = status == PackageStatus.InWarehouse;
            return visible ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Konwertuje z powrotem.
        /// </summary>
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
        /// <summary>
        /// Konwertuje status paczki do widoczności.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PackageStatus status = (PackageStatus)value;
            bool visible = status == PackageStatus.InDelivery;
            return visible ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Konwertuje z powrotem.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
