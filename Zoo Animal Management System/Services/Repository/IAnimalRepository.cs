using Zoo_Animal_Management_System.Models;

namespace Zoo_Animal_Management_System.Services.Repository
{
    public interface IAnimalRepository
    {
        Task<bool> AddAnimal(Animal animal);
        Task<bool> AddAnimalList(List<Animal> animals);
        Task<Animal> GetAnimalById(Guid animalId);
    }
}