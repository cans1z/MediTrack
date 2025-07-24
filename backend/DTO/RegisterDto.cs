using MediTrack.Types;

namespace MediTrack.DTO;

public class RegisterDto
{
    public string UserName { get; set; }    
    public string Password { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }

    public User CreateUser()
    {
        return new User
        {
            UserName = this.UserName,
            Email = this.Email,
            Role = this.Role,
            Password = this.Password // plain
        };
    }
}
