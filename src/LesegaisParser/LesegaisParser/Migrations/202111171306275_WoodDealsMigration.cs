namespace LesegaisParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WoodDealsMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportWoodDeals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SellerName = c.String(),
                        SellerInn = c.String(),
                        BuyerName = c.String(),
                        BuyerInn = c.String(),
                        WoodVolumeBuyer = c.Double(nullable: false),
                        WoodVolumeSeller = c.Double(nullable: false),
                        DealDate = c.DateTime(nullable: false),
                        DealNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReportWoodDeals");
        }
    }
}
