using Microsoft.EntityFrameworkCore;
using WebApplication2.Context;

namespace WebApplication2.Services;

public class MedicamentService : IMedicamentService
{
    private readonly ApplicationContext _context;

    public MedicamentService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesMedicamentExist(int idMedicament)
    {
        return await _context.Medicaments.AnyAsync(m => m.IdMedicament == idMedicament);
    }
}