namespace CarRentalDemo.Web.DataContexts.CarRentalMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarBrand",
                c => new
                    {
                        CarBrendID = c.Int(nullable: false, identity: true),
                        BrendName = c.String(nullable: false),
                        BrendImage = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CarBrendID);
            
            CreateTable(
                "dbo.Car",
                c => new
                    {
                        CarID = c.Int(nullable: false, identity: true),
                        CarBrendID = c.Int(nullable: false),
                        CarDescription = c.String(nullable: false, maxLength: 50),
                        CarImage = c.String(nullable: false),
                        CarRegistrationDate = c.DateTime(nullable: false),
                        PricePerDay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Available = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CarID)
                .ForeignKey("dbo.CarBrand", t => t.CarBrendID, cascadeDelete: true)
                .Index(t => t.CarBrendID);
            
            CreateTable(
                "dbo.Rent",
                c => new
                    {
                        RentID = c.Int(nullable: false, identity: true),
                        CarID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        DateOfRent = c.DateTime(nullable: false),
                        DateOfReturn = c.DateTime(nullable: false),
                        PayMetod = c.String(nullable: false),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Returned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RentID)
                .ForeignKey("dbo.Car", t => t.CarID, cascadeDelete: true)
                //.ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.CarID)
                .Index(t => t.UserID);
            AddForeignKey("dbo.Rent", "UserID", "dbo.AspNetUsers", "Id");
            //CreateTable(
            //    "dbo.AspNetUsers",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            FirstName = c.String(nullable: false),
            //            SurName = c.String(nullable: false),
            //            PhoneNumber = c.String(nullable: false),
            //            DrivingLicenseNumber = c.String(),
            //            Email = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DrivingLicen",
                c => new
                    {
                        DrivingLicenID = c.Int(nullable: false, identity: true),
                        LicenNumber = c.String(),
                    })
                .PrimaryKey(t => t.DrivingLicenID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rent", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rent", "CarID", "dbo.Car");
            DropForeignKey("dbo.Car", "CarBrendID", "dbo.CarBrand");
            DropIndex("dbo.Rent", new[] { "UserID" });
            DropIndex("dbo.Rent", new[] { "CarID" });
            DropIndex("dbo.Car", new[] { "CarBrendID" });
            DropTable("dbo.DrivingLicen");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Rent");
            DropTable("dbo.Car");
            DropTable("dbo.CarBrand");
        }
    }
}
