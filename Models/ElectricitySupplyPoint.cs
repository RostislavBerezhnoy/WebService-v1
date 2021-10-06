using System.Collections.Generic;
using System.Text.Json.Serialization;

//Точка поставки электроэнергии
namespace WebService.Models
{
    public class ElectricitySupplyPoint : Point
    {
        public float MaxPowerKW { get; set; }
        public int? ConsumptionObjectID { get; set; }
        
        [JsonIgnore]
        public virtual ConsumptionObject ConsumptionObject { get; set; }

        [JsonIgnore]
        public ICollection<MeteringDevice> MeteringDevice { get; set; } = new List<MeteringDevice>();
    }
}