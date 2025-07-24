namespace MediTrack.Types;

public class Session
{
    public int Id { get; set; }
    public string Token { get; set; }
    public bool IsActive { get; set; }
    public DateTime AuthDate { get; set; } = DateTime.UtcNow;
    
    public int UserId { get; set; }
    public User User { get; set; }  
}