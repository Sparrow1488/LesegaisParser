using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LesegaisParser.Entities
{
    public class ReportWoodDeal
    {
        public ReportWoodDeal()
        {
            JsonView = JsonConvert.SerializeObject(this);
        }

        [Key]
        public int Id { get; set; }
        public string SellerName { get; set; }
        public string SellerInn { get; set; }
        public string BuyerName { get; set; }
        public string BuyerInn { get; set; }
        public double WoodVolumeBuyer { get; set; }
        public double WoodVolumeSeller { get; set; }
        public DateTime DealDate { get; set; }
        public string DealNumber { get; set; }
        // serialized model as json
        public string JsonView { get; set; }
    }
}
