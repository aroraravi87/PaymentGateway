namespace CustomLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbEntityContext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ContactNo", c => c.Int(nullable: false));

           

        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "ContactNo");
            DropStoredProcedure("dbo.GetCustomer_Details");
        }
    }
}
