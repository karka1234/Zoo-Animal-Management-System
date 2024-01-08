using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Column(TypeName = "nvarchar(20)")]
        public EnclosureSize Size { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public EnclosureLocation Location { get; set; }
        public List<string> EnclosureObjects { get; set; } = new List<string>();

        public List<Animal> Animals { get; set; } = new List<Animal> { };
    }
}
