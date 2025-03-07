namespace CareerAPI.DTOs
{
    public class UserDTO
    {
        public required string Email { get; set; }
        public required string[] Skills { get; set; }
    }
}