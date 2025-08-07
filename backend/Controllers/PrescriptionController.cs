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
    public ActionResult AddPrescription([FromBody] AddPrescriptionDto dto)
    {
        var user = HttpContext.Items["User"] as User;
        if (user == null) return Unauthorized();
        if (user.Role != UserRole.Doctor) return BadRequest("You don't have permission to access this resource.");

        try
        {
            _prescriptionService.AddPrescription(dto, user);
            return Ok(new { message = "Prescription added successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("list/{patientId}")]
    public ActionResult GetPrescriptionsByPatient(int patientId)
    {
        var user = HttpContext.Items["User"] as User;
        if (user == null) return Unauthorized();

        if (user.Role != UserRole.Doctor || user.Id != patientId)
        {
            return BadRequest("You don't have permission to access this resource.");
        }

        var prescriptions = _prescriptionService.GetPrescriptionsByPatient(patientId);
        return Ok(prescriptions);
    }

    
}