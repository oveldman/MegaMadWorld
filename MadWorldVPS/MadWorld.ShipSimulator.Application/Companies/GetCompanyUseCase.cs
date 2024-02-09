using MadWorld.ShipSimulator.Contracts.Companies;
using MadWorld.ShipSimulator.Domain.CommonExceptions;
using MadWorld.ShipSimulator.Domain.Companies;

namespace MadWorld.ShipSimulator.Application.Companies;

public class GetCompanyUseCase
{
    private readonly ICompanyRepository _companyRepository;

    public GetCompanyUseCase(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    
    public async Task<GetCompanyResponse> GetCompanyAsync(string userId)
    {
        if (Guid.TryParse(userId, out var userIdGuid))
        {
            throw new GuidNotValidException(nameof(userId));
        }
        
        var company = await _companyRepository.GetCompanyByIdAsync(userIdGuid);

        return new GetCompanyResponse()
        {
            CompanyFound = company.IsSome,
            Company = company
                .Select(c => c.ToDetails())
                .FirstOrDefault()
        };
    }
}