namespace bbFiles.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [ComplexType]
    public class Address
    {   [Required]
        [Display(Name = "City", ResourceType = typeof(bbFiles.Properties.Strings))]
        [StringLength(30)]
        public string City { get; set; }
        [Required]
        [StringLength(30)]
        [Display(Name = "Street", ResourceType = typeof(bbFiles.Properties.Strings))]
        public string Street { get; set; }
        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "PostalCode", ResourceType = typeof(bbFiles.Properties.Strings))]
        public string PostalCode { get; set; }
    }

    public partial class Acceptor
    {

        public Acceptor()
        {
            Orders = new List<Order>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "AcceptorID", ResourceType = typeof(bbFiles.Properties.Strings))]
        public int AcceptorID { get; set; }

        [Required]
        [Display(Name = "AcceptorName", ResourceType = typeof(bbFiles.Properties.Strings))]
        [StringLength(50)]
        public string AcceptorName { get; set; }

        [Required]
        [Display(Name = "Address", ResourceType = typeof(bbFiles.Properties.Strings))]
        public Address Address { get; set; }
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(bbFiles.Properties.Strings))]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "PhoneNumber", ResourceType = typeof(bbFiles.Properties.Strings))]
        public int? PhoneNumber { get; set; }

        [Display(Name = "RelevantUserID", ResourceType = typeof(bbFiles.Properties.Strings))]
        public User User { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
