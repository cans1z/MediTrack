namespace MediTrack.DTO;

public class LoginUserDto
{
    public string UserName { get; set; } // или убери UserName и используй почту
    public string Password { get; set; }
    public string Email { get; set; } // todo: почту из авторизации убери, если ее ты не используешь
}