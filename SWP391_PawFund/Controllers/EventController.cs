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
	public class EventsController : ControllerBase
	{
		private readonly IEventService _eventService;

		public EventsController(IEventService eventService)
		{
			_eventService = eventService;
		}

		// GET: api/Events
		[HttpGet]
		
		public async Task<ActionResult<IEnumerable<EventResponseModel>>> GetEvents()
		{
			var events = await _eventService.GetAllEventsAsync();
			return Ok(events);
		}

		// GET: api/Events/5
		[HttpGet("{id}")]
		
		public async Task<ActionResult<EventResponseModel>> GetEvent(int id)
		{
			try
			{
				var eventItem = await _eventService.GetEventByIdAsync(id);
				return Ok(eventItem);
			}
			catch (Exception ex)
			{
				return NotFound(new { message = ex.Message });
			}
		}

		// POST: api/Events
		[HttpPost]
		[Authorize]
		public async Task<ActionResult> PostEvent(CreateEventRequestModel eventModel)
		{
			try
			{
				await _eventService.CreateEventAsync(eventModel);
				return Ok(new { message = "Event created successfully." });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		// PUT: api/Events/5
		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> PutEvent(int id, UpdateEventRequestModel eventModel)
		{
			try
			{
				await _eventService.UpdateEventAsync(id, eventModel);
				return Ok(new { message = "Event updated successfully." });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		// DELETE: api/Events/5
		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> DeleteEvent(int id)
		{
			try
			{
				await _eventService.DeleteEventAsync(id);
				return NoContent();
			}
			catch (Exception ex)
			{
				return NotFound(new { message = ex.Message });
			}
		}
	}
}