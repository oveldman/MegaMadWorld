using MadWorld.ShipSimulator.Contracts.Companies;
using MadWorld.ShipSimulator.Domain.Companies;

namespace MadWorld.ShipSimulator.Application.Companies;

public static class CompanyMapper
{
    public static CompanyContract ToDetails(this Company company)
    {
        return new CompanyContract()
        {
            Id = company.Id.ToString()
        };
    }
}