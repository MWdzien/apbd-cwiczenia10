using Microsoft.EntityFrameworkCore;
using WebApplication2.Context;
using WebApplication2.DTOs;

namespace WebApplication2.Services;

public class PatientService : IPatientService
{
    private readonly ApplicationContext _context;

    public PatientService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesPatientExist(int idPatient)
    {
        return await _context.Patients.AnyAsync(e => e.IdPatient == idPatient);
    }

    public async Task<PatientDTO> GetPatientInfo(int idPatient)
    {
        Patient patient = await _context.Patients.Include(e => e.Prescriptions).ThenInclude(e => e.Doctor)
            .Include(e => e.Prescriptions).ThenInclude(e => e.PrescriptionMedicaments).ThenInclude(e => e.Medicament)
            .SingleOrDefaultAsync(e => idPatient == idPatient);

        return new PatientDTO()
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = patient.Prescriptions.OrderBy(p => p.DueDate).Select(p => new PrescriptionDTO()
            {
                IdPrescription = p.IdPrescription,
                Date = p.Date,
                DueDate = p.DueDate,
                Doctor = new Doctor()
                {
                    IdDoctor = p.IdDoctor,
                    FirstName = p.Doctor.FirstName,
                    LastName = p.Doctor.LastName
                },
                Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentDTO()
                {
                    IdMedicament = pm.Medicament.IdMedicament,
                    Description = pm.Medicament.Description,
                    Dose = pm.Dose
                }).ToList()
            }).ToList()
        };
    }
}