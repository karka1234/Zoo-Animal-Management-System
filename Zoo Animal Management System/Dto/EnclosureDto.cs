using System.ComponentModel.DataAnnotations;
using static Zoo_Animal_Management_System.Enums;

namespace Zoo_Animal_Management_System.Dto
{
    public class EnclosureDto
    {
        public string Name { get; set; } = string.Empty;
        public EnclosureSize Size { get; set; }
        public EnclosureLocation Location { get; set; }
        public List<string> Objects { get; set; } = new List<string>();
    }
}
