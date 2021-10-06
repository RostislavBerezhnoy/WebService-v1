using System.Text.Json.Serialization;

//Трансформатор тока
namespace WebService.Models
{
    public class ElectricalTransformer : Device
    {
        public int? ElectricityMeteringPointID { get; set; }
        [JsonIgnore]
        public ElectricityMeteringPoint ElectricityMeteringPoint { get; set; }
        public float? TransformationRatio { get; set; }
    }
}