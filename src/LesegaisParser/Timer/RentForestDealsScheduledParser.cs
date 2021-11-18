using LesegaisParser.Data;
using LesegaisParser.Data.Providers;
using LesegaisParser.Data.Providers.Interfaces;
using LesegaisParser.Entities;
using LesegaisParser.Intefraces;
using LesegaisParser.Timer.Intefaces;
using Quartz;
using Quartz.Impl;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LesegaisParser.Timer
{
    public class RentForestDealsScheduledParser : 
        IScheduledParser<RentForestAreaParser, ReportWoodDeal>
    {
        private ILesegaisParser<ReportWoodDeal> _parser { get; set; }
        private IDataProvider<ReportWoodDeal> _db { get; set; }

        public async Task StartAsync(int minutes)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create(typeof(RentForestDealsScheduledParser)).Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("ForestDealsScheduledParser", "Group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(minutes)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if(_parser == null)
                _parser = new RentForestAreaParser();
            if (_db == null)
                _db = new WoodDealsDataProvider("Default");

            SimpleLogger.LogInfo("[ScheduledParser] Started parse data...");
            bool needUpdateData = true;
            int currentPage = 0;
            while (needUpdateData)
            {
                var data = await _parser.ParseAsync(50, currentPage);
                using WoodDealsDbContext c = new WoodDealsDbContext("Default");
                data.TryGetData(out var woodDeals);
                int totalDealsCount = 0;
                int existsDealsCount = 0;
                var bufferedDeals = new List<ReportWoodDeal>();
                foreach (var deal in woodDeals)
                {
                    var countInDb = c.WoodDeals.Count((dealDb) => dealDb.DealNumber == deal.DealNumber);
                    if (countInDb < 1)
                        bufferedDeals.Add(deal);
                    else existsDealsCount++;
                    totalDealsCount++;
                }

                if (bufferedDeals.Count > 0)
                    await _db.AddAsync(data);

                if (existsDealsCount > 0)
                    needUpdateData = false;
                else currentPage++;
            }
            SimpleLogger.LogSucc($"[ScheduledParser] Parsed data success");
        }
        
    }
}
