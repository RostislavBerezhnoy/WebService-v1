using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public abstract class Object
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
    }
}