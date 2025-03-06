using Microsoft.AspNetCore.Mvc;
using CareerAPI.Models;
using AutoMapper;
using CareerAPI.DTOs;

namespace CareerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserDbContext userDbContext;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<UserDTO> CreateUser([FromBody] User user)
        {
            if (user.Email is null || user.PasswordHash is null)
            {
                return BadRequest();
            }

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpGet]
        public ActionResult<UserDTO> GetUser(string id)
        {
            var user = _mapper.Map<UserDTO>(userDbContext.Users.Find(id));

            if (user is null)
            {

                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        public IActionResult TestConnection()
        {
            return Ok(new
            {
                message = "Backend is connected!"
            });
        }
    }
}
