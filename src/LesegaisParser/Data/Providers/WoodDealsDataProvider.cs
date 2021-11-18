using LesegaisParser.Data.Providers.Interfaces;
using LesegaisParser.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace LesegaisParser.Data.Providers
{
    public class WoodDealsDataProvider : IDataProvider<ReportWoodDeal>
    {
        private readonly string _connectionStringName;

        public WoodDealsDataProvider(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        public async Task AddAsync(Data<ReportWoodDeal> entity)
        {
            using var db = new WoodDealsDbContext(_connectionStringName);
            if (entity == null)
                throw new NullReferenceException($"{nameof(entity)} was null!");

            if(entity.TryGetData(out var woodDeals))
            {
                woodDeals = Serialize(woodDeals);

                db.WoodDeals.AddRange(woodDeals);
                await db.SaveChangesAsync();
            }
        }

        /// <returns>Count of success uploaded entities</returns>
        public async Task<int> AddRangeAsync(IEnumerable<Data<ReportWoodDeal>> entities)
        {
            using var db = new WoodDealsDbContext(_connectionStringName);
            if (entities == null)
                throw new NullReferenceException($"{nameof(entities)} was null!");

            foreach (var data in entities)
            {
                if (data.TryGetData(out var woodDeals))
                {
                    woodDeals = Serialize(woodDeals);
                    db.WoodDeals.AddRange(woodDeals);
                }
            }
            return await db.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IEnumerable<ReportWoodDeal> entities)
        {
            using var db = new WoodDealsDbContext(_connectionStringName);
            if (entities == null)
                throw new NullReferenceException($"{nameof(entities)} was null!");

            entities = Serialize(entities);
            foreach (var deal in entities)
            {
                entities = Serialize(entities);
                db.WoodDeals.AddRange(entities);
            }
            return await db.SaveChangesAsync();
        }

        public ReportWoodDeal Get(int id)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<ReportWoodDeal> Serialize(IEnumerable<ReportWoodDeal> deals)
        {
            foreach (var deal in deals)
                deal.JsonView = JsonConvert.SerializeObject(deal);
            return deals;
        }
    }
}
