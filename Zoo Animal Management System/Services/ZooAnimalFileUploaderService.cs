using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Newtonsoft.Json;
using Zoo_Animal_Management_System.Dto;
using Zoo_Animal_Management_System.Models;
using Zoo_Animal_Management_System.Requests;
using Zoo_Animal_Management_System.Services.Adapters;
using Zoo_Animal_Management_System.Services.Repository;
using static Zoo_Animal_Management_System.Enums;

namespace Zoo_Animal_Management_System.Services
{
    public class ZooAnimalFileUploaderService : IZooAnimalFileUploaderService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IEnclosureRepository _enclosureRepository;
        private readonly IEnclosureAdapter _enclosureAdapter;
        private readonly IAnimalAdapter _animalAdapter;
        private readonly ILogger<AnimalDistributionService> _logger;

        public ZooAnimalFileUploaderService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository, IEnclosureAdapter enclosureAdapter, IAnimalAdapter animalAdapter, ILogger<AnimalDistributionService> logger)
        {
            _animalRepository = animalRepository;
            _enclosureRepository = enclosureRepository;
            _enclosureAdapter = enclosureAdapter;
            _animalAdapter = animalAdapter;
            _logger = logger;
        }
        public async Task<IActionResult> UploadFile<TDto>(string fileNamePrefix, FileUploadRequest request)
        {
            IFormFile file = request.File;
            if (file == null || file.Length == 0)
                return new BadRequestObjectResult("File does not exist or is empty");

            string expectedFileName = fileNamePrefix.ToLower().Trim();

            if (!file.FileName.ToLower().StartsWith(expectedFileName))
                return new BadRequestObjectResult($"Incorrect file name, should start with '{expectedFileName}'.");

            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var content = await reader.ReadToEndAsync();
                    var dtoFromJson = JsonConvert.DeserializeObject<TDto>(content);

                    if (dtoFromJson == null)
                    {
                        return new BadRequestObjectResult($"Cannot read {expectedFileName} from file");
                    }

                    return dtoFromJson switch
                    {
                        AnimalCollectionDto animalDto when expectedFileName.Equals(AnimalEnclosure.animal.ToString()) => await UploadAnimals(animalDto),
                        EnclosureCollectionDto enclosureDto when expectedFileName.Equals(AnimalEnclosure.enclosure.ToString()) => await UploadEnclosure(enclosureDto),
                        _ => new BadRequestObjectResult($"Unknown error")
                    };
                }
            }
            catch (JsonException ex)
            {
                return new BadRequestObjectResult($"Error deserializing {ex.Message}");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error {ex.Message}");
            }
        }

        private async Task<IActionResult> UploadAnimals(AnimalCollectionDto animalsFromFile)
        {
            List<Animal> animals = _animalAdapter.BindList(animalsFromFile.Animals);
            bool succesfull = await _animalRepository.AddAnimalList(animals);
            return ReturnMessageOfUpdate(succesfull, "Animals uploaded sucessfully", "Animals not updated");
        }

        private async Task<IActionResult> UploadEnclosure(EnclosureCollectionDto enclosuresFromFile)
        {
            List<Enclosure> enclosures = _enclosureAdapter.BindList(enclosuresFromFile.Enclosures);
            bool succesfull = await _enclosureRepository.AddEnclosureList(enclosures);
            return ReturnMessageOfUpdate(succesfull, "Enclosures uploaded sucessfully", "Enclosures not updated");
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
