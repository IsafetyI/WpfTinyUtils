using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfTinyUtils.Converters
{
    public class DateOnlyToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new NotImplementedException();
            if (value == null)
                return default(DateOnly).ToString();
            if (value is not DateOnly)
                return default(DateOnly).ToString();
            return ((DateOnly)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(DateOnly))
                throw new NotImplementedException();
            if (value == null)
                return default(DateOnly);
            if (value is not string)
                return default(DateOnly);
            if (DateOnly.TryParse((string)value, out DateOnly date))
                return date;
            return Binding.DoNothing;
        }
    }
}
