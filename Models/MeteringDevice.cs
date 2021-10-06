using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

//Расчетный прибор учета
namespace WebService.Models
{
    public class MeteringDevice
    {
        public int ID { get; set; }
        
        [Required]
        public string No { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [JsonIgnore]
        public int? ElectricitySupplyPointID { get; set; }

        [JsonIgnore]
        public virtual ElectricitySupplyPoint ElectricitySupplyPoints { get; set; }

        [JsonIgnore]
        public ICollection<ElectricityMeteringPoint> ElectricityMeteringPoint { get; set; } = new List<ElectricityMeteringPoint>();
    }
}