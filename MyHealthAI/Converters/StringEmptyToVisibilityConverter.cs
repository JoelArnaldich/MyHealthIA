using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MyHealthAI
{
    public class StringEmptyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && string.IsNullOrEmpty(str))
            {
                return Visibility.Collapsed; // Si el string está vacío, ocultamos el control
            }
            return Visibility.Visible; // Si el string tiene valor, mostramos el control
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
