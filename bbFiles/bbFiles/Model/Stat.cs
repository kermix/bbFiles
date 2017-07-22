namespace bbFiles.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Stat
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "BloodType", ResourceType = typeof(bbFiles.Properties.Strings))]
        public string BloodType { get; set; }

        [Range(0,long.MaxValue)]
        [Required]
        [Display(Name = "Amount", ResourceType = typeof(bbFiles.Properties.Strings))]
        public long TotalAmount { get; set; }
    }
}
