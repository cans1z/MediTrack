namespace MediTrack.Types;

public class Prescriptions
{
    public int Id { get; set; } 
    public int DoctorId { get; set; } 
    public int PatientId { get; set; } 
    public int MedicationId { get; set; } 
    public string Dosage { get; set; }
    public int Frequency { get; set; }
    public DateTime  StartDate { get; set; }
    public int Period { get; set; }
    public bool IsFlexible { get; set; }
    public string Comment { get; set; }
}