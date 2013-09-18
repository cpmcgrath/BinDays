using System;
using System.Windows;
using System.Windows.Data;

namespace CMcG.BinDays
{
    public class VisibleConverter : IValueConverter
    {
        public bool Invert      { get; set; }
        public bool HideOnEmpty { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = (value is bool) ? (bool)value : value != null && (!HideOnEmpty || !object.Equals(value, string.Empty));
            return val ^ Invert ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var result = (Visibility)value == Visibility.Visible;
            return result ^ Invert;
        }
    }
}
