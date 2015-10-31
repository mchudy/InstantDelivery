using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace InstantDelivery.Core.Repositories
{
    public class SalaryValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            decimal i;
            return value != null && decimal.TryParse(value.ToString(), out i) && i>0 ? new ValidationResult(true, null) : new ValidationResult(false, "Proszę podać dodatnią wartość całkowitoliczbową.");
        }
    }

    public class FirstNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            var condition = value != null && (string) value != "" && char.IsUpper(((string) value)[0]) && !((string) value).Any(char.IsDigit) && ((string) value).All(char.IsLetterOrDigit);
            return condition ? new ValidationResult(true, null) : new ValidationResult(false, "Proszę podać poprawne imię.");
        }
    }

    public class LastNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            var condition = value != null && (string)value != "" && char.IsUpper(((string)value)[0]) && !((string)value).Any(char.IsDigit) && ((string)value).All(char.IsLetterOrDigit);
            return condition ? new ValidationResult(true, null) : new ValidationResult(false, "Proszę podać poprawne nazwisko.");
        }
    }

    public class PhoneNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            var condition = value != null && ((string)value).All(char.IsDigit) && ((string)value).Length==9;
            return condition ? new ValidationResult(true, null) : new ValidationResult(false, "Proszę podać 9-cyfrowy numer telefonu.");
        }
    }
}