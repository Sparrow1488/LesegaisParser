using Kernel.Layer;
using System.Collections;

namespace LesegaisParser
{
    public class Program
    {
        public static void Main()
        {
            var deal = new Woodreportdeals();
            var manager = new WoodreportdealsManager();
            var array = new ArrayList();
            array.Add(deal);

            var key = "item-key";
            manager.SaveObject(key, array);                       // сохранение в БД
            var receivedObject = manager.GetObject(key);  // извлечение из БД
        }
    }
}
