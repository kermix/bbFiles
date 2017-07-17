using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles.Structs
{
    class Order
    {
        public string acceptorName { get; set; }
        public BloodTypes bloodType { get; set; }
        public bool rhMarker { get; set; }
        public double amount { get; set; }

        public Order() { }
        public Order(string acceptorName, BloodTypes bloodType, bool rhMarker, double amount)
        {
            this.acceptorName = acceptorName;
            this.bloodType = bloodType;
            this.rhMarker = rhMarker;
            this.amount = amount;
        }

        public void Add()
        {
            var dc = new databaseDataContext();
            var newOrderRow = new Orders()
            {
                AcceptorID = (from c in dc.Acceptors
                              where c.AcceptorName == acceptorName
                              select c.AcceptorID).Single(),
                OrderDate = DateTime.Now.Date,
                BloodType = bloodType,
                RhMarker = rhMarker,
                Amount = amount
            };
        }
    }

}
