namespace LesegaisParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WoodDealsChangeAllPropsMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ReportWoodDeals", "SellerName", c => c.String(maxLength: 500));
            AlterColumn("dbo.ReportWoodDeals", "SellerInn", c => c.String(maxLength: 130));
            AlterColumn("dbo.ReportWoodDeals", "BuyerName", c => c.String(maxLength: 500));
            AlterColumn("dbo.ReportWoodDeals", "BuyerInn", c => c.String(maxLength: 130));
            AlterColumn("dbo.ReportWoodDeals", "DealNumber", c => c.String(maxLength: 135));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReportWoodDeals", "DealNumber", c => c.String(maxLength: 120));
            AlterColumn("dbo.ReportWoodDeals", "BuyerInn", c => c.String(maxLength: 128));
            AlterColumn("dbo.ReportWoodDeals", "BuyerName", c => c.String(maxLength: 150));
            AlterColumn("dbo.ReportWoodDeals", "SellerInn", c => c.String(maxLength: 128));
            AlterColumn("dbo.ReportWoodDeals", "SellerName", c => c.String(maxLength: 150));
        }
    }
}
