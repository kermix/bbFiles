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
        public int PESEL { set; get; }
        public Donor() { }
        public Donor(string firstname, string surname, DateTime birthdate, BloodTypes bloodtype, bool rhMarker, int pesel, string email, int? phone)
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
            databaseDataContext dc = new databaseDataContext();
            Donors newDonorRow = new Donors()
            {
                Firstname = this.firstname,
                Surname = this.surname,
                Birthdate = this.birthdate,
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
                dc.Donors.InsertOnSubmit(newDonorRow);
                dc.SubmitChanges();
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
            q.Birthdate = this.birthdate;
            q.BloodType = this.bloodtype;
            q.RhMarker = this.rhMarker;
            q.PhoneNumber = this.phone;
            q.Email = this.email;

            dc.SubmitChanges();
        }
    }
}
