using Swashbuckle.AspNetCore.Annotations;

namespace CareerAPI.DTOs
{
    public class UserDTO
    {
        [SwaggerSchema("The user's unique identifier.")]
        public string Id { get; }

        [SwaggerSchema("The user's email address.")]
        public string Email { get; set; }

        [SwaggerSchema("A list of the user's skills.")]
        public List<string> Skills { get; set; }
    }
}
