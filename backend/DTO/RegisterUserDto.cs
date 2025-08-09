using System.ComponentModel;
using MediTrack.Types;

namespace MediTrack.DTO;

public class RegisterUserDto
{
    public string UserName { get; set; }  // todo: валидация (мин. кол-во символов)
    public string Password { get; set; } // todo: валидация (мин. кол-во символов)
    public string Email { get; set; } // todo: валидация (мин. кол-во символов, regex эл. почты)
    
    [DefaultValue("Patient")]
    public UserRole Role { get; set; }

    public User CreateUser()
    {
        return new User
        {
            UserName = this.UserName,
            Email = this.Email,
            Role = this.Role,
            Password = this.Password
        };
    }
}
