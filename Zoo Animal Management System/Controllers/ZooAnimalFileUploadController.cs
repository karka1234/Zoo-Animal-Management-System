using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Zoo_Animal_Management_System.Dto;
using Zoo_Animal_Management_System.Models;
using Zoo_Animal_Management_System.Requests;
using Zoo_Animal_Management_System.Services;
using Zoo_Animal_Management_System.Services.Adapters;

namespace Zoo_Animal_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZooAnimalFileUploadController : ControllerBase
    {


        
        private readonly IAnimalDistributionService _distributionService;
        private readonly IEnclosureAdapter _enclosureAdapter;
        private readonly IAnimalAdapter _animalAdapter;

        public ZooAnimalFileUploadController(IAnimalDistributionService distributionService, IEnclosureAdapter enclosureAdapter, IAnimalAdapter animalAdapter)
        {
            _distributionService = distributionService;
            _enclosureAdapter = enclosureAdapter;
            _animalAdapter = animalAdapter;
        }

        [HttpPost("uploadEnclosures")]
        public async Task<IActionResult> UploadEnclosures([FromForm] FileUploadRequest request)
        {
            IFormFile file = request.File;
            if (file == null || file.Length == 0)            
                return BadRequest("File not exist or its empty");            
            if (!file.FileName.ToLower().StartsWith("enclosure"))            
                return BadRequest("Incorect file name, should be 'enclosure'.");            
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var content = await reader.ReadToEndAsync();
                    var enclosuresFromJson = JsonConvert.DeserializeObject<EnclosureCollectionDto>(content);
                    if (enclosuresFromJson == null)
                    {
                        return BadRequest("Cannot read enclosures from file");
                    }
                    return await _distributionService.UploadEnclosure(enclosuresFromJson);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error: {ex.Message}", stackTrace = ex.StackTrace });
            }
        }

        [HttpPost("uploadAnimals")]
        public async Task<IActionResult> UploadAnimals([FromForm] FileUploadRequest request)
        {
            IFormFile file = request.File;
            if (file == null || file.Length == 0)            
                return BadRequest("File not exist or its empty");            
            if (!file.FileName.ToLower().StartsWith("animals"))            
                return BadRequest("Incorect file name, should be 'animals'.");            
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var content = await reader.ReadToEndAsync();
                    var animalsFromJson = JsonConvert.DeserializeObject<AnimalCollectionDto>(content);
                    if (animalsFromJson == null)
                    {
                        return BadRequest("Cannot read animals from file");
                    }
                    return await _distributionService.UploadAnimals(animalsFromJson);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error: {ex.Message}", stackTrace = ex.StackTrace });
            }
        }



    }
}
