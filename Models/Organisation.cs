using System.Collections.Generic;

//Организация
namespace WebService.Models
{
    public class Organisation : Object
    {
       public ICollection<SubOrganisation> SubOrganisations { get; set; } = new List<SubOrganisation>();
    }
}