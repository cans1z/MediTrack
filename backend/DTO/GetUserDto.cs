using MediTrack.Types;

namespace MediTrack.DTO;

public class GetUserDto
{ // todo: id тоже возвращай
    public string UserName { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public bool IsBanned { get; set; }
    
    public static GetUserDto FromUser(Types.User user)
    {
        return new GetUserDto
        {
            UserName = user.UserName,
            Email = user.Email,
            Role = user.Role,
            IsBanned = user.IsBanned
        };
    }
}