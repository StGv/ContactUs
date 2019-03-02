namespace ContactUsService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimaryKeys : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Email);
            
            AlterColumn("dbo.CustomerMessages", "Text", c => c.String(maxLength: 2500));
            AlterColumn("dbo.CustomerMessages", "Customer_Email", c => c.String(maxLength: 128));
            CreateIndex("dbo.CustomerMessages", "Customer_Email");
            AddForeignKey("dbo.CustomerMessages", "Customer_Email", "dbo.Customers", "Email");
            DropColumn("dbo.CustomerMessages", "ReceivedOn");
            DropColumn("dbo.CustomerMessages", "Customer_FirstName");
            DropColumn("dbo.CustomerMessages", "Customer_LastName");
            DropColumn("dbo.CustomerMessages", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerMessages", "CustomerId", c => c.String());
            AddColumn("dbo.CustomerMessages", "Customer_LastName", c => c.String());
            AddColumn("dbo.CustomerMessages", "Customer_FirstName", c => c.String());
            AddColumn("dbo.CustomerMessages", "ReceivedOn", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.CustomerMessages", "Customer_Email", "dbo.Customers");
            DropIndex("dbo.CustomerMessages", new[] { "Customer_Email" });
            AlterColumn("dbo.CustomerMessages", "Customer_Email", c => c.String());
            AlterColumn("dbo.CustomerMessages", "Text", c => c.String());
            DropTable("dbo.Customers");
        }
    }
}
