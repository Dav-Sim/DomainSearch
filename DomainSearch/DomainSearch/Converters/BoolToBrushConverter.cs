using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace DomainSearch.Converters
{
    /// <summary>
    /// this converter replaces true to green and false to red
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush cFalse = Brushes.Tomato;
            Brush cTrue = Brushes.GreenYellow;

            if (parameter != null && parameter.ToString().Contains("|"))
            {
                try
                {
                    string[] cols = parameter.ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    cTrue = (SolidColorBrush)new BrushConverter().ConvertFromString(cols[0]);
                    cFalse = (SolidColorBrush)new BrushConverter().ConvertFromString(cols[1]);
                }
                catch { }
            }

            if ((bool)value == true)
            {
                return cTrue;
            }
            else
            {
                return cFalse;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
