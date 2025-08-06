using MediTrack.DTO;
using MediTrack.Services;
using MediTrack.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediTrack.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginUserDto data)
    {
        try
        {
            var token = _authService.Login(data.UserName, data.Password);
            return Ok(new { token });
        }
        catch (UnauthorizedException)
        {
            return Unauthorized("Invalid credentials or banned account.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("fetch")]
    public ActionResult FetchProfile(string token)
    {
        try
        {
            var user = _authService.ValidateToken(token);
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


