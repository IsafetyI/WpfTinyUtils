using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfTinyUtils.Converters
{
    public class BoolToStringConverterInverted : IValueConverter
    {
        public static string NoString { get; set; } = "No";

        public static string YesString { get; set; } = "Yes";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new NotImplementedException();
            if (value == null)
                return string.Empty;
            if (value is not bool)
                return string.Empty;
            if ((bool)value)
                return NoString;
            return YesString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new NotImplementedException();
            if (value == null)
                return false;
            if (value is not string)
                return false;
            return (string)value == NoString;
        }
    }
}
