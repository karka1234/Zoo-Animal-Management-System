using Zoo_Animal_Management_System.Dto;
using Zoo_Animal_Management_System.Models;

namespace Zoo_Animal_Management_System.Services.Adapters
{
    public interface IAnimalAdapter
    {
        AnimalDto Bind(Animal animal);
        Animal Bind(AnimalDto animalDto);
        List<Animal> BindList(List<AnimalDto> animalDto);
    }
}