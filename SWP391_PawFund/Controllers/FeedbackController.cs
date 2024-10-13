using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.RequestModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWP391_PawFund.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // GET: api/Feedback
        [HttpGet]
        public IActionResult GetAllFeedbacks()
        {
            var feedbacks = _feedbackService.GetAllFeedbacks();
            return Ok(feedbacks);
        }

        // GET: api/Feedback/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackById(int id)
        {
            try
            {
                var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
                return Ok(feedback);
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

        // POST: api/Feedback
        [HttpPost("Create_FeedBack")]
        [Authorize(Roles = "Admin, User")] // Chỉ Admin và User mới được tạo Feedback
        public async Task<IActionResult> CreateFeedback([FromBody] FeedBackRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdFeedback = await _feedbackService.CreateFeedbackAsync(request);
                return CreatedAtAction(nameof(GetFeedbackById), new { id = createdFeedback.FeedbackId }, createdFeedback);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while creating the feedback." });
            }
        }

        // PUT: api/Feedback/{id}
        [HttpPut("Update_FeedBack/{id}")]
        [Authorize(Roles = "Admin, User")] // Chỉ Admin và User mới được cập nhật Feedback
        public async Task<IActionResult> UpdateFeedback(int id, [FromBody] FeedBackRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedFeedback = await _feedbackService.UpdateFeedbackAsync(id, request);
                return Ok(updatedFeedback);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while updating the feedback." });
            }
        }

        // DELETE: api/Feedback/{id}
        [HttpDelete("Remove_FeedBack/{id}")]
        [Authorize(Roles = "Admin")] // Chỉ Admin mới được xóa Feedback
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            try
            {
                await _feedbackService.DeleteFeedbackAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the feedback." });
            }
        }
    }
}
