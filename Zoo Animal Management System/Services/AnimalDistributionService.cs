using Microsoft.AspNetCore.Mvc;
using Zoo_Animal_Management_System.Database;
using Zoo_Animal_Management_System.Dto;
using Zoo_Animal_Management_System.Models;
using Zoo_Animal_Management_System.Services.Adapters;
using Zoo_Animal_Management_System.Services.Repository;

namespace Zoo_Animal_Management_System.Services
{
    public class AnimalDistributionService : IAnimalDistributionService
    {
        private readonly ZooDbContext _context;
        private readonly IAnimalRepository _animalRepository;
        private readonly IEnclosureRepository _enclosureRepository;
        private readonly IEnclosureAdapter _enclosureAdapter;
        private readonly IAnimalAdapter _animalAdapter;
        private readonly ILogger<AnimalDistributionService> _logger;
        public AnimalDistributionService(ZooDbContext context, IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository, IEnclosureAdapter enclosureAdapter, IAnimalAdapter animalAdapter, ILogger<AnimalDistributionService> logger)
        {
            _context = context;
            _animalRepository = animalRepository;
            _enclosureRepository = enclosureRepository;
            _enclosureAdapter = enclosureAdapter;
            _animalAdapter = animalAdapter;
            _logger = logger;
        }
        public async Task<IActionResult> UploadAnimals(AnimalCollectionDto animalsFromFile)
        {
            List<Animal> animals = _animalAdapter.BindList(animalsFromFile.Animals);
            bool succesfull = await _animalRepository.AddAnimalList(animals);
            if (succesfull)
            {
                return new OkObjectResult(animals);
            }
            return new BadRequestObjectResult(animals);
        }

        public async Task<IActionResult> UploadEnclosure(EnclosureCollectionDto enclosuresFromFile)
        {
            List<Enclosure> enclosures = _enclosureAdapter.BindList(enclosuresFromFile.Enclosures);
            bool succesfull = await _enclosureRepository.AddEnclosureList(enclosures);
            if (succesfull)
            {
                return new OkObjectResult(enclosures);
            }
            return new BadRequestObjectResult(enclosures);
        }


    }
}
