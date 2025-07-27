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
    public UserRole? NewRole { get; set; }
    public bool? IsBanned { get; set; }
}