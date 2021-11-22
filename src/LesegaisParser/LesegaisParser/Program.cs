using Kernel.Tools.Logging;
using System;

namespace LesegaisParser
{
    public class Program
    {
        public static void Main()
        {
            var deals = new WoodDealsParser().ParseAsync().GetAwaiter().GetResult();
            if (deals.Length > 0)
            {
                Logger.Success("Сделки успешно получены");
                foreach (var deal in deals)
                    Logger.Log("Покупатель: " + deal.Buyername + "; ИНН: " + deal.Buyerinn);
            }
            else Logger.Error("Возникла ошибка в ходе получения сделок, либо же в ответ ничего не пришло");

            Console.ReadKey();
        }
    }
}
