using LanguageExt;

namespace MadWorld.ShipSimulator.Domain.Companies;

public interface ICompanyRepository
{
    Task<Option<Company>> GetCompanyByIdAsync(Guid userId);
}