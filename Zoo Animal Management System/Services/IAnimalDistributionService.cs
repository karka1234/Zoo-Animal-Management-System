using Microsoft.AspNetCore.Mvc;
using Zoo_Animal_Management_System.Dto;
using Zoo_Animal_Management_System.Models;

namespace Zoo_Animal_Management_System.Services
{
    public interface IAnimalDistributionService
    {
        Task<IActionResult> UploadAnimals(AnimalCollectionDto animalsFromFile);
        Task<IActionResult> UploadEnclosure(EnclosureCollectionDto enclosuresFromFile);
    }
}