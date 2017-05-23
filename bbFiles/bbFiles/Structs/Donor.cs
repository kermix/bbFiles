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
    }
}
