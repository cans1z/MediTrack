using System.ComponentModel;
using MediTrack.Types;
using System.ComponentModel.DataAnnotations;
namespace MediTrack.DTO;



public class RegisterUserDto
{
    [Required(ErrorMessage = "Username is required")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters long")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [DefaultValue(UserRole.Patient)]
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

