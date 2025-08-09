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
                throw new Exception("You are not allowed to create intake record");
            
            var newPrescription = dto.CreateIntakeRecord();
            newPrescription.PrescriptionId = prescriptionId;
            db.IntakeRecords.Add(newPrescription);
            db.SaveChanges();
        }
    }
    
    //update intake record
    // todo: переделай, если хочешь обновление учета приема медикамента - обновляй по id
     public void UpdateIntakeRecord(int intakeId, UpdateIntakeDto updateIntakeDto, User user, int prescriptionId)
     {
         using (var db = new ApplicationContext())
         {
             var prescription = db.Prescriptions
                 .Include(p => p.Patient)
                 .Include(p => p.Doctor)
                 .FirstOrDefault(p => p.Id == prescriptionId);
            
             if (user.Id != prescription.DoctorId && !(user.Id == prescription.PatientId && prescription.IsFlexible))
                 throw new Exception("You are not allowed to modify this intake record");
             
             var upIntakeRecord = db.IntakeRecords.Find(intakeId);
             if(upIntakeRecord == null)
                 throw new Exception("Intake record not found");
             upIntakeRecord.PrescriptionId = prescriptionId;
             upIntakeRecord.Comment = updateIntakeDto.Comment;
             upIntakeRecord.Status = updateIntakeDto.Status;
             upIntakeRecord.DateTaken = updateIntakeDto.DateTaken;
             db.IntakeRecords.Update(upIntakeRecord);
             db.SaveChanges();
         }
     }
     
     //delete intake
     public void DeleteIntakeRecord(int intakeId)
     {
         using var db = new ApplicationContext();
         var intake = db.IntakeRecords.Find(intakeId);
         if (intake == null)
             throw new Exception("Intake record not found");
         db.IntakeRecords.Remove(intake);
         db.SaveChanges();
     }
    
}