namespace bbFiles.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Stat
    {
        [Key]
        public string BloodType { get; set; }

        public long TotalAmount { get; set; }
    }
}
