namespace bbFiles.Model
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Credential
    {

        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(10)]
        public string Login { get; set; }

        [Required]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DefaultValue(false)]
        public bool PasswordChanged { get; set; }

        [Column(TypeName = "date")]
        public DateTime RegisteredDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastLoggedDate { get; set; }

        [Required]
        public bbFiles.Roles Role { get; set; }

        public virtual Acceptor Acceptor { get; set; }
    }
}
