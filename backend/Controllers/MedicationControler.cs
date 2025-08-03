using MediTrack.DTO;
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

    public MedicationControler(AuthService authService)
    {
        _authService = authService;
    }
    
    //get medications
    [HttpGet("list")]
    public ActionResult<List<Medication>> GetMedicatons([FromQuery] string token)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
                return BadRequest("You dont have permission to get users");
            using (var db = new ApplicationContext())
            {
                var medications = db.Medications.ToList();
                return Ok(medications);
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
    //get medication by id
    [HttpPost("add")]
    public ActionResult Add([FromQuery] string token, [FromBody] AddMedicationDto medicationDto)
    {
        try
        {
            var user = _authService.ValidateToken(token);
            if (user.Role != UserRole.Administrator)
                return BadRequest("You dont have permission to get users");
            using (var db = new ApplicationContext())
            {
                var newMedication = medicationDto.CreateMedication();
                db.Medications.Add(newMedication);
                db.SaveChanges();
            }

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