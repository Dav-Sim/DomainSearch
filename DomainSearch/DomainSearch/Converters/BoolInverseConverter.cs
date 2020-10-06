using System;
using System.Globalization;
using System.Windows.Data;

namespace DomainSearch.Converters
{
    /// <summary>
    /// this converter inverts bool
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BoolInverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
