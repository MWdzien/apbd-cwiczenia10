namespace WebApplication2.Services;

public interface IMedicamentService
{
    Task<bool> DoesMedicamentExist(int idMedicament);
}