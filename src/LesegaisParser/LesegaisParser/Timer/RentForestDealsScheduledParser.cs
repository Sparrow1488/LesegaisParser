using LesegaisParser.Entities;
using LesegaisParser.Intefraces;
using LesegaisParser.Timer.Intefaces;
using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace LesegaisParser.Timer
{
    public class RentForestDealsScheduledParser : 
        IScheduledParser<RentForestAreaParser, ReportWoodDeal>
    {
        public ILesegaisParser<ReportWoodDeal> Parser { get; set; }

        public async Task Execute(IJobExecutionContext context)
        {
            if(Parser == null)
                Parser = new RentForestAreaParser();

            Console.WriteLine("[ScheduledParser] Started parse data...");
            var data = await Parser.ParseAsync(20, 1);
            Console.WriteLine("[ScheduledParser] Parsed data");
        }

        public async Task StartAsync(int minutes)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create(typeof(RentForestDealsScheduledParser)).Build();

            ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
                .WithIdentity("ForestDealsScheduledParser", "Group1")     // идентифицируем триггер с именем и группой
                .StartNow()                            // запуск сразу после начала выполнения
                .WithSimpleSchedule(x => x            // настраиваем выполнение действия
                    .WithIntervalInMinutes(minutes)          // через 1 минуту
                    .RepeatForever())                   // бесконечное повторение
                .Build();                               // создаем триггер

            await scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
        }
    }
}
