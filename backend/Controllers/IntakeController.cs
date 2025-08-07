using MediTrack.DTO;
using MediTrack.Extensions;
using MediTrack.Services;
using MediTrack.Types;
using Microsoft.AspNetCore.Mvc;

namespace MediTrack.Controllers;

[Route("api/intakes")]
[ApiController]
public class IntakeController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly IntakeService _intakeService; 
    
    public IntakeController(AuthService authService, IntakeService intakeService)
    {
        _authService = authService;
        _intakeService = intakeService;
    }
    
    //get intakes
    [HttpGet("list")]
    public ActionResult<List<IntakeRecord>> GetIntakeRecords([FromRoute] int prescriptionId)
    {
        try
        {
            var user = HttpContext.GetUser(_authService);
            return Ok(_intakeService.GetIntakeRecords(prescriptionId, user));
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
    
    //add intakes
    [HttpPost("create")]
    public ActionResult CreateIntakeRecord([FromBody] AddIntakeDto addIntakeDto)
    {
        try
        {
             var user = HttpContext.GetUser(_authService);
            _intakeService.AddIntakeRecord(addIntakeDto, user);
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
}