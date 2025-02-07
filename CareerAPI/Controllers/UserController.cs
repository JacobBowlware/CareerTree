using Microsoft.AspNetCore.Mvc;
using CareerAPI.Models;

namespace CareerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user.Id > 0 && user.Email is not null)
            {
                return Ok(user);
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            User user = new User(1, "testingGETUSER-IloveJoslyn-Email@gmail.com");
            return Ok(user);
        }
    }
}
