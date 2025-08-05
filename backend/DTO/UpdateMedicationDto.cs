using MediTrack.Types;

namespace MediTrack.DTO;

public class UpdateMedicationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string DosageForm { get; set; }
}