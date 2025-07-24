namespace MediTrack.Types;

public class Medication
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Description { get; set; }
    public string DosageForm { get; set; }
    public bool IsDeleted { get; set; }
}