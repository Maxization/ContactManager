using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using Validations;

namespace Validation
{
    public class EmailValidationRule : ValidationRule, IValidation
    {
        public string Name => "EmailValidation";

        public string Description => "Email is not correct";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = (value as string);
            var fail = new ValidationResult(false, Description);
            if (String.IsNullOrEmpty(str)) return fail;
            if (Regex.IsMatch(str, @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$"))
            {
                return ValidationResult.ValidResult;
            }
            return fail;
        }
    }
}
