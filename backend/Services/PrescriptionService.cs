using MediTrack.DTO;
using MediTrack.Types;

namespace MediTrack.Services;

public class PrescriptionService
{
    public List<Prescription> GetPrescriptionsByPatient(int patientId)
    {
        using var db = new ApplicationContext();

        return db.Prescriptions
            .Where(p => p.PatientId == patientId)
            .ToList();
    }

    
    public void AddPrescription(AddPrescriptionDto dto, User doctor)
    {
        using var db = new ApplicationContext();
// todo: проверяй чтобы в patientId был айди только пациента
        var patient = db.Users.FirstOrDefault(u => u.Id == dto.PatientId && u.Role == UserRole.Patient);
        if (patient == null) throw new Exception("Patient not found");

        var medication = db.Medications.FirstOrDefault(m => m.Id == dto.MedicationId && !m.IsDeleted);
        if (medication == null) throw new Exception("Medication not found");

        var prescription = new Prescription
        {
            DoctorId = doctor.Id,
            PatientId = dto.PatientId,
            MedicationId = dto.MedicationId,
            Dosage = dto.Dosage,
            Frequency = dto.Frequency,
            StartDate = dto.StartDate,
            Period = dto.Period,
            IsFlexible = dto.IsFlexible,
            Comment = dto.Comment ?? string.Empty,
        };

        db.Prescriptions.Add(prescription);
        db.SaveChanges();
    }
}