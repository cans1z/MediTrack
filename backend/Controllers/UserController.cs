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
    public ActionResult Register([FromQuery] string token, [FromBody]RegisterUserDto registerUserUserDto)
    { // todo: RegisterDto переименуй в RegisterUserDto
        try
        {
            var user = _authService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
                return BadRequest("You dont have permission to register this user"); 
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
    // todo: передавай id в path: api/users/delete/{userId}
    [HttpDelete("delete")]
    public ActionResult Delete([FromRoute] int userId, [FromQuery] string token)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
                return BadRequest("You dont have permission to delete this user");
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
    [HttpPatch("update")] //todo в path передавай id пользователя которого хочешь редактировать
    public ActionResult UpdateUser([FromQuery] string token, [FromBody] UpdateUserDto updateUserUserDto)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
                return BadRequest("You dont have permission to update this user");
            _userService.UpdateUser(updateUserUserDto);
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
    [HttpGet("list")]
    public ActionResult<List<User>> ListUsers([FromQuery] string token)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
                return BadRequest("You dont have permission to get users");
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
    
}