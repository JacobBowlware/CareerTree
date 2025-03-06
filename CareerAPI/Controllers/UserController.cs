using Microsoft.AspNetCore.Mvc;
using CareerAPI.Models;
using AutoMapper;

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
