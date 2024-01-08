using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zoo_Animal_Management_System.Services;

namespace Zoo_Animal_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZooAnimalManagerController : ControllerBase
    {
        //get animal info
        //add animal to zoo
        //delete animal from zoo
        //update animal info

        //get enclosure info 
        //add enclosure
        //delete enclosure
        //update enclosure

        //fill enclosures

        private readonly IAnimalDistributionService _distributionService;

        public ZooAnimalManagerController(IAnimalDistributionService distributionService)
        {
            _distributionService = distributionService;
        }

        [HttpPut("FillEnclosures")]
        public async Task<IActionResult> FillEnclosures()
        {
            return await _distributionService.FillEnclosures();
        }

        [HttpGet("GetNotAssignedAnimalsInfo")]
        public async Task<IActionResult> GetNotAssignedAnimalsInfo()
        {
            return await _distributionService.GetNotAssignedAnimalsInfo();
        }

        [HttpPut("AssignOrChangeAnimalEnclosure")]
        public async Task<IActionResult> AssignOrChangeAnimalEnclosure([FromQuery] Guid requestAnimalId, [FromQuery] Guid requestEnclosureId)
        {
            return await _distributionService.AssignOrChangeAnimalEnclosure(requestAnimalId, requestEnclosureId);
        }

        [HttpDelete("DeleteAnimal/{id}")]
        public async Task<IActionResult> DeleteAnimal([FromRoute] Guid id)
        { 
            return await _distributionService.DeleteAnimal(id);
        }
        [HttpPut("RemoveAnimalFromEnclosure")]
        public async Task<IActionResult> RemoveAnimalFromEnclosure([FromQuery] Guid requestAnimalId)
        {
            return await _distributionService.RemoveAnimalFromEnclosure(requestAnimalId);
        }
    }
}
