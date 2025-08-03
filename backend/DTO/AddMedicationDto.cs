using MediTrack.Types;

namespace MediTrack.DTO;

public class AddMedicationDto
{
    public string Name { get; set; }    
    public string Description { get; set; }
    public string DosageForm { get; set; }

    public Medication CreateMedication()
    {
        return new Medication()
        {
            Name = this.Name,
            Description = this.Description,
            DosageForm = this.DosageForm
        };
    }
}