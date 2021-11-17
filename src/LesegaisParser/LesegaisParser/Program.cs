﻿using LesegaisParser.Data.Providers;
using LesegaisParser.Data.Providers.Interfaces;
using LesegaisParser.Entities;
using LesegaisParser.Intefraces;
using LesegaisParser.Timer;
using LesegaisParser.Timer.Intefaces;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LesegaisParser
{
    public class Program
    {
        public static string Query { get; set; }
        public static void Main()
        {
            StartInitAsync().Wait();

            ILesegaisParser<ReportWoodDeal> parser = new RentForestAreaParser();
            var data = parser.ParseAsync(5, 1).Result;
            if(data.TryGetData(out var receivedData))
            {
                Console.WriteLine("Data received success!");
                foreach (var dataItem in receivedData)
                    Console.WriteLine("BuyerName is " + dataItem.BuyerName);

                IDataProvider<ReportWoodDeal> provider = new RentForestDealsDataProvider("Default");
                provider.AddAsync(data).Wait();
            }
            else Console.WriteLine("Failed to get data");

            Console.ReadKey();
        }

        private static async Task StartInitAsync()
        {
            //Database.Delete("Default");

            var parser = new RentForestDealsScheduledParser();
            await parser.StartAsync(1);
        }
    }
}
