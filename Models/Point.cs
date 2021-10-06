using System.ComponentModel.DataAnnotations;
namespace WebService.Models
{
    public abstract class Point
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}