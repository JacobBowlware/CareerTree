using Microsoft.AspNetCore.Identity;

namespace CareerAPI.Models;

public class User : IdentityUser
{
    public string[] Skills { get; set; }
}