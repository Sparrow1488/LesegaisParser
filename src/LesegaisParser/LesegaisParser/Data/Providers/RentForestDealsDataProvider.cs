using LesegaisParser.Data.Providers.Interfaces;
using LesegaisParser.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace LesegaisParser.Data.Providers
{
    public class RentForestDealsDataProvider : IDataProvider<ReportWoodDeal>
    {
        private readonly WoodDealsDbContext _db;

        public RentForestDealsDataProvider(string connectionStringName)
        {
            _db = new WoodDealsDbContext(connectionStringName);
        }

        public async Task AddAsync(Data<ReportWoodDeal> entity)
        {
            if (entity == null)
                throw new NullReferenceException($"{nameof(entity)} was null!");

            if(entity.TryGetData(out var woodDeals))
            {
                _db.WoodDeals.AddRange(woodDeals);
                await _db.SaveChangesAsync();
            }
        }

        public Task AddRangeAsync(IEnumerable<Data<ReportWoodDeal>> entities)
        {
            throw new NotImplementedException();
        }

        public ReportWoodDeal Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
