using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediTrack.Types;

namespace MediTrack.DTO;

public class UpdateUserDto
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    [DefaultValue("Patient")]
    public UserRole Role { get; set; }
    [DefaultValue(false)]
    public bool IsBanned { get; set; } // todo: в целом ок, но мне кажется можно создать отдельную ручку: api/users/ban
}