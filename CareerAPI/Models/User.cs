namespace CareerAPI.Models;
public interface IUser
{
    int Id { get; set; }
    string Email { get; set; }
    // string PasswordHash { get; set; }
}

public class User : IUser
{
    public int Id { get; set; }
    public string Email { get; set; }
    // public string PasswordHash { get; set; }

    public User(int id, string email)
    {
        this.Id = id;
        this.Email = email;
    }
}