namespace LesegaisParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WoodDealsChangePropMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ReportWoodDeals", "BuyerName", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReportWoodDeals", "BuyerName", c => c.String(maxLength: 100));
        }
    }
}
