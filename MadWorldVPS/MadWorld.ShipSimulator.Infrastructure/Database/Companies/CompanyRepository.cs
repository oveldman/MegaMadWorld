using LanguageExt;
using MadWorld.ShipSimulator.Domain.Companies;
using Microsoft.EntityFrameworkCore;

namespace MadWorld.ShipSimulator.Infrastructure.Database.Companies;

public class CompanyRepository : ICompanyRepository
{
    private readonly ShipSimulatorContext _context;

    public CompanyRepository(ShipSimulatorContext context)
    {
        _context = context;
    }

    public async Task<Option<Company>> GetCompanyByIdAsync(Guid userId)
    {
        return await _context.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }
}