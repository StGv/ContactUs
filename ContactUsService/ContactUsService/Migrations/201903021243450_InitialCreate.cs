namespace ContactUsService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        ReceivedOn = c.DateTime(nullable: false),
                        Customer_FirstName = c.String(),
                        Customer_LastName = c.String(),
                        Customer_Email = c.String(),
                        CustomerId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomerMessages");
        }
    }
}
