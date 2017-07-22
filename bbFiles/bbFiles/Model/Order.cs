namespace bbFiles.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public Order()
        {
            Donates = new List<Donate>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID", ResourceType = typeof(bbFiles.Properties.Strings))]
        public int OrderID { get; set; }

        [Column(TypeName = "date")]
        [Required]
        [Display(Name = "OrderDate", ResourceType = typeof(bbFiles.Properties.Strings))]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "BloodType", ResourceType = typeof(bbFiles.Properties.Strings))]
        public short BloodType { get; set; }

        [Required]
        [Display(Name = "RhMarker", ResourceType = typeof(bbFiles.Properties.Strings))]
        public bool RhMarker { get; set; }

        [Required]
        [Display(Name = "Amount", ResourceType = typeof(bbFiles.Properties.Strings))]
        [Range(0.0, int.MaxValue)]
        public int Amount { get; set; }

        [DefaultValue(false)]
        [Required]
        [Display(Name = "Send", ResourceType = typeof(bbFiles.Properties.Strings))]
        public bool Send { get; set; }

        [Required]
        [Display(Name = "AcceptorID", ResourceType = typeof(bbFiles.Properties.Strings))]
        public Acceptor Acceptor { get; set; }

        [Required]
        [Display(Name = "DonateIDs", ResourceType = typeof(bbFiles.Properties.Strings))]
        public ICollection<Donate> Donates { get; set; }
    }
}
