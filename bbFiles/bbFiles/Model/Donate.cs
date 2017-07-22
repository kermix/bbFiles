namespace bbFiles.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Donate
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID", ResourceType = typeof(bbFiles.Properties.Strings))]
        public int DonateID { get; set; }

        [Required]
        [Range(100, 1000)]
        [Display(Name = "Amount", ResourceType = typeof(bbFiles.Properties.Strings))]
        public int Amount { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "DonateDate", ResourceType = typeof(bbFiles.Properties.Strings))]
        public DateTime Date { get; set; }

        [DefaultValue(false)]
        [Display(Name = "DonateDate", ResourceType = typeof(bbFiles.Properties.Strings))]
        public bool Available { get; set; }

        [Display(Name = "PESEL", ResourceType = typeof(bbFiles.Properties.Strings))]
        public Donor Donor { get; set; }
    }
}
