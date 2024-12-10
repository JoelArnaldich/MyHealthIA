using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MyHealthAI
{

    public class MessageAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Si es un mensaje del usuario, se alinea a la derecha
            return value is bool isUserMessage && isUserMessage ? HorizontalAlignment.Right : HorizontalAlignment.Left;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}

