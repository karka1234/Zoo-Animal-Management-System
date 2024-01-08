using System.ComponentModel.DataAnnotations;

namespace Zoo_Animal_Management_System.Dto
{
    public class AnimalDto
    {
        public string Species { get; set; } = string.Empty;
        public string Food { get; set; } = string.Empty;
        public int Amount { get; set; }
    }
}
