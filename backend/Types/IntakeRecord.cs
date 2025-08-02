namespace MediTrack.Types;

public class IntakeRecord
{
    public int Id { get; set; } 
    public DateTime DateTaken { get; set; } 
    public string Status  { get; set; } //Taken, Missed
    public string Comment { get; set; }
    
    public int PrescriptionId { get; set; } 
    public Prescription Prescription { get; set; }
}