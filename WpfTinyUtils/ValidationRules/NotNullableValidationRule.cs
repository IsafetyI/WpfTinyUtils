using System.Globalization;
using System.Windows.Controls;

namespace WpfTinyUtils.ValidationRules
{
    public class NotNullableValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "value shouldn't be null");
            return new ValidationResult(true, "Sucessful");
        }
    }
}
