namespace MediTrack.Types;

public enum UserRole { Administrator, Doctor, Patient }
public enum IntakeStatus { Taken, Missed } // todo: это точно нужно в файле User.cs? когда проект разрастется будет сложно в нем ориентироваться

// todo: прописать ДТО-response для User, без Password и Sessions
public class User
{
    public int Id { get; set; } 
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public bool IsBanned { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    
    public ICollection<Session> Sessions { get; set; } 
}