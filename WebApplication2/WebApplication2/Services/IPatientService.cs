using WebApplication2.DTOs;

namespace WebApplication2.Services;

public interface IPatientService
{
    Task<bool> DoesPatientExist(int idPatient);
    Task<PatientDTO> GetPatientInfo(int idPatient);
}