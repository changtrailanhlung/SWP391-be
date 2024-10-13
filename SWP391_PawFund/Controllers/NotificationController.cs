using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.RequestModels;
using ServiceLayer.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP391_PawFund.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationsController : ControllerBase
	{
		private readonly INotificationService _notificationService;

		public NotificationsController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		// GET: api/Notifications
		[HttpGet]
		[Authorize]
		public async Task<ActionResult<IEnumerable<NotificationResponseModel>>> GetNotifications()
		{
			var notifications = await _notificationService.GetAllNotificationsAsync();
			return Ok(notifications);
		}

		// GET: api/Notifications/5
		[HttpGet("{id}")]
		[Authorize]
		public async Task<ActionResult<NotificationResponseModel>> GetNotification(int id)
		{
			try
			{
				var notification = await _notificationService.GetNotificationByIdAsync(id);
				return Ok(notification);
			}
			catch (Exception ex)
			{
				return NotFound(new { message = ex.Message });
			}
		}

		// POST: api/Notifications
		[HttpPost]
		[Authorize]
		public async Task<ActionResult> PostNotification(CreateNotificationRequestModel notificationModel)
		{
			try
			{
				await _notificationService.CreateNotificationAsync(notificationModel);
				return Ok(new { message = "Notification created successfully." });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		// PUT: api/Notifications/5
		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> PutNotification(int id, UpdateNotificationRequestModel notificationModel)
		{
			try
			{
				await _notificationService.UpdateNotificationAsync(id, notificationModel);
				return Ok(new { message = "Notification updated successfully." });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		// DELETE: api/Notifications/5
		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> DeleteNotification(int id)
		{
			try
			{
				await _notificationService.DeleteNotificationAsync(id);
				return NoContent();
			}
			catch (Exception ex)
			{
				return NotFound(new { message = ex.Message });
			}
		}
	}
}
