using LesegaisParser.Entities;
using System.Data.Entity;

namespace LesegaisParser.Data
{
    public class WoodDealsDbContext : DbContext
    {
        public WoodDealsDbContext() { }
        public WoodDealsDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
        public DbSet<ReportWoodDeal> WoodDeals { get; set; }
    }
}
