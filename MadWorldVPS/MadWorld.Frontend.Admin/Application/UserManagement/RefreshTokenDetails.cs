namespace MadWorld.Frontend.Admin.Application.UserManagement;

public class RefreshTokenDetails
{
    public string Id { get; set; }
    public string Audience { get; set; }
    public DateTime Expires { get; set; }
}