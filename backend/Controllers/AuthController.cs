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
    public ActionResult Login(LoginDto data)
    {
        using (var db = new ApplicationContext())
        {
            var user = db.Users
                .Where(i => i.Password == data.Password && i.UserName == data.Username)
                .FirstOrDefault();
            if (user == null) return Unauthorized();
            var session = new Session { UserId = user.Id, IsActive = true, Token = $"{Guid.NewGuid()}" };
            db.Sessions.Add(session);
            db.SaveChanges();
            return Ok(session);
        }
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
