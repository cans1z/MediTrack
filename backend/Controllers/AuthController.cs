using MediTrack.Services;
using MediTrack.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediTrack.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginDto data)
    {
        using var db = new ApplicationContext();
        var user = db.Users.FirstOrDefault(i => i.UserName == data.Username && i.Password == data.Password);
        if (user == null || user.IsBanned) return Unauthorized();

        var oldSessions = db.Sessions.Where(s => s.UserId == user.Id && s.IsActive);
        foreach (var s in oldSessions) s.IsActive = false;

        var newSession = new Session
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            IsActive = true,
            AuthDate = DateTime.UtcNow
        };

        db.Sessions.Add(newSession);
        db.SaveChanges();

        return Ok(new { token = newSession.Token });
    }

    [HttpGet("fetch")]
    public ActionResult FetchProfile(string token)
    {
        try
        {
            var user = AuthService.ValidateToken(token);
            return Ok(user);
        }
        catch (UnauthorizedException unex)
        {
            return Unauthorized(unex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}

public class LoginDto
{
    public string Username { get; set; }    
    public string Password { get; set; }
}
