namespace LesegaisParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JsonMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReportWoodDeals", "JsonView", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReportWoodDeals", "JsonView");
        }
    }
}
