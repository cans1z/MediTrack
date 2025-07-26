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
    //create user
    [HttpPost("register")]
    public ActionResult Register([FromQuery] string token, [FromBody]RegisterDto registerUserDto)
    {
        try
        {
            var user = AuthService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
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
    [HttpPost("delete")]
    public ActionResult Delete([FromQuery] string token, [FromBody]DeleteDto deleteUserDto)
    {
        try
        {
            var user = AuthService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
                return BadRequest("You dont have permission to delete this user");
            using (var db = new ApplicationContext())
            {
                var delUser = deleteUserDto;
                var ad = db.Users.FirstOrDefault(u => u.Email == deleteUserDto.Email);
                db.Users.Remove(ad);
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
    
    //update user
    
    //get users
}
    
/*
public class RegisterDto
{
    public string UserName { get; set; }    
    public string Password { get; set; }
    public string Email { get; set; }

    [Required]
    /*[RegularExpression("^(Administrator|Doctor|Patient)$",
        ErrorMessage = "Роль должна быть: Administrator, Doctor или Patient")]#1#
    public UserRole Role { get; set; }

    public User CreateUser()
    {
        return new User
            {
                UserName = this.UserName, 
                Password = this.Password,
                Email = this.Email,
                Role = this.Role};
    }
    */