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
            
            // todo: NO COntent
            return Ok(new { message = "Prescription added successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
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

    
}