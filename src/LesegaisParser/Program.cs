using LesegaisParser.Data.Providers;
using LesegaisParser.Data.Providers.Interfaces;
using LesegaisParser.Entities;
using LesegaisParser.Intefraces;
using LesegaisParser.Timer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LesegaisParser
{
    public class Program
    {
        public static ILesegaisParser<ReportWoodDeal> WoodDealParser { get; set; }
        public static IDataProvider<ReportWoodDeal> Database { get; set; }
        private static IList<Data<ReportWoodDeal>> BufferedData { get; set; }
        public static void Main()
        {
            // Before beginning work, please create DB local named "LesegaisDb" 
            // and then paste on App.config your connection string

            StartInit();

            var totalSize = WoodDealParser.GetTotalCountAsync().Result;
            SimpleLogger.LogInfo("Total count of values found : " + totalSize);
            SaveAbsolutelyAllValues(totalSize).Wait();              // PARSE ALL DATA FROM SITE

            var parser = new RentForestDealsScheduledParser();  // PARSE VALUES FROM SITE EVERY 10 MINUTES AND UPDATE DB
            parser.StartAsync(10).Wait();

            SimpleLogger.LogInfo("Press any button to exit cmd...");
            Console.ReadKey();
        }

        private static void StartInit()
        {
            //System.Data.Entity.Database.Delete("Default"); // To recreate db
            
            Database = new WoodDealsDataProvider("Default");
            WoodDealParser = new RentForestAreaParser();
            BufferedData = new List<Data<ReportWoodDeal>>();
        }

        private static async Task SaveAbsolutelyAllValues(int totalCount)
        {
            int valuesInRequest = 500;
            int requests = totalCount / valuesInRequest;
            int remainder = totalCount % valuesInRequest;
            SimpleLogger.LogInfo($"Total requests {requests} requests");
            SimpleLogger.LogInfo($"Last request gets {remainder} values");
            SimpleLogger.LogInfo("Started to parse all values...");

            for (int i = 0; i < requests; i++)
            {
                Data<ReportWoodDeal> data;

                if (i == requests - 1)
                    data = await WoodDealParser.ParseAsync(remainder, i);
                else data = await WoodDealParser.ParseAsync(valuesInRequest, i);

                if (data.ContentIsValid())
                {
                    SimpleLogger.LogSucc($"Data gets success ({i + 1}/{requests + 1})");

                    BufferedData.Add(data);

                    if (i % 2 == 0 || i == requests - 2)
                    {
                        AddRangeInDb(BufferedData).Wait();
                        BufferedData = new List<Data<ReportWoodDeal>>();
                    }
                }
                else SimpleLogger.LogError($"Failed to get data. ({i + 1}/{requests + 1})");
            }
            SimpleLogger.LogSucc("All values parsed");
        }

        private static async Task AddRangeInDb(IList<Data<ReportWoodDeal>> dataRange)
        {
            SimpleLogger.LogInfo("Buffered data uploading in Database...");
            var successCount = await Database.AddRangeAsync(dataRange);
            if (successCount != dataRange.Count * dataRange[0].SearchReportWoodDeal.Content.Length)
                SimpleLogger.LogError("Not all data uploaded success!");
            SimpleLogger.LogSucc("Buffered data was upload in Db");
        }
    }
}
