namespace LesegaisParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WoodDealsChangeMaxValues : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ReportWoodDeals", "SellerName", c => c.String(maxLength: 100));
            AlterColumn("dbo.ReportWoodDeals", "SellerInn", c => c.String(maxLength: 128));
            AlterColumn("dbo.ReportWoodDeals", "BuyerName", c => c.String(maxLength: 100));
            AlterColumn("dbo.ReportWoodDeals", "BuyerInn", c => c.String(maxLength: 128));
            AlterColumn("dbo.ReportWoodDeals", "DealNumber", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReportWoodDeals", "DealNumber", c => c.String());
            AlterColumn("dbo.ReportWoodDeals", "BuyerInn", c => c.String());
            AlterColumn("dbo.ReportWoodDeals", "BuyerName", c => c.String());
            AlterColumn("dbo.ReportWoodDeals", "SellerInn", c => c.String());
            AlterColumn("dbo.ReportWoodDeals", "SellerName", c => c.String());
        }
    }
}
