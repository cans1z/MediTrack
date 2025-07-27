using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediTrack.Types;

namespace MediTrack.DTO;

public class UpdateDto
{
    [Required]
    public string Email { get; set; }
    public string? NewName { get; set; }
    public string? NewPassword { get; set; }
    public string? NewEmail { get; set; }
    [DefaultValue("Patient")]
    public UserRole? NewRole { get; set; }
    [DefaultValue(false)]
    public bool? IsBanned { get; set; }
}