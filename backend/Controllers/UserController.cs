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
    
    //get users
    [HttpGet("list")]
    public ActionResult<List<User>> ListUsers([FromQuery] string token)
    {
        try
        {
            if (!_authService.TryValidateAdmin(token, out _, out var errorMsg))
                return Unauthorized(errorMsg);
            return Ok(_userService.GetUsers());
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
    
    //get user by id
    [HttpGet("{userId}")]
    public ActionResult<User> GetUser([FromQuery] string token, [FromRoute] int userId)
    {
        try
        {
            if (!_authService.TryValidateAdmin(token, out _, out var errorMsg))
                return Unauthorized(errorMsg);
            return Ok(_userService.GetUser(userId));
        }
        catch (MedicationNotFoundException mnfex)
        {
            return NotFound(mnfex.Message);
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
    
    //create user
    [HttpPost("register")]
    public ActionResult Register([FromQuery] string token, [FromBody]RegisterUserDto registerUserUserDto)
    {
        try
        {
            if (!_authService.TryValidateAdmin(token, out _, out var errorMsg))
                return Unauthorized(errorMsg);
            _userService.RegisterUser(registerUserUserDto);
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
    [HttpDelete("delete/{userId}")]
    public ActionResult Delete([FromRoute] int userId, [FromQuery] string token)
    {
        try
        {
            if (!_authService.TryValidateAdmin(token, out _, out var errorMsg))
                return Unauthorized(errorMsg);
            _userService.DeleteUser(userId);
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
    [HttpPatch("update/{userId}")]
    public ActionResult UpdateUser([FromQuery] string token, [FromBody] UpdateUserDto updateUserUserDto, [FromRoute] int userId)
    {
        try
        {
            if (!_authService.TryValidateAdmin(token, out _, out var errorMsg))
                return Unauthorized(errorMsg);
            _userService.UpdateUser(updateUserUserDto, userId);
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
}