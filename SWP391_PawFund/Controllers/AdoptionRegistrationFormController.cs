using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ModelLayer.Entities;
using ServiceLayer.Interfaces;
using ServiceLayer.RequestModels;
using ServiceLayer.ResponseModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWP391_PawFund.Controllers
{
    [Route("api/AdoptionRegistrationForm")]
    [ApiController]
    public class AdoptionRegistrationFormController : ControllerBase
    {
        private readonly IAdoptionRegistrationFormService _adoptionFormService;
        private readonly IShelterService _shelterService;
        private readonly IUsersService _usersService;
        private readonly IPetService _petService;
        private readonly IAuthServices _authServices;
        private readonly IFileUploadService _fileUploadService;
        private readonly ILogger<AdoptionRegistrationFormController> _logger;

        public AdoptionRegistrationFormController(
            IAdoptionRegistrationFormService adoptionFormService,
            IUsersService usersService,
            IShelterService shelterService,
            IPetService petService,
            IAuthServices authServices,
            IFileUploadService fileUploadService,
            ILogger<AdoptionRegistrationFormController> logger)
        {
            _adoptionFormService = adoptionFormService;
            _usersService = usersService;
            _shelterService = shelterService;
            _petService = petService;
            _authServices = authServices;
            _fileUploadService = fileUploadService;
            _logger = logger;
        }


        // GET: api/AdoptionRegistrationForm
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<AdoptionRegistrationFormResponse>> GetAllForms()
        {
            var forms = _adoptionFormService.GetAllAdoptionForms();

            var response = forms.Select(form => new AdoptionRegistrationFormResponse
            {
                Id = form.Id,
                SocialAccount = form.SocialAccount,
                IncomeAmount = form.IncomeAmount,
                IdentificationImage = form.IdentificationImage,
                IdentificationImageBackSide = form.IdentificationImageBackSide,
                AdopterId = form.AdopterId,
                ShelterStaffId = form.ShelterStaffId,
                PetId = form.PetId
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id}/detail")]
        [Authorize]
        public async Task<ActionResult<AdoptionRegistrationFormDetailResponse>> GetFormDetailById(int id)
        {
            try
            {
                var form = await _adoptionFormService.GetAdoptionFormByIdAsync(id);
                if (form == null)
                {
                    return NotFound(new { message = "Form not found." });
                }

                var adopter = await _usersService.GetUserByIdAsync(form.AdopterId);
                var pet = await _petService.GetPetById(form.PetId);
                var shelterStaff = await _usersService.GetUserByIdAsync((int)form.ShelterStaffId);
                var shelter = pet != null ? await _shelterService.GetShelterByID(pet.ShelterID) : null;

                var response = new AdoptionRegistrationFormDetailResponse
                {
                    Id = form.Id,
                    SocialAccount = form.SocialAccount,
                    IncomeAmount = form.IncomeAmount,
                    IdentificationImage = form.IdentificationImage,
                    IdentificationImageBackSide = form.IdentificationImageBackSide,
                    Status = form.Status
                };

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error in GetFormDetailById with id: {Id}", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in GetFormDetailById with id: {Id}", id);
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }


        // POST: api/AdoptionRegistrationForm
        [HttpPost("Create_Adopte_Form")]
        [Authorize]
        public async Task<IActionResult> CreateForm([FromForm] AdoptionRegistrationFormRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Upload images to Firebase or any file storage and get URLs
            var identificationImageUrl = await _fileUploadService.UploadFileAsync(request.IdentificationImage);
            var identificationImageBackSideUrl = await _fileUploadService.UploadFileAsync(request.IdentificationImageBackSide);

            var form = new AdoptionRegistrationForm
            {
                SocialAccount = request.SocialAccount,
                IncomeAmount = request.IncomeAmount,
                IdentificationImage = identificationImageUrl, // Store the image URLs
                IdentificationImageBackSide = identificationImageBackSideUrl,
                AdopterId = request.AdopterId,
                ShelterStaffId = request.ShelterStaffId,
                PetId = request.PetId,
                Status = false
            };

            await _adoptionFormService.CreateAdoptionFormAsync(form);

            var response = new AdoptionRegistrationFormResponse
            {
                Id = form.Id,
                SocialAccount = form.SocialAccount,
                IncomeAmount = form.IncomeAmount,
                IdentificationImage = form.IdentificationImage,
                IdentificationImageBackSide = form.IdentificationImageBackSide,
                AdopterId = form.AdopterId,
                ShelterStaffId = form.ShelterStaffId,
                PetId = form.PetId
            };

            return CreatedAtAction(nameof(GetFormDetailById), new { id = form.Id }, response);
        }

        // PUT: api/AdoptionRegistrationForm/{id}
        [HttpPut("Update_Adopte_Form/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateForm(int id, [FromForm] AdoptionRegistrationFormRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingForm = await _adoptionFormService.GetAdoptionFormByIdAsync(id);
            if (existingForm == null)
            {
                return NotFound(new { message = "Form not found." });
            }

            // Upload new images if provided
            if (request.IdentificationImage != null)
            {
                existingForm.IdentificationImage = await _fileUploadService.UploadFileAsync(request.IdentificationImage);
            }

            if (request.IdentificationImageBackSide != null)
            {
                existingForm.IdentificationImageBackSide = await _fileUploadService.UploadFileAsync(request.IdentificationImageBackSide);
            }

            // Update other fields
            existingForm.SocialAccount = request.SocialAccount;
            existingForm.IncomeAmount = request.IncomeAmount;
            existingForm.AdopterId = request.AdopterId;
            existingForm.ShelterStaffId = request.ShelterStaffId;
            existingForm.PetId = request.PetId;
            existingForm.Status = request.Status;

            await _adoptionFormService.UpdateAdoptionFormAsync(existingForm);

            return Ok(new { message = "Form has been updated successfully." });
        }

        // DELETE: api/AdoptionRegistrationForm/{id}
        [HttpDelete("Remove_Adopte_Form/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteForm(int id)
        {
            var form = await _adoptionFormService.GetAdoptionFormByIdAsync(id);
            if (form == null)
            {
                return NotFound(new { message = "Form not found." });
            }

            await _adoptionFormService.DeleteAdoptionFormAsync(id);
            return Ok(new { message = "Form has been deleted successfully." });
        }
    }

}
