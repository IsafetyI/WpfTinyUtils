using System.Globalization;
using System.Windows.Controls;

namespace WpfTinyUtils.ValidationRules
{
    public class NonEmptyStringValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "value shouldn't be null");
            if (value is not string)
                return new ValidationResult(false, "value should be a string");
            string sValue = (string)value;
            if (sValue.Trim().Length <= 0)
                return new ValidationResult(false, "value shouldn't be empty string");
            return new ValidationResult(true, "Sucessful");
        }
    }
}
