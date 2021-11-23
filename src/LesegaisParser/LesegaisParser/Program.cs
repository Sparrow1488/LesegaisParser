using Kernel.Tools.Logging;
using System;
using System.Threading;

namespace LesegaisParser
{
    public class Program
    {
        private static readonly WoodDealsParser Parser = new WoodDealsParser();

        public static void Main()
        {
            while (true)
            {
                Parser.ParseItemsCount = 200;
                var total = Parser.GetTotalSizeAsync().GetAwaiter().GetResult();
                ParseAbsolutelyAllData(total);

                Logger.Log("Перерыв 10 минут");
                Console.WriteLine();
                Thread.Sleep(new TimeSpan(0, 10, 0));
            }

            Console.ReadKey();
        }

        private static void ParseAbsolutelyAllData(int totalSize)
        {
            var requestsCount = totalSize / Parser.ParseItemsCount;
            var lastRequestCount = totalSize % Parser.ParseItemsCount;    // последний запрос - исключение

            for (int i = 0; i < requestsCount; i++)
            {
                var deals = Parser.ParseAsync().GetAwaiter().GetResult();
                if (deals.Length > 0)
                    Logger.Success($"Сделки успешно получены ({i + 1}/{requestsCount + 2})");
                else Logger.Error($"Возникла ошибка в ходе получения сделок, либо же в ответ ничего не пришло ({i + 1}/{requestsCount + 2})");
               
                Parser.ParseCurrentPage++;
            }

            // последний запрос
            Parser.ParseCurrentPage = requestsCount + 1;
            Parser.ParseItemsCount = lastRequestCount;
            var lastDeals = Parser.ParseAsync().GetAwaiter().GetResult();
            if (lastDeals.Length > 0)
                Logger.Success($"Все сделки успешно получены ({requestsCount + 1}/{requestsCount + 1})");
            else
                Logger.Error($"Возникла ошибка в ходе получения ответа ({requestsCount + 1}/{requestsCount + 1})");
        }
    }
}
