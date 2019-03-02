namespace ContactUsService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerMessages", "ReceivedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerMessages", "ReceivedOn", c => c.DateTime(nullable: false));
        }
    }
}
