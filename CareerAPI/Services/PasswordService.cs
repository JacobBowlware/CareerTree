using Microsoft.AspNetCore.Identity;
using CareerAPI.Models;

namespace CareerAPI.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<User> _passwordHasher;
    }
}
