using Zoo_Animal_Management_System.Dto;
using Zoo_Animal_Management_System.Models;

namespace Zoo_Animal_Management_System.Services.Adapters
{
    public interface IEnclosureAdapter
    {
        EnclosureDto Bind(Enclosure enclosure);
        Enclosure Bind(EnclosureDto enclosureDto);
        List<Enclosure> BindList(List<EnclosureDto> enclosureDto);
    }
}