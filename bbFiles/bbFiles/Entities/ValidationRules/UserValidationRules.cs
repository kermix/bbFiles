using System;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace bbFiles.Entities.ValidationRules
{
    public class LengthValidationRule : ValidationRule
    {
        private int _min;
        private int _max;

        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }
        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int length = value.ToString().Length;
            if ((length < Min) || (length > Max))
            {
                return new ValidationResult(false,
                  Resources.Strings.ProperFieldLength + Min + " - " + Max + "."); 
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
    public class RegexValidationRule : ValidationRule
    {
        private string _regex;
        public string RegEx
        {
            get { return _regex; }
            set { _regex = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool m = Regex.IsMatch(value.ToString(), RegEx);
            string output = Resources.Strings.ProperFieldCharacters;

            if (RegEx == "^[a-zA-Z0-9]+$")
                output += "a-z, A-Z, 0-9";
            else if (RegEx == "^[a-zA-Z0-9!@#$%^*()-+=_]+$")
                output += "a-z, A-Z, 0-9, !@#$%^*()-+=_";
            else
                output = Resources.Strings.InProperFieldFormat;

            if (m == false)
                return new ValidationResult(false, output);
            else
                return new ValidationResult(true, null);
        }
    }
    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
                var emailAttrib = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
                bool result;
                result = emailAttrib.IsValid(value.ToString());

                return new ValidationResult(result, Resources.Strings.InProperEmailFormat);
        }
    }
}
