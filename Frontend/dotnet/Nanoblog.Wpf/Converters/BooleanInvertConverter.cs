using System;
using System.Globalization;
using System.Windows.Data;

namespace Nanoblog.Wpf.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(bool) && value is bool)
            {
                return !(bool)value;
            }

            throw new ArgumentException("Value and targetType must be boolean type!");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
