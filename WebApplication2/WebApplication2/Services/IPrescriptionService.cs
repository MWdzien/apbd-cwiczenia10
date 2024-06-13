using WebApplication2.DTOs;

namespace WebApplication2.Services;

public interface IPrescriptionService
{
    Task AddPrescription(Prescription prescription);
}