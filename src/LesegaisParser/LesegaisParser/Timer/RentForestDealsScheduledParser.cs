using LesegaisParser.Data;
using LesegaisParser.Data.Providers;
using LesegaisParser.Data.Providers.Interfaces;
using LesegaisParser.Entities;
using LesegaisParser.Intefraces;
using LesegaisParser.Timer.Intefaces;
using Quartz;
using Quartz.Impl;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LesegaisParser.Timer
{
    public class RentForestDealsScheduledParser : 
        IScheduledParser<RentForestAreaParser, ReportWoodDeal>
    {
        private ILesegaisParser<ReportWoodDeal> _parser { get; set; }
        private IDataProvider<ReportWoodDeal> _db { get; set; }

        public async Task Execute(IJobExecutionContext context)
        {
            if(_parser == null)
                _parser = new RentForestAreaParser();
            if (_db == null)
                _db = new WoodDealsDataProvider("Default");

            SimpleLogger.LogInfo("[ScheduledParser] Started parse data...");
            bool dataAvailable = true;
            int currentPage = 0;
            while (dataAvailable)
            {
                var data = await _parser.ParseAsync(50, currentPage);
                using WoodDealsDbContext c = new WoodDealsDbContext("Default");
                data.TryGetData(out var items);
                int all = 0;
                foreach (var item in items)
                {
                    var a = c.WoodDeals.Count((deal) => deal.DealNumber == item.DealNumber);
                    if (a > 0)
                        all++;
                }
            }
            SimpleLogger.LogSucc("[ScheduledParser] Parsed data success");
        }

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
    }
}
