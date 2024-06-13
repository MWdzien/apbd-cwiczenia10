using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Services;

namespace WebApplication2;

[Route("api/prescription")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription(Prescription prescription)
    {
        await _prescriptionService.AddPrescription(prescription);
        return Ok();
    }
}