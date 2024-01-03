namespace MadWorld.Backend.Identity.Domain;

public class JwtToken
{
    public string Token { get; set; } = null!;
    public DateTime Expired { get; set; }
}