using Microsoft.EntityFrameworkCore.Update.Internal;
using Zoo_Animal_Management_System.Dto;
using Zoo_Animal_Management_System.Models;

namespace Zoo_Animal_Management_System.Services.Adapters
{
    public class EnclosureAdapter : IEnclosureAdapter
    {
        public EnclosureDto Bind(Enclosure enclosure)
        {
            return new EnclosureDto()
            {
                Name = enclosure.Name,
                Size = enclosure.Size,
                Location = enclosure.Location,
                Objects = enclosure.EnclosureObjects,
            };
        }
        public Enclosure Bind(EnclosureDto enclosureDto)
        {
            return new Enclosure()
            {
                Name = enclosureDto.Name,
                Size = enclosureDto.Size,
                Location = enclosureDto.Location,
                EnclosureObjects = enclosureDto.Objects,
            };
        }

        public List<Enclosure> BindList(List<EnclosureDto> enclosureDto)
        {
            var list = new List<Enclosure>();
            enclosureDto.ForEach(e =>
            {
                list.Add(Bind(e));
            });
            return list;
        }
    }
}