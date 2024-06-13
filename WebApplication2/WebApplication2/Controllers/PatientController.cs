using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;

namespace WebApplication2;

[Route("api/patient")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{IdPatient}")]
    public async Task<IActionResult> GetPatient(int IdPatient)
    {
        var patient = await _patientService.GetPatientInfo(IdPatient);
        return Ok(patient);
    }
}