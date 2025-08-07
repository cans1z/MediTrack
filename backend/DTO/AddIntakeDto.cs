using MediTrack.Types;

namespace MediTrack.DTO;

public class AddIntakeDto
{
    public DateTime DateTaken { get; set; } 
    public string Status { get; set; }
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