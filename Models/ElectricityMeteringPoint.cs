using System.Collections.Generic;
using System.Text.Json.Serialization;

//Точка измерения электроенергии
namespace WebService.Models
{
    public class ElectricityMeteringPoint : Point
    {
        public int? ConsumptionObjectID { get; set; }
        public string ConsumptionObjectName { get; set; }

        [JsonIgnore]
        public virtual ConsumptionObject ConsumptionObject { get; set; }

        public virtual VoltageTransformer VoltageTransformers { get; set; }

        public virtual ElectricalTransformer ElectricalTransformers { get; set; }

        [JsonIgnore]
        public int? ElectricityMeterID { get; set; }

        public virtual ElectricityMeter ElectricityMeters { get; set; }

        [JsonIgnore]
        public ICollection<MeteringDevice> MeteringDevice { get; set; } = new List<MeteringDevice>();
    }
}