using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entities;
using ServiceLayer.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWP391_PawFund.Controllers
{

    [Route("api/Shelter")]
    [ApiController]
    public class ShelterController : ControllerBase
    {
        private readonly IShelterService _shelterService;

        public ShelterController(IShelterService shelterService)
        {
            _shelterService = shelterService;
        }

        // GET: api/Shelter
        [HttpGet]
        public ActionResult<IEnumerable<Shelter>> GetAllShelters()
        {
            var shelters = _shelterService.GetShelter();
            return Ok(shelters);
        }

        // GET: api/Shelter/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Shelter>> GetShelterById(int id)
        {
            var shelter = await _shelterService.GetShelterByID(id);
            if (shelter == null)
            {
                return NotFound(new { message = "Shelter not found." });
            }
            return Ok(shelter);
        }

        // POST: api/Shelter
        [HttpPost("Create_Shelter")]
        public async Task<IActionResult> CreateShelter([FromForm] Shelter shelter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _shelterService.CreateShelter(shelter);
            return CreatedAtAction(nameof(GetShelterById), new { id = shelter.Id }, shelter);
        }

        // PUT: api/Shelter/{id}
        [HttpPut("Update_Shelter/{id}")]
        public async Task<IActionResult> UpdateShelter(int id, [FromForm] Shelter shelter)
        {
            if (id != shelter.Id)
            {
                return BadRequest("Shelter ID mismatch");
            }

            var existingShelter = await _shelterService.GetShelterByID(id);
            if (existingShelter == null)
            {
                return NotFound(new { message = "Shelter not found." });
            }

            await _shelterService.UpdateShelter(shelter);
            return Ok(new { message = "Shelter have been Updated successfully." });
        }

        // DELETE: api/Shelter/{id}
        [HttpDelete("Remove_Shelter/{id}")]
        public async Task<IActionResult> DeleteShelter(int id)
        {
            var shelter = await _shelterService.GetShelterByID(id);
            if (shelter == null)
            {
                return NotFound(new { message = "Shelter not found." });
            }

            await _shelterService.DeleteShelter(id);
            return Ok(new { message = "Shelter have been Deleted successfully." });
        }
    }
}
