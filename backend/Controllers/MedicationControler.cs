using MediTrack.DTO;
using MediTrack.Extensions;
using MediTrack.Services;
using MediTrack.Types;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MediTrack.Controllers;

[Route("api/medications")]
[ApiController]
public class MedicationControler : ControllerBase
{
    private readonly AuthService _authService;
    private readonly MedicationService _medicationService;

    public MedicationControler(AuthService authService, MedicationService medicationService)
    {
        _authService = authService;
        _medicationService = medicationService;
    }

    
    //get medications
    [HttpGet("list")]
    public ActionResult<List<Medication>> GetMedicatons([FromQuery] string token)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
                return Unauthorized("Admin access required.");

            return Ok(_medicationService.GetMedications());
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
    
    
    //get medication by id
    [HttpGet("{medicationId}")]
    public ActionResult<Medication> GetMedication([FromQuery] string token, [FromRoute] int medicationId)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
                return Unauthorized("Admin access required.");
            return Ok(_medicationService.GetMedication(medicationId));
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
    
    //add medication
    [HttpPost("add")]
    public ActionResult Add([FromQuery] string token, [FromBody] AddMedicationDto addMedicationDto)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
                return Unauthorized("Admin access required.");
            _medicationService.AddMedication(addMedicationDto);
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
    
    //delete medication
    [HttpDelete("delete/{medicationId}")]
    public ActionResult Delete([FromQuery] string token, int medicationId)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
                return Unauthorized("Admin access required.");
            
            _medicationService.DeleteMedication(medicationId);
            
            return NoContent();
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
    
    //update medication
    [HttpPatch("update/{medicationId}")]
    public ActionResult UpdateMedication([FromQuery] string token, [FromBody] UpdateMedicationDto updateMedicationDto,
        [FromRoute] int medicationId)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
                return Unauthorized("Admin access required.");

            _medicationService.UpdateMedication(updateMedicationDto, medicationId);
            return NoContent();
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
}