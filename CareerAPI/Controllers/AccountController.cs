using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CareerAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using CareerAPI.DTOs;

namespace CareerAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 IConfiguration configuration,
                                 IPasswordHasher<User> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp([FromBody] SignupDto signupDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(signupDto.Email);
            if (existingUser != null)
            {
                return BadRequest("User already exists.");
            }

            var user = new User
            {
                Email = signupDto.Email,
                UserName = signupDto.Email,
                NormalizedUserName = signupDto.Email.ToUpper(),
                Skills = []
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, signupDto.Password);

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest($"Error creating user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid email or password.");
        }

        // Generate JWT token
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}