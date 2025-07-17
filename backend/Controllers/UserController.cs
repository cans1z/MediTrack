using System.ComponentModel.DataAnnotations;
using MediTrack.Services;
using MediTrack.Types;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MediTrack.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    //create user
    [HttpPost("register")]
    public ActionResult Register([FromQuery] string token, [FromBody]RegisterDto registerUserDto)
    {
        try
        {
            var user = AuthService.ValidateToken(token);
            if (user.Role != "Administrator")
                return BadRequest("You dont have permission to register this user");
            using (var db = new ApplicationContext())
            {
                var newUser = registerUserDto.CreateUser();
                db.Users.Add(newUser);
                db.SaveChanges();
                return NoContent();
            } 
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
    
    //update user
    
    //get users
}
    
public class RegisterDto
{
    public string UserName { get; set; }    
    public string Password { get; set; }
    [Required]
    [RegularExpression("^(Administrator|Doctor|Patient)$",
        ErrorMessage = "Роль должна быть: Administrator, Doctor или Patient")]
    public string Role { get; set; }

    public User CreateUser()
    {
        return new User
            {UserName = this.UserName, Password = this.Password, Role = this.Role};
    }
    
}