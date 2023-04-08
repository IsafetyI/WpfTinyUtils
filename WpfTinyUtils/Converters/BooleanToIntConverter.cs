using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfTinyUtils.Converters
{
    public class BooleanToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(int) && targetType != typeof(object))
                throw new NotImplementedException();
            if (value == null)
                return 0;
            if (value is not bool)
                return 0;
            if ((bool)value)
                return 1;
            else
                return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new NotImplementedException();
            if (value == null)
                return false;
            if (value is not int && value is not object)
                return false;
            return (int)value == 1;
        }
    }
}
