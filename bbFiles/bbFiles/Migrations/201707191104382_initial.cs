namespace bbFiles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Acceptors",
                c => new
                    {
                        AcceptorID = c.Int(nullable: false, identity: true),
                        AcceptorName = c.String(nullable: false, maxLength: 10),
                        Address = c.String(nullable: false, maxLength: 50),
                        Email = c.String(maxLength: 50),
                        PhoneNumber = c.Int(),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AcceptorID);
            
            CreateTable(
                "dbo.Credentials",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        PasswordChanged = c.Boolean(nullable: false),
                        RegisteredDate = c.DateTime(nullable: false, storeType: "date"),
                        LastLoggedDate = c.DateTime(storeType: "date"),
                        Role = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Donates",
                c => new
                    {
                        DonateID = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        BloodType = c.Int(nullable: false),
                        RhMarker = c.Boolean(nullable: false),
                        DonorPESEL = c.Long(nullable: false),
                        Available = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DonateID);
            
            CreateTable(
                "dbo.Donors",
                c => new
                    {
                        PESEL = c.Long(nullable: false),
                        Firstname = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                        Birthdate = c.DateTime(nullable: false, storeType: "date"),
                        BloodType = c.Int(nullable: false),
                        RhMarker = c.Boolean(nullable: false),
                        BloodGiven = c.Double(nullable: false),
                        Email = c.String(maxLength: 50),
                        PhoneNumber = c.Int(),
                    })
                .PrimaryKey(t => t.PESEL);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        AcceptorID = c.Int(nullable: false),
                        DonateID = c.String(nullable: false),
                        OrderDate = c.DateTime(nullable: false, storeType: "date"),
                        BloodType = c.Short(),
                        RhMarker = c.Boolean(),
                        Amount = c.Double(),
                        Send = c.Boolean(),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.Stats",
                c => new
                    {
                        BloodType = c.String(nullable: false, maxLength: 10),
                        TotalAmount = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.BloodType);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stats");
            DropTable("dbo.Orders");
            DropTable("dbo.Donors");
            DropTable("dbo.Donates");
            DropTable("dbo.Credentials");
            DropTable("dbo.Acceptors");
        }
    }
}
