namespace MadWorld.ShipSimulator.Domain.Companies;

public interface ICompanyLiveUpdater
{
    Task UpdateCompanyAsync(Company company);
}