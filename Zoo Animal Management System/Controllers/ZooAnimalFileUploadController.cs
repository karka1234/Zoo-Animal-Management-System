using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using Zoo_Animal_Management_System.Dto;
using Zoo_Animal_Management_System.Models;
using Zoo_Animal_Management_System.Requests;
using Zoo_Animal_Management_System.Services;
using Zoo_Animal_Management_System.Services.Adapters;
using static Zoo_Animal_Management_System.Enums;

namespace Zoo_Animal_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZooAnimalFileUploadController : ControllerBase
    {
        private readonly IZooAnimalFileUploaderService _fileUploaderService;
        public ZooAnimalFileUploadController(IZooAnimalFileUploaderService fileUploaderService)
        {
            _fileUploaderService = fileUploaderService;
        }

        [HttpPost("uploadEnclosures")]
        public async Task<IActionResult> UploadEnclosures([FromForm] FileUploadRequest request)
        {
            return await _fileUploaderService.UploadFile<EnclosureCollectionDto>(AnimalEnclosure.enclosure.ToString(), request);
        }

        [HttpPost("uploadAnimals")]
        public async Task<IActionResult> UploadAnimals([FromForm] FileUploadRequest request)
        {
            return await _fileUploaderService.UploadFile<AnimalCollectionDto>(AnimalEnclosure.animal.ToString(), request);
        }  
    }
}
