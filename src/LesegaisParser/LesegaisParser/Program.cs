using LesegaisParser.Data.Providers;
using LesegaisParser.Data.Providers.Interfaces;
using LesegaisParser.Entities;
using LesegaisParser.Intefraces;
using System;

namespace LesegaisParser
{
    public class Program
    {
        public static string Query { get; set; }
        public static void Main()
        {
            ILesegaisParser<ReportWoodDeal> parser = new RentForestAreaParser();
            var data = parser.ParseAsync(20, 1).Result;
            if(data.TryGetData(out var receivedData))
            {
                Console.WriteLine("Data received success!");
                foreach (var dataItem in receivedData)
                    Console.WriteLine("BuyerName is " + dataItem.BuyerName);

                IDataProvider<ReportWoodDeal> provider = new RentForestDealsDataProvider("Default");
                provider.AddAsync(data).Wait();
            }
            else Console.WriteLine("Failed to get data");
        }
    }
}
