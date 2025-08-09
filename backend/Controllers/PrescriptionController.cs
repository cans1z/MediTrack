using MediTrack.DTO;
using MediTrack.Services;
using MediTrack.Types;
using Microsoft.AspNetCore.Mvc;

namespace MediTrack.Controllers;

[Route("api/prescriptions")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly PrescriptionService _prescriptionService;
    
    public PrescriptionController(AuthService authService, PrescriptionService prescriptionService)
    {
        _authService = authService;
        _prescriptionService = prescriptionService;
    }
    
    
    // todo: проверяй чтобы в patientId был айди только пациента
    [HttpGet("list/{patientId}")]
    public ActionResult GetPrescriptionsByPatient([FromQuery] string token, int patientId)
    {
        var user = _authService.ValidateToken(token);
        if (user.Role == UserRole.Patient) // todo тут сам реши какие роли это могут
            return Unauthorized("Admin access required.");
        var prescriptions = _prescriptionService.GetPrescriptionsByPatient(patientId);
        return Ok(prescriptions);
    }
    
    //add prescriptions
    [HttpPost("add")]
    public ActionResult AddPrescription([FromQuery] string token, [FromBody] AddPrescriptionDto dto)
    {
        var user = _authService.ValidateToken(token);
        if (user.Role == UserRole.Patient) // todo тут сам реши какие роли это могут
            return Unauthorized("Admin access required.");
        
        try
        {
            _prescriptionService.AddPrescription(dto, user);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    
    //update prescriptions
    [HttpPut("update")]
    public ActionResult UpdatePrescription([FromQuery] string token, [FromBody] UpdatePrescriptionDto dto,
        int prescriptionId)
    {
        var user = _authService.ValidateToken(token);
        if (user.Role == UserRole.Patient)
            return Unauthorized("You don't have permission to edit this prescription.");
        try
        {
            _prescriptionService.UpdatePrescription(dto, prescriptionId);
            //todo fix datetime issue
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    //delete prescriptions
    [HttpDelete("delete")]
    public ActionResult DeletePrescription([FromQuery] string token, int prescriptionId)
    {
        var user = _authService.ValidateToken(token);
        if (user.Role == UserRole.Patient)
            return Unauthorized("You don't have permission to perform this action.");
        try
        {
            _prescriptionService.DeletePrescription(prescriptionId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    

    
}