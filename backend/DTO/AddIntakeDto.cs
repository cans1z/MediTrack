using MediTrack.Types;

namespace MediTrack.DTO;

public class AddIntakeDto
{
    public int PrescriptionId { get; set; }
    public DateTime DateTaken { get; set; } 
    public string Status { get; set; }
    public string? Comment { get; set; }

    public IntakeRecord CreateIntakeRecord()
    {
        return new IntakeRecord()
        {
            PrescriptionId = this.PrescriptionId,
            DateTaken = this.DateTaken,
            Status = this.Status,
            Comment = this.Comment ?? string.Empty,
        };
    }
}