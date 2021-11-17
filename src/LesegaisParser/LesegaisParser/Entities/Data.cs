using System.Collections.Generic;

namespace LesegaisParser.Entities
{
    public class Data<T>
    {
        public SearchReportWoodDeal<T> SearchReportWoodDeal { get; set; }
        public bool TryGetData(out IEnumerable<T> outData)
        {
            bool dataExists = false;
            outData = null;
            if (SearchReportWoodDeal?.Content != null)
            {
                outData = SearchReportWoodDeal.Content;
                dataExists = true;
            }
            return dataExists;
        }
    }
}
