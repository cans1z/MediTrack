namespace MediTrack.DTO;

public class UpdateIntakeDto
{
    public DateTime? DateTaken { get; set; } 
    public string? Status  { get; set; } //Taken, Missed
    public string? Comment { get; set; }
    
    public int? PrescriptionId { get; set; } 
}