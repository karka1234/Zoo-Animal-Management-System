using Microsoft.AspNetCore.Mvc;
using Zoo_Animal_Management_System.Models;
using Zoo_Animal_Management_System.Services.Repository;
using static Zoo_Animal_Management_System.Enums;

namespace Zoo_Animal_Management_System.Services
{
    public class AnimalDistributionService : IAnimalDistributionService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IEnclosureRepository _enclosureRepository;
        private readonly ILogger<AnimalDistributionService> _logger;

        public AnimalDistributionService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository, ILogger<AnimalDistributionService> logger)
        {
            _animalRepository = animalRepository;
            _enclosureRepository = enclosureRepository;
            _logger = logger;
        }
        public async Task<IActionResult> FillEnclosures()
        {
            List<Animal> animals = await _animalRepository.GetAllAnimalsWifouthEnclosure();
            List<Enclosure> enclosures = await _enclosureRepository.GetAllEnclosures();
            if (!animals.Any() && !enclosures.Any())
                return new BadRequestObjectResult("Theres no assigned animals or any of enclosures");

            List<Animal> herbivoreGroups = animals.Where(a => a.Food == AnimalFood.Herbivore).ToList();
            AssignHerbivoresToEnclosures(herbivoreGroups, enclosures);

            List<Animal> carnivoreGroups = animals.Where(a => a.Food == AnimalFood.Carnivore).ToList();
            AssignCarnivoresToEnclosures(carnivoreGroups, enclosures);


            bool succesfull = await _animalRepository.UpdateAnimals();
            return ReturnMessageOfUpdate(succesfull, "Animals successfully assigned to enclosures.", "Animals was not assigned to enclosures");
        }
        public async Task<IActionResult> GetNotAssignedAnimalsInfo()
        {
            List<Animal> animals = await _animalRepository.GetAllAnimalsWifouthEnclosure();
            if (animals.Count != 0)
                return new OkObjectResult(animals);
            else
                return new BadRequestObjectResult("Theres no free animals in database");
        }
        public async Task<IActionResult> AssignOrChangeAnimalEnclosure(Guid animalId, Guid enclosureId)
        {
            Animal animal = await _animalRepository.GetAnimalById(animalId);
            if (animal == null)
            {
                return new BadRequestObjectResult("Theres no animal with such id");
            }
            Enclosure enclosure = await _enclosureRepository.GetEnclosureById(enclosureId);
            if (enclosure == null)
            {
                return new BadRequestObjectResult("Theres no enclosure with such id");
            }

            animal.Enclosure = enclosure;

            bool succesfull = await _animalRepository.UpdateAnimals();
            return ReturnMessageOfUpdate(succesfull, "Animal uploaded sucessfully", "Animal enclosure not updated");
        }

        public async Task<IActionResult> DeleteAnimal(Guid animalId)
        {
            Animal animal = await _animalRepository.GetAnimalById(animalId);
            if (animal == null)
            {
                return new BadRequestObjectResult("Theres no animal with such id");
            }

            bool succesfull = await _animalRepository.DeleteAnimal(animal);
            return ReturnMessageOfUpdate(succesfull, "Animal was deleted sucessfully", "Animal still exists");
        }


        public async Task<IActionResult> RemoveAnimalFromEnclosure(Guid animalId)
        {
            Animal animal = await _animalRepository.GetAnimalById(animalId);
            if (animal == null)
            {
                return new BadRequestObjectResult("Theres no animal with such id");
            }

            animal.Enclosure = null;

            bool succesfull = await _animalRepository.UpdateAnimals();
            return ReturnMessageOfUpdate(succesfull, "Animal was removed from enclosure sucessfully", "Animal still exists in enclosure");
        }





        private void AssignHerbivoresToEnclosures(List<Animal> herbivores, List<Enclosure> enclosures)
        {
            // Only assign if enclosure size is Huge and theres is less than 10 animals or if large and theres less than 8 animals
            foreach (var herbivore in herbivores)
            {
                List<Enclosure> suitableEnclosures = GetSuitableHerbivoreEnclosuresHugeEnclosure(enclosures);
                if (!suitableEnclosures.Any())
                {
                    suitableEnclosures = GetSuitableHerbivoreEnclosuresLArgeEnclosure(enclosures);
                }
                suitableEnclosures.First().Animals.Add(herbivore);
            }
        }
        private List<Enclosure> GetSuitableHerbivoreEnclosuresHugeEnclosure(List<Enclosure> enclosures)
        {
            return enclosures
                .Where(enclosure =>
                    (enclosure.Size == EnclosureSize.Huge && enclosure.Animals.Select(animalsInEnclosure => animalsInEnclosure.Amount).Sum() < 10))
                .ToList();
        }
        private List<Enclosure> GetSuitableHerbivoreEnclosuresLArgeEnclosure(List<Enclosure> enclosures)
        {
            return enclosures
                .Where(enclosure =>
                    (enclosure.Size == EnclosureSize.Large && enclosure.Animals.Select(animalsInEnclosure => animalsInEnclosure.Amount).Sum() < 8))
                .ToList();
        }
        private void AssignCarnivoresToEnclosures(List<Animal> carnivores, List<Enclosure> enclosures)
        {
            foreach (var carnivore in carnivores)
            {
                List<Enclosure> suitableEnclosures = GetSuitableCarnivoreEnclosures(enclosures);
                if (suitableEnclosures.Any())
                {
                    suitableEnclosures.First().Animals.Add(carnivore);
                }
            }
        }
        private List<Enclosure> GetSuitableCarnivoreEnclosures(List<Enclosure> enclosures)
        {
            // Only assign if there are less than 2 species in the enclosure or if enclosure is small allow only 1 species and assigning only if enclosures are empty or there is no herbivore
            return enclosures
                .Where(enclosure =>
                ((enclosure.Animals.Count < 2) || (enclosure.Animals.Count < 1 && enclosure.Size == EnclosureSize.Small))
                &&
                (!enclosure.Animals.Any() || (enclosure.Animals.Any(animalsInEnclosure => animalsInEnclosure.Food != AnimalFood.Herbivore))))
                .ToList();
        }
        private IActionResult ReturnMessageOfUpdate(bool succesfull, string okMessage, string errorMessage)
        {
            if (succesfull)
            {
                _logger.LogInformation(okMessage);
                return new OkObjectResult(okMessage);
            }
            _logger.LogError(errorMessage);
            return new BadRequestObjectResult(errorMessage);
        }

    }
}
