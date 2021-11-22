namespace LesegaisParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WoodDealsChangeMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ReportWoodDeals", "SellerName", c => c.String(maxLength: 150));
            AlterColumn("dbo.ReportWoodDeals", "DealNumber", c => c.String(maxLength: 120));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReportWoodDeals", "DealNumber", c => c.String(maxLength: 100));
            AlterColumn("dbo.ReportWoodDeals", "SellerName", c => c.String(maxLength: 100));
        }
    }
}
