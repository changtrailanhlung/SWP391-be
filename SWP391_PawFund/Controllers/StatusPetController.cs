using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entities;
using ServiceLayer.Interfaces;
using ServiceLayer.RequestModels;
using ServiceLayer.ResponseModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWP391_PawFund.Controllers
{
    [Route("api/StatusPet")]
    [ApiController]
    public class StatusPetController : ControllerBase
    {
        private readonly IStatusPetService _statusPetService;

        public StatusPetController(IStatusPetService statusPetService)
        {
            _statusPetService = statusPetService;
        }

        // GET: api/StatusPet/pet/{petId}
        [HttpGet("Pet/{petId}")]
        public ActionResult<IEnumerable<StatusResponseModel>> GetStatusesForPet(int petId)
        {
            var statuses = _statusPetService.GetStatusesForPet(petId);

            if (statuses == null || !statuses.Any())
            {
                return NotFound($"No statuses found for PetId {petId}.");
            }

            var response = statuses.Select(s => new StatusResponseModel
            {
                Id = s.Id,
                Date = s.Date,
                Disease = s.Disease,
                Vaccine = s.Vaccine,
                PetId = s.Pet?.Id ?? 0,
                PetName = s.Pet?.Name ?? string.Empty
            });

            return Ok(response);
        }

        // GET: api/StatusPet/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusDetailResponseModel>> GetStatusById(int id)
        {
            var status = await _statusPetService.GetStatusByIdAsync(id);
            if (status == null)
            {
                return NotFound($"Status with ID {id} not found.");
            }
            var pet = status.Pet;

            var response = new StatusDetailResponseModel
            {
                Id = status.Id,
                Date = status.Date,
                Disease = status.Disease,
                Vaccine = status.Vaccine,
                PetId = pet?.Id ?? 0,
                PetName = pet?.Name ?? string.Empty,
                Pet = pet != null ? new PetResponseModel
                {
                    Type = status.Pet.Type,
                    Breed = status.Pet.Breed,
                    Gender = status.Pet.Gender,
                    Age = (int)status.Pet.Age,
                    Size = status.Pet.Size,
                    Color = status.Pet.Color,
                    AdoptionStatus = status.Pet.AdoptionStatus,
                    Image = status.Pet.Image,
                    ShelterID = status.Pet.ShelterID,
                    UserID = (int)status.Pet.UserID
                } : null!
            };

            return Ok(response);
        }

        // POST: api/StatusPet
        [HttpPost("Create_PetStatus")]
        public async Task<IActionResult> CreateStatus([FromBody] StatusRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Kiểm tra xem PetId có tồn tại không và lấy PetName
            var pet = _statusPetService.GetStatusesForPet(request.PetId);
            if (pet == null)
            {
                return BadRequest($"Pet with ID {request.PetId} does not exist.");
            }
            var status = new Status
            {
                Date = request.Date,
                Disease = request.Disease,
                Vaccine = request.Vaccine,
                PetId = request.PetId
            };

            await _statusPetService.CreateStatusAsync(status);

            // Lấy lại Status vừa tạo với thông tin Pet đã được nạp
            var createdStatus = await _statusPetService.GetStatusByIdAsync(status.Id);
            if (createdStatus == null)
            {
                return StatusCode(500, "An error occurred while creating the status.");
            }

            var response = new StatusResponseModel
            {
                Id = createdStatus.Id,
                Date = createdStatus.Date,
                Disease = createdStatus.Disease,
                Vaccine = createdStatus.Vaccine,
                PetId = createdStatus.Pet?.Id ?? 0,
                PetName = createdStatus.Pet?.Name ?? string.Empty
            };

            return CreatedAtAction(nameof(GetStatusById), new { id = status.Id }, response);
        }

        // PUT: api/StatusPet/{id}
        [HttpPut("Update_PetStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusRequestModel request)
        {
            if (id != request.PetId)
            {
                return BadRequest("Status ID mismatch.");
            }

            var existingStatus = await _statusPetService.GetStatusByIdAsync(id);
            if (existingStatus == null)
            {
                return NotFound($"Status with ID {id} not found.");
            }
            // Kiểm tra xem PetId mới có tồn tại không
            var pet = _statusPetService.GetStatusesForPet(request.PetId);
            if (pet == null)
            {
                return BadRequest($"Pet with ID {request.PetId} does not exist.");
            }
            existingStatus.Date = request.Date;
            existingStatus.Disease = request.Disease;
            existingStatus.Vaccine = request.Vaccine;
            try
            {
                await _statusPetService.UpdateStatusAsync(existingStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the status.");
            }

            return NoContent();
        }

        // DELETE: api/StatusPet/{id}
        [HttpDelete("Remove_PetStatus/{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await _statusPetService.GetStatusByIdAsync(id);
            if (status == null)
            {
                return NotFound($"Status with ID {id} not found.");
            }

            try
            {
                await _statusPetService.DeleteStatusAsync(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the status.");
            }

            return NoContent();
        }
    }
}
