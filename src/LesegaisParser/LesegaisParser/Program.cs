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
            var result = parser.ParseAsync(20, 1).Result;
            if(result.TryGetData(out var receivedData))
            {
                Console.WriteLine("Data received success!");
                foreach (var dataItem in receivedData)
                    Console.WriteLine("BuyerName is " + dataItem.BuyerName);
            }
            else Console.WriteLine("Failed to get data");
        }
    }
}
