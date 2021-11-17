using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

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
        [MaxLength(100)]
        public string SellerName { get; set; }
        [MaxLength(128)]
        public string SellerInn { get; set; }
        [MaxLength(100)]
        public string BuyerName { get; set; }
        [MaxLength(128)]
        public string BuyerInn { get; set; }
        public double WoodVolumeBuyer { get; set; }
        public double WoodVolumeSeller { get; set; }
        public DateTime DealDate { get; set; }
        [MaxLength(100)]
        public string DealNumber { get; set; }
        // serialized model as json
        public string JsonView { get; set; }
    }
}
