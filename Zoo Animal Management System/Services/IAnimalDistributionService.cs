using Microsoft.AspNetCore.Mvc;

namespace Zoo_Animal_Management_System.Services
{
    public interface IAnimalDistributionService
    {
        Task<IActionResult> AssignOrChangeAnimalEnclosure(Guid animalId, Guid enclosureId);
        Task<IActionResult> DeleteAnimal(Guid animalId);
        Task<IActionResult> FillEnclosures();
        Task<IActionResult> GetNotAssignedAnimalsInfo();
        Task<IActionResult> RemoveAnimalFromEnclosure(Guid animalId);
    }
}