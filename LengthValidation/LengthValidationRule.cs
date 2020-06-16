using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Validations;

namespace LengthValidation
{
    public class LengthValidationRule : ValidationRule, IValidation
    {
        public string Name => "LengthValidation";

        public string Description => "Text is too short";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = (value as string);
            var fail = new ValidationResult(false, Description);
            if (String.IsNullOrEmpty(str)) return fail;
            if (str.Length < 5) return fail;
            return ValidationResult.ValidResult;
        }
    }
}
