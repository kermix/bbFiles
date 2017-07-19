namespace bbFiles
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class databaseContext : DbContext
    {
        public databaseContext()
            : base("name=databaseContext")
        {
        }

        public virtual DbSet<Model.Acceptor> Acceptors { get; set; }
        public virtual DbSet<Model.Credential> Credentials { get; set; }
        public virtual DbSet<Model.Donate> Donates { get; set; }
        public virtual DbSet<Model.Donor> Donors { get; set; }
        public virtual DbSet<Model.Order> Orders { get; set; }
        public virtual DbSet<Model.Stat> Stats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
 
        }
    }
}
