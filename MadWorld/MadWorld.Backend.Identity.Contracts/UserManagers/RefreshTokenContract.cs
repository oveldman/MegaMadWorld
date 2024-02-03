namespace MadWorld.Backend.Identity.Contracts.UserManagers;

public class RefreshTokenContract
{
    public string Id { get; set; }
    public string Audience { get; set; }
    public DateTime Expires { get; set; }
}