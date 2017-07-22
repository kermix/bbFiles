namespace bbFiles.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Donor
    {

        public Donor()
        {
            Donates = new List<Donate>();
        }


        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "PESEL", ResourceType = typeof(bbFiles.Properties.Strings))]
        public long PESEL { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Firstname", ResourceType = typeof(bbFiles.Properties.Strings))]
        public string Firstname { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Lastname", ResourceType = typeof(bbFiles.Properties.Strings))]
        public string Surname { get; set; }

        [Column(TypeName = "date")]
        [Required]
        [Display(Name = "Birthdate", ResourceType = typeof(bbFiles.Properties.Strings))]
        public DateTime Birthdate { get; set; }

        [Required]
        [Display(Name = "BloodType", ResourceType = typeof(bbFiles.Properties.Strings))]
        public int BloodType { get; set; }

        [Required]
        [Display(Name = "RhMarker", ResourceType = typeof(bbFiles.Properties.Strings))]
        public bool RhMarker { get; set; }

        [Required]
        [Display(Name = "BloodGiven", ResourceType = typeof(bbFiles.Properties.Strings))]
        public double BloodGiven { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(bbFiles.Properties.Strings))]
        public string Email { get; set; }

        [Phone]
        [Required]
        [Display(Name = "Phone", ResourceType = typeof(bbFiles.Properties.Strings))]
        public int? PhoneNumber { get; set; }

        public virtual ICollection<Donate> Donates { get; set; }
    }
}
