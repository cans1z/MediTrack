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
    [HttpGet("list/{prescriptionId}")]
    public ActionResult<List<IntakeRecord>> GetIntakeRecords([FromQuery] string token, [FromRoute] int prescriptionId)
    {
        try
        {
            var user = _authService.ValidateToken(token);
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
    // todo, prescriptionId передавай не в ДТО а в роуте
    [HttpPut("create/{prescriptionId}")] // пока в Query)))
    public ActionResult AddIntakeRecord([FromQuery] string token, int prescriptionId,  [FromBody] AddIntakeDto addIntakeDto)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            _intakeService.AddIntakeRecord(addIntakeDto, user, prescriptionId);
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
    
    //update intakes
    [HttpPatch("update/{intakeId}")]
    public ActionResult UpdateIntakeRecord([FromQuery] string token, int intakeId, int prescriptionId, [FromBody] UpdateIntakeDto updateIntakeDto)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            _intakeService.UpdateIntakeRecord(intakeId, updateIntakeDto, user, prescriptionId);
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
    
    //delete intakes
    [HttpDelete("delete/{intakeId}")]
    public ActionResult DeleteIntakeRecord([FromQuery] string token, int intakeId)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            _intakeService.DeleteIntakeRecord(intakeId);
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