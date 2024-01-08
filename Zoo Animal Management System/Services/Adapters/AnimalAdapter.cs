using Zoo_Animal_Management_System.Dto;
using Zoo_Animal_Management_System.Models;

namespace Zoo_Animal_Management_System.Services.Adapters
{
    public class AnimalAdapter : IAnimalAdapter
    {
        public AnimalDto Bind(Animal animal)
        {
            return new AnimalDto()
            {
                Species = animal.Species,
                Food = animal.Food,
                Amount = animal.Amount,
            };
        }
        public Animal Bind(AnimalDto animalDto)
        {
            return new Animal()
            {
                Species = animalDto.Species,
                Food = animalDto.Food,
                Amount = animalDto.Amount,
            };
        }

        public List<Animal> BindList(List<AnimalDto> animalDto)
        {
            var list = new List<Animal>();
            animalDto.ForEach(a =>
            {
                list.Add(Bind(a));
            });
            return list;
        }
    }
}