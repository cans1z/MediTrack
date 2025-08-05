using MediTrack.DTO;
using MediTrack.Types;

namespace MediTrack.Services;

public class MedicationNotFoundException : Exception
{
    public MedicationNotFoundException() : base("Medication not found") {}
}
public class MedicationService
{
    public List<Medication> GetMedications()
    {
        using (var db = new ApplicationContext())
        {
            var medications = db.Medications.ToList();
            return medications;
        }
    }

    public Medication GetMedication(int medicationId)
    {
        using (var db = new ApplicationContext())
        {
            var medication = db.Medications.FirstOrDefault(x => x.Id == medicationId);
            if (medication == null)
                throw new MedicationNotFoundException();
            return medication;
        }
    }

    public void AddMedication(AddMedicationDto addMedicationDto)
    {
        using (var db = new ApplicationContext())
        {
            var newMedication = addMedicationDto.CreateMedication();
            db.Medications.Add(newMedication);
            db.SaveChanges();
        }
    }
    
    public void DeleteMedication(int medicationId)
    {
        using (var db = new ApplicationContext())
        {
            var medicationToDelete = db.Medications.FirstOrDefault(m => m.Id == medicationId);

            if (medicationToDelete == null) 
                throw new MedicationNotFoundException();
            db.Medications.Remove(medicationToDelete);
            db.SaveChanges();
        } 
    }

    public void UpdateMedication(UpdateMedicationDto dto, int medicationId)
    {
        using var db = new ApplicationContext();
        var medication = db.Medications.FirstOrDefault(m => m.Id == medicationId);
        if (medication == null)
            throw new MedicationNotFoundException();

        medication.Name = string.IsNullOrWhiteSpace(dto.Name) ? medication.Name : dto.Name;
        medication.Description = string.IsNullOrWhiteSpace(dto.Description) ? medication.Description : dto.Description;
        medication.DosageForm = string.IsNullOrWhiteSpace(dto.DosageForm) ? medication.DosageForm : dto.DosageForm;

        db.SaveChanges();
    }

   
}