using Microsoft.AspNetCore.Mvc;
using CareerAPI.Models;
using CareerAPI.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace CareerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly UserDbContext userDbContext;

        public UserController(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult TestConnection()
        {
            return Ok(new
            {
                message = "Backend is connected!"
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var user = await userDbContext.Users.FindAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpPost]
        public async Task<IActionResult> AddSkills([FromQuery] string email, [FromQuery] string[] skills)
        {

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Unauthorized("Invalid email.");
            }

            user.Skills = skills;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest($"Error updating user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return Ok();
        }
    }
}
