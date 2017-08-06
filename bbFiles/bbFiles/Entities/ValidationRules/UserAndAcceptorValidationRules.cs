using System;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace bbFiles.Entities.ValidationRules
{
    /// <summary>
    /// Rule that validates lenght of the field's value.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.ValidationRule" />
    public class LengthValidationRule : ValidationRule
    {
        private int _min;
        private int _max;

        /// <summary>
        /// Gets or sets the minimum lenght.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }
        /// <summary>
        /// Gets or sets the maximum length.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        /// <summary>
        /// When overridden in a derived class, performs validation checks on a value.
        /// Validates if the value lenhgt is not in proper range.
        /// </summary>
        /// <param name="value">The value from the binding target to check.</param>
        /// <param name="cultureInfo">The culture to use in this rule.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.Controls.ValidationResult" /> object.
        /// </returns>
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
    /// <summary>
    /// Validates format of the value and characters used in value.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.ValidationRule" />
    public class RegexValidationRule : ValidationRule
    {
        private string _regex;
        /// <summary>
        /// Gets or sets the validation reg ex.
        /// </summary>
        /// <value>
        /// The reg ex.
        /// </value>
        public string RegEx
        {
            get { return _regex; }
            set { _regex = value; }
        }

        /// <summary>
        /// When overridden in a derived class, performs validation checks on a value.
        /// Validates if user use proper format and characters to fill the field.
        /// </summary>
        /// <param name="value">The value from the binding target to check.</param>
        /// <param name="cultureInfo">The culture to use in this rule.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.Controls.ValidationResult" /> object.
        /// </returns>
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
    /// <summary>
    /// Validates if value is Email address
    /// </summary>
    /// <seealso cref="System.Windows.Controls.ValidationRule" />
    public class EmailValidationRule : ValidationRule
    {

        /// <summary>
        /// When overridden in a derived class, performs validation checks on a value.
        /// Validates if user use proper address email.
        /// </summary>
        /// <param name="value">The value from the binding target to check.</param>
        /// <param name="cultureInfo">The culture to use in this rule.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.Controls.ValidationResult" /> object.
        /// </returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var emailAttrib = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            bool result;
            result = emailAttrib.IsValid(value.ToString());

            return new ValidationResult(result, Resources.Strings.InProperEmailFormat);
        }
    }
}

