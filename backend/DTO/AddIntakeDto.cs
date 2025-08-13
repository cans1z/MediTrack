using MediTrack.Types;
using System.ComponentModel.DataAnnotations;

namespace MediTrack.DTO;

public class AddIntakeDto
{
    [Required(ErrorMessage = "PrescriptionId is required")]
    public int PrescriptionId { get; set; }

    [Required(ErrorMessage = "DateTaken is required")]
    public DateTime DateTaken { get; set; }

    [Required(ErrorMessage = "Status is required")]
    [RegularExpression("^(Taken|Missed)$", ErrorMessage = "Status must be either 'Taken' or 'Missed'")]
    public string Status { get; set; }

    [MaxLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
    public string? Comment { get; set; }
    public IntakeRecord CreateIntakeRecord()
    {
        return new IntakeRecord()
        {
            DateTaken = this.DateTaken,
            Status = this.Status,
            Comment = this.Comment ?? string.Empty,
        };
    }
}