namespace MadWorld.Frontend.Admin.Application.UserManagement;

public class UserDetails
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
    public IReadOnlyList<RolesDetails> Roles { get; init; } = Array.Empty<RolesDetails>();
    public IReadOnlyList<RefreshTokenDetails> RefreshTokens { get; init; } = Array.Empty<RefreshTokenDetails>();
}