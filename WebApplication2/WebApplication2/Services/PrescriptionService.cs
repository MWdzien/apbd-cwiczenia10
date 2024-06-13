using WebApplication2.Context;
using WebApplication2.DTOs;

namespace WebApplication2.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly ApplicationContext _context;
    private readonly IPatientService _patientService;
    private readonly IMedicamentService _medicamentService;

    public PrescriptionService(ApplicationContext context, IPatientService patientService, IMedicamentService medicamentService)
    {
        _context = context;
        _patientService = patientService;
        _medicamentService = medicamentService;
    }

    public async Task AddPrescription(Prescription prescription)
    {
        if (!await _patientService.DoesPatientExist(prescription.IdPatient))
        {
            await _context.AddAsync(prescription.Patient);
            await _context.SaveChangesAsync();
        }

        foreach (var pres in prescription.PrescriptionMedicaments)
        {
            if (!await _medicamentService.DoesMedicamentExist(pres.IdMedicament)) throw new InvalidOperationException("Medicament does no exist");
        }

        if (prescription.PrescriptionMedicaments.Count > 10)
            throw new InvalidOperationException("Max medicament number exceeded");
        if (prescription.DueDate < DateTime.Now)
            throw new InvalidOperationException("Prescription outdated");

        await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync();
    }
}