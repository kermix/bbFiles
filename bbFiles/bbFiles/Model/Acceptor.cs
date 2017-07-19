namespace bbFiles.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Acceptor
    {

        public Acceptor()
        {
            Orders = new List<Order>();
        }

        public int AcceptorID { get; set; }

        [Required]
        [StringLength(10)]
        public string AcceptorName { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public int? PhoneNumber { get; set; }

        public User User { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
