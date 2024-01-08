using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Zoo_Animal_Management_System.Enums;

namespace Zoo_Animal_Management_System.Dto
{
    public class AnimalDto
    {
        public string Species { get; set; } = string.Empty;
        public AnimalFood Food { get; set; }
        public int Amount { get; set; }
    }
}
