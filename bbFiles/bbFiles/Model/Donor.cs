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

        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Column(TypeName = "date")]
        public DateTime Birthdate { get; set; }

        public int BloodType { get; set; }

        public bool RhMarker { get; set; }

        public double BloodGiven { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PESEL { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int? PhoneNumber { get; set; }

        public virtual ICollection<Donate> Donates { get; set; }
    }
}
