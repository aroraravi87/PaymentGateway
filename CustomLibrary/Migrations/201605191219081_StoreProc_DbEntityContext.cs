namespace CustomLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreProc_DbEntityContext : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Customer_Insert",
                p => new
                    {
                        FirstName = p.String(),
                        LastName = p.String(),
                        EmailID = p.String(),
                        Address = p.String(),
                        ContactNo = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Customers]([FirstName], [LastName], [EmailID], [Address], [ContactNo])
                      VALUES (@FirstName, @LastName, @EmailID, @Address, @ContactNo)
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[Customers]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID]
                      FROM [dbo].[Customers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.Customer_Update",
                p => new
                    {
                        ID = p.Int(),
                        FirstName = p.String(),
                        LastName = p.String(),
                        EmailID = p.String(),
                        Address = p.String(),
                        ContactNo = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Customers]
                      SET [FirstName] = @FirstName, [LastName] = @LastName, [EmailID] = @EmailID, [Address] = @Address, [ContactNo] = @ContactNo
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Customer_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Customers]
                      WHERE ([ID] = @ID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Customer_Delete");
            DropStoredProcedure("dbo.Customer_Update");
            DropStoredProcedure("dbo.Customer_Insert");
        }
    }
}
