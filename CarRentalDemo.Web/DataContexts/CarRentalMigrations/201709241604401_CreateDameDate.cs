namespace CarRentalDemo.Web.DataContexts.CarRentalMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDameDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rent", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rent", "CreateDate");
        }
    }
}
