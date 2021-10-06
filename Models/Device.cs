using System;
using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public abstract class Device
    {
        public int ID { get; set; }

        [Required]
        public string No { get; set; }
        public string Type { get; set; }

        public string ElectricityMeteringPointName { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Verification Date")]
        public DateTime? VerificationDate { get; set; }
    }
}