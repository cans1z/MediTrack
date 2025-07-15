using MediTrack.Types;
using Microsoft.AspNetCore.Mvc;

namespace MediTrack.Controllers;

[Route("api/test")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok(new User { Id = 12, UserName = "Test" });
    }
    
    [HttpPost("login")]
    public ActionResult Login(LoginDto data)
    {
        using (var db = new ApplicationContext())
        {
            var Kek = db.Users
                .Where(i => i.Password == data.Password && i.UserName == data.Username)
                .FirstOrDefault();
            
            if (Kek != null) return Unauthorized();
            
            
        }
        return 
    }
}

public class LoginDto()
{
    public string Username { get; set; }    
    public string Password { get; set; }
}