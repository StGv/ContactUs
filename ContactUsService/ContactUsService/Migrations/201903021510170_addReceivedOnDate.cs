namespace ContactUsService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addReceivedOnDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerMessages", "ReceivedOn", c => c.DateTime(nullable: false));
        //defaultValueSql: "GETUTCDATE()"
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerMessages", "ReceivedOn");
        }
    }
}
