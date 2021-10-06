using System.Text.Json.Serialization;

//Счетчик электрической энергии
namespace WebService.Models
{
    public class ElectricityMeter : Device
    {
        public int? ElectricityMeteringPointID { get; set; }
        
        [JsonIgnore]
        public virtual ElectricityMeteringPoint ElectricityMeteringPoint { get; set; }
    }
}