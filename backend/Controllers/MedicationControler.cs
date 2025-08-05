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
    public ActionResult<List<Medication>> GetMedicatons()
    {
        try
        {
            if (!HttpContext.IsAdmin(_authService, out _))
                return Unauthorized("Admin access required.");

            return Ok(_medicationService.GetMedications());
            //todo move to medication service
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
    public ActionResult<Medication> GetMedication([FromRoute] int medicationId)
    {
        try
        {
            if (!HttpContext.IsAdmin(_authService, out _))
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
    public ActionResult Add([FromBody] AddMedicationDto addMedicationDto)
    {
        try
        {
            if (!HttpContext.IsAdmin(_authService, out _))
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
    public ActionResult Delete([FromRoute] int medicationId)
    {
        try
        {
            if (!HttpContext.IsAdmin(_authService, out _))
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
    public ActionResult UpdateMedication([FromBody] UpdateMedicationDto updateMedicationDto,
        [FromRoute] int medicationId)
    {
        try
        {
            if (!HttpContext.IsAdmin(_authService, out _))
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