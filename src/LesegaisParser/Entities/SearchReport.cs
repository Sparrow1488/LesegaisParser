using Newtonsoft.Json;

namespace LesegaisParser.Entities
{
    public class SearchReport<T>
    {
        public T[] Content { get; set; }
        public int Total { get; set; }
    }
}
