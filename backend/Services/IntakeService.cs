using MediTrack.DTO;
using MediTrack.Types;
using Microsoft.EntityFrameworkCore;

namespace MediTrack.Services;

public class IntakeService
{
    //get intake records by user
    public List<IntakeRecord> GetIntakeRecords(int prescriptionId, User user)
    {
        using (var db = new ApplicationContext())
        {
            var prescription = db.Prescriptions
                .Where(p => p.Id == prescriptionId)
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .FirstOrDefault();
            
            if (prescription == null)
                throw new Exception("Prescription not found");

            if (user.Id != prescription.DoctorId && user.Id != prescription.PatientId)
                throw new Exception("Access denied");
            
            return db.IntakeRecords
                .Where(i => i.PrescriptionId == prescriptionId)
                .OrderByDescending(d => d.DateTaken)
                .ToList();
        }
    }

    //add intake record
    public void AddIntakeRecord(AddIntakeDto dto, User user, int prescriptionId)
    {
        using (var db = new ApplicationContext())
        {
            var prescription = db.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .FirstOrDefault(p => p.Id == prescriptionId);
            
            if (user.Id != prescription.DoctorId && !(user.Id == prescription.PatientId && prescription.IsFlexible))
                throw new Exception("You are not allowed to modify this intake record");
            
            var newPrescription = dto.CreateIntakeRecord();
            newPrescription.PrescriptionId = prescriptionId;
            db.IntakeRecords.Add(newPrescription);
            db.SaveChanges();
        }
    }
    
    //update intake record
    // todo: переделай, если хочешь обновление учета приема медикамента - обновляй по id
    // public void UpdateIntakeRecord(AddIntakeDto dto, User user, int prescriptionId)
    // {
    //     using (var db = new ApplicationContext())
    //     {
    //         var prescription = db.Prescriptions
    //             .Include(p => p.Patient)
    //             .Include(p => p.Doctor)
    //             .FirstOrDefault(p => p.Id == prescriptionId);
    //         
    //         if(user.Id != prescription.DoctorId &&
    //            !(user.Id == prescription.PatientId && prescription.IsFlexible))
    //         {
    //             throw new Exception("You are not allowed to modify this intake record");
    //         }
    //         
    //         var existingIntake = db.IntakeRecords.FirstOrDefault(r => 
    //             r.PrescriptionId == dto.PrescriptionId && r.DateTaken == dto.DateTaken);
    //         if (existingIntake != null)
    //         {
    //             existingIntake.Status = dto.Status;
    //             existingIntake.Comment = dto.Comment ?? string.Empty;
    //         }
    //         else
    //         {
    //             throw new Exception("There is no such intake record");
    //         }
    //
    //         db.SaveChanges();
    //     }
    // }
    
}