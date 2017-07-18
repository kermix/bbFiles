using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles.Structs
{
    class Order
    {
        public int acceptorID { get; set; }
        public BloodTypes bloodType { get; set; }
        public bool rhMarker { get; set; }
        public int amount { get; set; }
        public bool Send { set; get; }

        public Order() { }
        public Order(int acceptorID, BloodTypes bloodType, bool rhMarker, int amount, bool send)
        {
            this.acceptorID = acceptorID;
            this.bloodType = bloodType;
            this.rhMarker = rhMarker;
            this.amount = amount;
            this.Send = send;
        }

        public void Add()
        {
            var dc = new databaseDataContext();
            try
            {
                var newOrderRow = new Orders()
                {
                    AcceptorID = (from c in dc.Acceptors
                                  where c.AcceptorID == acceptorID
                                  select c.AcceptorID).Single(),
                    OrderDate = DateTime.Now.Date,
                    BloodType = bloodType,
                    RhMarker = rhMarker,
                    Amount = amount,
                    DonateID = GetAvaliableDonates(dc),
                    Send = false
                };
                
                dc.Orders.InsertOnSubmit(newOrderRow);
                dc.SubmitChanges();
            }
            catch (Exception ex) { throw ex; }
        }

        public string GetAvaliableDonates(databaseDataContext dc)
        {
            int blockedBlood = 0;
            var avaliableDonates = dc.Donates.Where(x => x.Available == true && 
                                                        x.BloodType == bloodType && 
                                                        x.RhMarker == rhMarker &&
                                                        x.Amount <= amount)
                .OrderByDescending(x => x.Amount)
                .AsEnumerable()
                .Select(x =>
                        {
                            blockedBlood += x.Amount;
                            return new { Donate = x, blood = blockedBlood };
                        })
                .TakeWhile(x => (x.blood < amount), true)
                .Select(x => { x.Donate.Available = false; return x.Donate.DonateID; });

            BlockAmountOfBlood(avaliableDonates, dc);

            return (string.Join(",", avaliableDonates));
        }

        public void BlockAmountOfBlood(IEnumerable<int> blockedIDs, databaseDataContext dc)
        {
            var stat = (from c in dc.Stats
                        where c.BloodType.StartsWith((bloodType).ToString() + (rhMarker == true ? "+" : "-"))
                        select c).Single();

            foreach(int donateID in blockedIDs)
            {
                stat.TotalAmount -= dc.Donates.Where(x => x.DonateID == donateID).Select(x => x.Amount).Single();
            }
        }


    }

}
