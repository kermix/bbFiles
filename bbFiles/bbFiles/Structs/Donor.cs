using System;

namespace bbFiles.Structs
{
    class Donor : UserContactInfo
    {
        public string firstname { get; set; }
        public string surname { get; set; }
        public DateTime birthdate { get; }
        public BloodTypes bloodtype { get; set; }
        public bool rhMarker { get; set; }
        public int PESEL { set; get; }
        public Donor() { }
        public Donor(string firstname, string surname, DateTime birthdate, BloodTypes bloodtype, bool rhMarker, int pesel)
        {
            this.firstname = firstname;
            this.surname = surname;
            this.birthdate = birthdate;
            this.bloodtype = bloodtype;
            this.rhMarker = rhMarker;
            this.PESEL = pesel;
        }
    }
}
