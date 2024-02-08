namespace MadWorld.ShipSimulator.Domain.Companies;

public class Company
{
    public const int MaxLength = 250;
    
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Amount { get; set; }
    public Guid UserId { get; set; }
}