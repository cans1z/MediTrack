using System.ComponentModel.DataAnnotations;
using MediTrack.DTO;
using MediTrack.Services;
using MediTrack.Types;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MediTrack.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly UserService _userService;

    public UserController(AuthService authService, UserService userService)
    {
        _authService = authService;
        _userService = userService;
    }
    //create user
    [HttpPost("register")]
    public ActionResult Register([FromQuery] string token, [FromBody]RegisterDto registerUserDto)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
                return BadRequest("You dont have permission to register this user"); 
            _userService.RegisterUser(registerUserDto);
            return NoContent();
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
    //delete user
    [HttpPost("delete")]
    public ActionResult Delete([FromQuery] string token, [FromBody]DeleteDto deleteUserDto)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
                return BadRequest("You dont have permission to delete this user");
            _userService.DeleteUser(deleteUserDto);
            return NoContent();
        }
        catch (UserNotFoundException unfex)
        {
            return NotFound(unfex.Message);
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
    
    //update user
    [HttpPatch("update")]
    public ActionResult UpdateUser([FromQuery] string token, [FromBody] UpdateDto updateUserDto)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
                return BadRequest("You dont have permission to update this user");
            _userService.UpdateUser(updateUserDto);
            return NoContent();
        }
        catch (UserNotFoundException unfex)
        {
            return NotFound(unfex.Message);
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
    
    //get users
    
}