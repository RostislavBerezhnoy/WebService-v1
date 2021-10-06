using System.Collections.Generic;
using System.Text.Json.Serialization;

//Дочерняя организация
namespace WebService.Models
{
    public class SubOrganisation : Object
    {
        public int? OrganisationID { get; set; }

        [JsonIgnore]
        public virtual Organisation Organisation { get; set; }
        public ICollection<ConsumptionObject> ConsumptionObjects { get; set; } = new List<ConsumptionObject>();
    }
}