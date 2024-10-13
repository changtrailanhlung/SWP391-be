using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entities;
using ServiceLayer.Interfaces;
using ServiceLayer.RequestModels;
using ServiceLayer.ResponseModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWP391_PawFund.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class CertificationController : ControllerBase
    {
        private readonly ICertificationService _certificationService;

        public CertificationController(ICertificationService certificationService)
        {
            _certificationService = certificationService;
        }

        // GET: api/Certification
        [HttpGet]
        public ActionResult<IEnumerable<CertificationResponseDetail>> GetAllCertificates()
        {
            var certificates = _certificationService.GetAllCertificates();
            return Ok(certificates);
        }

        // GET: api/Certification/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CertificationResponseDetail>> GetCertificateById(int id)
        {
            try
            {
                var certificate = await _certificationService.GetCertificateByIdAsync(id);
                return Ok(certificate);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // POST: api/Certification
        [HttpPost]
        [Authorize(Roles = "Admin, ShelterStaff")] // Chỉ Admin và ShelterStaff mới được tạo Certification
        public async Task<IActionResult> CreateCertificate([FromForm] CertificationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdCertification = await _certificationService.CreateCertificateAsync(request);
                return CreatedAtAction(nameof(GetCertificateById), new { id = createdCertification.Id }, createdCertification);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while creating the certification." });
            }
        }

        // PUT: api/Certification/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, ShelterStaff")] // Chỉ Admin và ShelterStaff mới được cập nhật Certification
        public async Task<IActionResult> UpdateCertificate(int id, [FromForm] CertificationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedCertification = await _certificationService.UpdateCertificateAsync(id, request);
                return Ok(updatedCertification);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while updating the certification." });
            }
        }

        // DELETE: api/Certification/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            try
            {
                await _certificationService.DeleteCertificateAsync(id);
                return Ok(new { message = "Certification has been deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the certification." });
            }
        }
    }

}
