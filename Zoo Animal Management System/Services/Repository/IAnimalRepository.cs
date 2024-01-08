using Zoo_Animal_Management_System.Models;

namespace Zoo_Animal_Management_System.Services.Repository
{
    public interface IAnimalRepository
    {
        Task<bool> AddAnimal(Animal animal);
        Task<bool> AddAnimalList(List<Animal> animals);
        Task<Animal> GetAnimalById(Guid animalId);
        Task<List<Animal>> GetAllAnimalsWifouthEnclosure();
        Task<bool> UpdateAnimals();
        Task<List<Animal>> GetAllAnimals();
        Task<bool> DeleteAnimal(Animal animal);
    }
}