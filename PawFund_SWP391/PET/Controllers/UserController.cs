using Bos.Model;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PET.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _dos;

        public UserController(IUserRepo dos)
        {
            _dos = dos;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _dos.GetAll();
            return Ok(user);
        }


        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountByID(int id)
        {
            var user = await _dos.GetUserByID(id);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string name, string password)
        {
            var user = await _dos.Login(name, password);
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            try
            {
                var u = await _dos.AddUser(user);
                return Ok(u);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
