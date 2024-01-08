using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Zoo_Animal_Management_System.Enums;

namespace Zoo_Animal_Management_System.Models
{
    public class Animal
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Species { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public AnimalFood Food { get; set; }
        [Required]
        public int Amount { get; set; }

        [ForeignKey("Enclosure")]
        public Guid? EnclosureId { get; set; }
        public Enclosure? Enclosure { get; set; }
    }
}
