using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entities;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWP391_PawFund.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly ITwilioRestClient _client;
        public SmsController(ITwilioRestClient client)
        {
            _client = client;
        }

        [HttpPost("send-sms")]
        public IActionResult SendSms([FromForm]SmsMessage model)
        {
            try
            {
                var message = MessageResource.Create(
                    to: new PhoneNumber(model.To),
                    from: new PhoneNumber(model.From),
                    body: model.Message,
                    client: _client
                );
                return Ok("Success " + message.Sid);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }
    }
}
