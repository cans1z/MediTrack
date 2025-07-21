namespace MediTrack.Types;

public class IntakeRecords
{
    public int Id { get; set; } 
    public int PrescriptionId { get; set; } 
    public DateTime DateTaken { get; set; } 
    public string Status  { get; set; } //Taken, Missed
    public string Comment { get; set; }
    public Prescriptions Prescription { get; set; }
}