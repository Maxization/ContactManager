using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using Validations;

namespace PhoneValidation
{
    public class PhoneValidationRule : ValidationRule, IValidation
    {
        public string Name => "PhoneValidation";

        public string Description => "Value is not correct phone number";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = (value as string);
            var fail = new ValidationResult(false, Description);
            if (String.IsNullOrEmpty(str)) return fail;
            if (Regex.IsMatch(str, @"^([0-9]{3}-){2}[0-9]{3}$"))
            {
                return ValidationResult.ValidResult;
            }
            return fail;
        }
    }
}
