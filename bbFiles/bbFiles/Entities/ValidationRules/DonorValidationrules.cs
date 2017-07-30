using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace bbFiles.Entities.ValidationRules
{
    public class PeselValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // Source: http://progbis.blogspot.com/2012/07/walidacja-numeru-pesel-w-c.html
            string pesel = value.ToString();
            byte[] weights = new byte[10] { 9, 7, 3, 1, 9, 7, 3, 1, 9, 7 };
            bool bResult = false;
            int sum = 0;
            int control = 0;

            control = Convert.ToInt32(pesel[10].ToString());

            for (int i = 0; i < 10; i++)
            {
                sum += weights[i] * Convert.ToInt32(pesel[i].ToString());
            }

            bResult = ((sum % 10) == control);

            if (bResult)
            {
                int year = 0;
                int month = 0;
                int day = Convert.ToInt32(pesel[4].ToString()) * 10 + Convert.ToInt32(pesel[5].ToString());

                if (pesel[2] == '0' || pesel[2] == '1')
                {
                    year = 1900;
                    month = Convert.ToInt32(pesel[2].ToString()) * 10 + Convert.ToInt32(pesel[3].ToString());
                }
                else if (pesel[2] == '2' || pesel[2] == '3')
                {
                    year = 2000;
                    month = (Convert.ToInt32(pesel[2].ToString()) * 10 + Convert.ToInt32(pesel[3].ToString()) - 20);
                }
                else if (pesel[2] == '4' || pesel[2] == '5')
                {
                    year = 2100;
                    month = (Convert.ToInt32(pesel[2].ToString()) * 10 + Convert.ToInt32(pesel[3].ToString()) - 40);
                }
                else if (pesel[2] == '6' || pesel[2] == '7')
                {
                    year = 2200;
                    month = (Convert.ToInt32(pesel[2].ToString()) * 10 + Convert.ToInt32(pesel[3].ToString()) - 60);
                }
                else if (pesel[2] == '8' || pesel[2] == '9')
                {
                    year = 1800;
                    month = (Convert.ToInt32(pesel[2].ToString()) * 10 + Convert.ToInt32(pesel[3].ToString()) - 80);
                }
                year += Convert.ToInt32(pesel[0].ToString()) * 10 + Convert.ToInt32(pesel[1].ToString());
                String szDate = year.ToString() + "-" + (month < 10 ? "0" + month.ToString() : month.ToString()) + "-" + (day < 10 ? "0" + day.ToString() : day.ToString());
                DateTime dt;
                bResult = DateTime.TryParse(szDate, out dt);
            }

            if (bResult)
                return new ValidationResult(true, null);
            else
                return new ValidationResult(false, bbFiles.Resources.Strings.PeselError);

        }
    }
}
