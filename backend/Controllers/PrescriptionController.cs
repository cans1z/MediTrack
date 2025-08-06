using MediTrack.Services;
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
    
    //get prescriptions
    [HttpPost("add")]
    public ActionResult AddPrescription([FromHeader(Name = "Authentication")] string token, [FromBody] AddPrescriptionDto dto)
    {
        if (!_authService.TryValidateDoctor(token, out var doctor, out var errorMessage))
            return Unauthorized(errorMessage);

        try
        {
            var prescription = _prescriptionService.AddPrescription(dto, doctor);
            return Ok(prescription);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("by-patient/{patientId}")]
    public ActionResult GetPrescriptionsForPatient([FromHeader(Name = "Authentication")] string token, int patientId)
    {
        if (!_authService.TryValidateToken(token, out var user, out var errorMessage))
            return Unauthorized(errorMessage);

        // Врач видит всех, пациент — только свои
        if (user.Role == UserRole.Patient && user.Id != patientId)
            return Forbid();

        try
        {
            var prescriptions = _prescriptionService.GetPrescriptionsForPatient(patientId);
            return Ok(prescriptions);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}