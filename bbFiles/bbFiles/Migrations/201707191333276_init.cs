namespace bbFiles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Stats");
            AddColumn("dbo.Credentials", "Acceptor_AcceptorID", c => c.Int());
            AddColumn("dbo.Donates", "Donor_PESEL", c => c.Long());
            AddColumn("dbo.Donates", "Order_OrderID", c => c.Int());
            AddColumn("dbo.Orders", "Acceptor_AcceptorID", c => c.Int());
            AlterColumn("dbo.Acceptors", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Credentials", "Login", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Credentials", "Password", c => c.String(nullable: false, maxLength: 18));
            AlterColumn("dbo.Stats", "BloodType", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Stats", "BloodType");
            CreateIndex("dbo.Orders", "Acceptor_AcceptorID");
            CreateIndex("dbo.Donates", "Donor_PESEL");
            CreateIndex("dbo.Donates", "Order_OrderID");
            CreateIndex("dbo.Credentials", "Acceptor_AcceptorID");
            AddForeignKey("dbo.Orders", "Acceptor_AcceptorID", "dbo.Acceptors", "AcceptorID");
            AddForeignKey("dbo.Donates", "Donor_PESEL", "dbo.Donors", "PESEL");
            AddForeignKey("dbo.Donates", "Order_OrderID", "dbo.Orders", "OrderID");
            AddForeignKey("dbo.Credentials", "Acceptor_AcceptorID", "dbo.Acceptors", "AcceptorID");
            DropColumn("dbo.Acceptors", "UserID");
            DropColumn("dbo.Donates", "BloodType");
            DropColumn("dbo.Donates", "RhMarker");
            DropColumn("dbo.Donates", "DonorPESEL");
            DropColumn("dbo.Orders", "AcceptorID");
            DropColumn("dbo.Orders", "DonateID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "DonateID", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "AcceptorID", c => c.Int(nullable: false));
            AddColumn("dbo.Donates", "DonorPESEL", c => c.Long(nullable: false));
            AddColumn("dbo.Donates", "RhMarker", c => c.Boolean(nullable: false));
            AddColumn("dbo.Donates", "BloodType", c => c.Int(nullable: false));
            AddColumn("dbo.Acceptors", "UserID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Credentials", "Acceptor_AcceptorID", "dbo.Acceptors");
            DropForeignKey("dbo.Donates", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.Donates", "Donor_PESEL", "dbo.Donors");
            DropForeignKey("dbo.Orders", "Acceptor_AcceptorID", "dbo.Acceptors");
            DropIndex("dbo.Credentials", new[] { "Acceptor_AcceptorID" });
            DropIndex("dbo.Donates", new[] { "Order_OrderID" });
            DropIndex("dbo.Donates", new[] { "Donor_PESEL" });
            DropIndex("dbo.Orders", new[] { "Acceptor_AcceptorID" });
            DropPrimaryKey("dbo.Stats");
            AlterColumn("dbo.Stats", "BloodType", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Credentials", "Password", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Credentials", "Login", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Acceptors", "Email", c => c.String(maxLength: 50));
            DropColumn("dbo.Orders", "Acceptor_AcceptorID");
            DropColumn("dbo.Donates", "Order_OrderID");
            DropColumn("dbo.Donates", "Donor_PESEL");
            DropColumn("dbo.Credentials", "Acceptor_AcceptorID");
            AddPrimaryKey("dbo.Stats", "BloodType");
        }
    }
}
