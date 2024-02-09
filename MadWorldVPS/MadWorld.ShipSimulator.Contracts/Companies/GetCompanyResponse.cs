namespace MadWorld.ShipSimulator.Contracts.Companies;

public class GetCompanyResponse
{
    public bool CompanyFound { get; set; }
    public CompanyContract? Company { get; set; }
}