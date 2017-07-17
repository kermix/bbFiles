using System;
using System.Linq;

namespace bbFiles.Structs
{
    partial class Donor : UserContactInfo
    {
        public string firstname { get; set; }
        public string surname { get; set; }
        public DateTime birthdate { get; }
        public BloodTypes bloodtype { get; set; }
        public bool rhMarker { get; set; }
        public long PESEL { set; get; }
  
        public Donor() { }
        public Donor(string firstname, string surname, DateTime birthdate, BloodTypes bloodtype, bool rhMarker, long pesel, string email, int? phone)
        {
            this.firstname = firstname;
            this.surname = surname;
            this.birthdate = birthdate;
            this.bloodtype = bloodtype;
            this.rhMarker = rhMarker;
            this.PESEL = pesel;
            this.email = email;
            this.phone = phone;
        }

        public void Add()
        {
            var dc = new databaseDataContext();
            var newDonorRow = new Donors()
            {
                Firstname = this.firstname,
                Surname = this.surname,
                BloodType = this.bloodtype,
                RhMarker = this.rhMarker,
                BloodGiven = 0,
                PESEL = this.PESEL,
                PhoneNumber = this.phone,
                Email = this.email
            };
            try
            {
                this.IsPhoneNumberValid();
                this.IsEmailValid();
                newDonorRow.Birthdate = ValidatePesel(newDonorRow.PESEL);
                if (newDonorRow.Birthdate != DateTime.MinValue.Date)
                {
                    if (DonorExist(this.PESEL))
                        throw new UserEditException(Properties.Strings.DonorExist);
                    dc.Donors.InsertOnSubmit(newDonorRow);
                    dc.SubmitChanges();
                }
                else
                {
                    throw new ArgumentException(Properties.Strings.PeselInvalid);
                }
            }catch(Exception ex) { throw ex; }
        }

        public void Edit()
        {
            databaseDataContext dc = new databaseDataContext();
            var q = (from r in dc.Donors
                     where r.PESEL == this.PESEL
                     select r).Single();

            q.Firstname = this.firstname;
            q.Surname = this.surname;
            q.BloodType = this.bloodtype;
            q.RhMarker = this.rhMarker;
            q.PhoneNumber = this.phone;
            q.Email = this.email;
            this.IsPhoneNumberValid();
            this.IsEmailValid();
            dc.SubmitChanges();
        }

        public static bool DonorExist(long pesel)
        {
            databaseDataContext dc = new databaseDataContext();
            bool userExists = (from c in dc.Donors
                               where c.PESEL == pesel
                               select c).Any();
            if (userExists)
                return true;
            return false;
        }
    }
}
