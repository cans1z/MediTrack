using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediTrack.Types;

namespace MediTrack.DTO;

public class UpdateUserDto
{
    [Required]
    public string Email { get; set; } // todo: убери это, в path просто передавай id пользователя
    public string? NewName { get; set; } // todo: "New" - не этично в данном случае, аналогично для 3ех остальных
    public string? NewPassword { get; set; }
    public string? NewEmail { get; set; }
    [DefaultValue("Patient")]
    public UserRole? NewRole { get; set; }
    [DefaultValue(false)]
    public bool? IsBanned { get; set; } // todo: в целом ок, но мне кажется можно создать отдельную ручку: api/users/ban
}