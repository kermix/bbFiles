using System;
using System.Text.RegularExpressions;

namespace bbFiles
{
    abstract class UserContactInfo
    {
        public int? phone { get; set; }
        public string email { get; set; }

        public bool IsPhoneNumberValid()
        {
            if (!Regex.Match(phone.ToString(), @"^(\+[0-9]{9})$").Success)
                throw new ArgumentException(Properties.Strings.InvalidPhoneNumber);
            else
                return true;
        }
        public bool IsEmailValid()
        {
            try
            {
                System.Net.Mail.MailAddress mail = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}