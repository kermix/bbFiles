using System;
using System.Linq;

namespace bbFiles.Structs
{
    class Donate
    {
        public int? amount { get; set; }
        public long? pesel { get; set; }
        public Donate() { }
        public Donate(int amount, long pesel)
        {
            this.amount = amount;
            this.pesel = pesel;
        }

        public void Add()
        {
            try
            {
                var dc = new databaseDataContext();
                var q = (from c in dc.Donors
                         where c.PESEL == pesel
                         select c).SingleOrDefault();
                if (q == null)
                    throw new Exception(Properties.Strings.DonorWithPeselNotFound);
                else
                {
                    var newDonateRow = new Donates()
                    {
                        Amount = this.amount != null ? (int)amount : 0,
                        Date = DateTime.Now.Date,
                        BloodType = q.BloodType,
                        RhMarker = q.RhMarker,
                        DonorPESEL = (long)pesel,
                        Available = true                        
                    };
                    var bloodGiven = (from c in dc.Donors
                                      where c.PESEL == pesel
                                      select c.BloodGiven).Single();
                    dc.Donates.InsertOnSubmit(newDonateRow);
                    bloodGiven += amount != null ? (int)amount / 1000 : 0;
                    dc.SubmitChanges();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



    }
}
