using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace bbFiles.Entities.ValidationRules
{
    /// <summary>
    /// Validates if value is in proper range.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.ValidationRule" />
    public class AmountValidationRule : ValidationRule
    {
        private int _min;
        private int _max;

        /// <summary>
        /// Gets or sets the minimum amount.
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
        /// Gets or sets the maximum amount.
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
        /// Validates if value does not overflow the minimum of the maximum.
        /// </summary>
        /// <param name="value">The value from the binding target to check.</param>
        /// <param name="cultureInfo">The culture to use in this rule.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.Controls.ValidationResult" /> object.
        /// </returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int _value = 0;
            bool parseResult = int.TryParse(value.ToString(), out _value);

            if (!parseResult || (_value < Min) || (_value > Max))
            {
                return new ValidationResult(false,
                  Resources.Strings.ProperFieldRange + Min + " - " + Max + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
