using System.Text.Json.Serialization;

//Трансформатор напряжения
namespace WebService.Models
{
    public class VoltageTransformer : Device
    {
        public int? ElectricityMeteringPointID { get; set; }
        
        [JsonIgnore]
        public virtual ElectricityMeteringPoint ElectricityMeteringPoint { get; set; }
        public float? TransformationRatio { get; set; }
    }
}