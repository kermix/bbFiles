using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace bbFiles.Entities.ValidationRules
{
    public class AmountValidationRule : ValidationRule
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
