namespace MadWorld.Backend.Identity.Contracts.UserInfo;

public class InfoResponse
{
    public string Email { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; }
}