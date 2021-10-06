using System.Collections.Generic;
using System.Text.Json.Serialization;

// Объект потребления
namespace WebService.Models
{
    public class ConsumptionObject : Object
    {
        public int? SubOrganisationID { get; set; }

        [JsonIgnore]
        public virtual SubOrganisation SubOrganisation { get; set; }
        public ICollection<ElectricityMeteringPoint> ElectricityMeteringPoint { get; set; } = new List<ElectricityMeteringPoint>();
        public ICollection<ElectricitySupplyPoint> ElectricitySupplyPoint { get; set; } = new List<ElectricitySupplyPoint>();
    }
}