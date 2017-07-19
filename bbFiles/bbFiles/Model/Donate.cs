namespace bbFiles.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Donate
    {
        public int DonateID { get; set; }

        public int Amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public bool Available { get; set; }

        public Donor Donor { get; set; }
    }
}
