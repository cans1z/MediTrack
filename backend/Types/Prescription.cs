using System.ComponentModel.DataAnnotations.Schema;

namespace MediTrack.Types;

public class Prescription
{
    public int Id { get; set; } 
    public int DoctorId { get; set; } // todo: Foreign
    public int PatientId { get; set; } // todo: Foreign
    public int MedicationId { get; set; } // todo: Foreign
    public string Dosage { get; set; }
    public int Frequency { get; set; }
    public DateTime  StartDate { get; set; }
    public int Period { get; set; }
    public bool IsFlexible { get; set; }
    public string Comment { get; set; }
}