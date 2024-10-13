 using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entities;
using ServiceLayer.Interfaces;
using ServiceLayer.RequestModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWP391_PawFund.Controllers
{
    [Route("api/Pet")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        // GET: api/Pet
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> GetAllPets()
        {
            var pets = _petService.GetPets();
            return Ok(pets);
        }

        // GET: api/Pet/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPetById(int id)
        {
            var pet = await _petService.GetPetById(id);
            if (pet == null)
            {
                return NotFound();
            }
            return Ok(pet);
        }

        // POST: api/Pet
        [HttpPost("Create_Pet")]
        public async Task<IActionResult> CreatePet([FromBody] PetCreateRequestModel petCreateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Tạo một thực thể Pet mới từ PetCreateRequestModel
            var pet = new Pet
            {
                ShelterID = petCreateRequest.ShelterID,
                UserID = petCreateRequest.UserID,
                Name = petCreateRequest.Name,
                Type = petCreateRequest.Type,
                Breed = petCreateRequest.Breed,
                Gender = petCreateRequest.Gender,
                Age = petCreateRequest.Age,
                Size = petCreateRequest.Size,
                Color = petCreateRequest.Color,
                Description = petCreateRequest.Description,
                AdoptionStatus = petCreateRequest.AdoptionStatus,
                StatusId = petCreateRequest.StatusId,
                Image = petCreateRequest.Image
            };
            await _petService.CreatePetAsync(pet);

            return CreatedAtAction(nameof(GetPetById), new { id = pet.Id }, petCreateRequest);
        }


        // PUT: api/Pet/{id}
        [HttpPut("Update_Pet/{id}")]
        public async Task<IActionResult> UpdatePet(int id, [FromBody] PetUpdateRequestModel updatedPet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Lấy pet hiện có từ database bằng id
            var existingPet = await _petService.GetPetById(id);

            if (existingPet == null)
            {
                return NotFound($"Pet with Id = {id} not found.");
            }

            existingPet.Name = updatedPet.Name;
            existingPet.Type = updatedPet.Type;
            existingPet.Breed = updatedPet.Breed;
            existingPet.Gender = updatedPet.Gender;
            existingPet.Age = updatedPet.Age;
            existingPet.Size = updatedPet.Size;
            existingPet.Color = updatedPet.Color;
            existingPet.Description = updatedPet.Description;
            existingPet.AdoptionStatus = updatedPet.AdoptionStatus;

            if (!string.IsNullOrEmpty(updatedPet.Image))
            {
                existingPet.Image = updatedPet.Image;
            }

            await _petService.UpdatePetAsync(existingPet);
            return Ok(new {message= "Pet updated successfully." });
        }


        // DELETE: api/Pet/{id}
        [HttpDelete("Remove_Pet/{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var pet = await _petService.GetPetById(id);
            if (pet == null)
            {
                return NotFound(new { message = "Pet not found." });
            }
            try
            {
                await _petService.DeletePetAsync(id);
                return Ok(new { message = "Pet have been Delete Successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PATCH: api/Pet/{id}/status
        [HttpPatch("{id}/Update_Pet_AdopteStatus")]
        public async Task<IActionResult> UpdatePetStatus(int id, [FromQuery] int newStatus)
        {
            var pet = await _petService.GetPetById(id);
            if (pet == null)
            {
                return NotFound(new { message = "Pet not found." });
            }

            await _petService.UpdatePetStatus(pet, newStatus);
            return Ok(new { message = "Pet Status have been Updated successfully." });
        }
    }
}
