using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Food { get; set; } = string.Empty;
        [Required]
        public int Amount { get; set; }

        [ForeignKey("Enclosure")]
        public Guid? EnclosureId { get; set; }
        public Enclosure? Enclosure { get; set; }
    }
}
