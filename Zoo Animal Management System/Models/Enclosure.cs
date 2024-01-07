using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using static Zoo_Animal_Management_System.Enums;

namespace Zoo_Animal_Management_System.Models
{
    public class Enclosure
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public EnclosureSize Size { get; set; }
        [Required]
        public EnclosureLocation Location { get; set; }
        public List<string> EnclosureObjects { get; set; } = new List<string>();

        public List<Animal> Animals { get; set; }
    }
}
