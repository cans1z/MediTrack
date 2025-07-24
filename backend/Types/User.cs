namespace MediTrack.Types;

public enum UserRole { Administrator, Doctor, Patient }
public enum IntakeStatus { Taken, Missed }

public class User
{
    public int Id { get; set; } 
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public bool IsBanned { get; set; }
    public bool IsDeleted { get; set; }
    
    public ICollection<Session> Sessions { get; set; }
}