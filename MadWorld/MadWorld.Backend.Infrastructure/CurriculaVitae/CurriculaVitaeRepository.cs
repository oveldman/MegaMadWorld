using MadWorld.Backend.Domain.CurriculaVitae;

namespace MadWorld.Backend.Infrastructure.CurriculaVitae;

public class CurriculaVitaeRepository : ICurriculaVitaeRepository
{
    private readonly CurriculaVitaeContext _context;

    public CurriculaVitaeRepository(CurriculaVitaeContext context)
    {
        _context = context;
    }

    public Profile GetProfile(bool isDraft)
    {
        return _context.Profiles.FirstOrDefault();
    }
}