namespace MadWorld.Frontend.Admin.Application.UserManagement;

public class UserDetails
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
    public IReadOnlyList<string> Roles { get; set; } = Array.Empty<string>();
    public IReadOnlyList<RefreshTokenDetails> RefreshTokens { get; set; } = Array.Empty<RefreshTokenDetails>();
}