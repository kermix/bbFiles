namespace bbFiles.Model
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Credential
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID", ResourceType = typeof(bbFiles.Properties.Strings))]
        public int UserID { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Username", ResourceType = typeof(bbFiles.Properties.Strings))]
        public string Login { get; set; }

        [Required]
        [StringLength(18, MinimumLength = 6)]
        [Display(Name = "Password", ResourceType = typeof(bbFiles.Properties.Strings))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DefaultValue(false)]
        [Display(Name = "PasswordChanged", ResourceType = typeof(bbFiles.Properties.Strings))]
        public bool PasswordChanged { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "RegisteredDate", ResourceType = typeof(bbFiles.Properties.Strings))]
        public DateTime RegisteredDate { get; set; }
        
        [Column(TypeName = "date")]
        [Display(Name = "LastLoginDate", ResourceType = typeof(bbFiles.Properties.Strings))]
        public DateTime? LastLoggedDate { get; set; }

        [Required]
        [Display(Name = "Roles", ResourceType = typeof(bbFiles.Properties.Strings))]
        public bbFiles.Roles Role { get; set; }

        public virtual Acceptor Acceptor { get; set; }
    }
}
