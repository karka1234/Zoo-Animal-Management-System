using Microsoft.EntityFrameworkCore;
using System;
using Zoo_Animal_Management_System.Database;
using Zoo_Animal_Management_System.Models;

namespace Zoo_Animal_Management_System.Services.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ZooDbContext _context;
        public AnimalRepository(ZooDbContext context)
        {
            _context = context;
        }
        private async Task<bool> UpdateAndCheckIfAnyRowsAffected()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            return await UpdateAndCheckIfAnyRowsAffected();
        }

        public async Task<Animal> GetAnimalById(Guid animalId)
        {
            Animal animal = await _context.Animals.Include(animal => animal.Enclosure).FirstOrDefaultAsync(animal => animal.Id == animalId);
            return animal;
        }

        public async Task<bool> AddAnimalList(List<Animal> animals)
        {
            _context.Animals.AddRange(animals);
            return await UpdateAndCheckIfAnyRowsAffected();
        }

        public async Task<List<Animal>> GetAllAnimals()///and wifouth anclosures.. neet to make maybe
        {
            return await _context.Animals.Include(animal => animal.Enclosure).ToListAsync();
        }
    }
}
