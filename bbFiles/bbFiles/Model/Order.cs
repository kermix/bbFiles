namespace bbFiles.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public Order()
        {
            Donates = new List<Donate>();
        }
        public int OrderID { get; set; }

        [Column(TypeName = "date")]
        public DateTime OrderDate { get; set; }

        public short? BloodType { get; set; }

        public bool? RhMarker { get; set; }

        public double? Amount { get; set; }

        public bool? Send { get; set; }

        public Acceptor Acceptor { get; set; }

        public ICollection<Donate> Donates { get; set; }
    }
}
