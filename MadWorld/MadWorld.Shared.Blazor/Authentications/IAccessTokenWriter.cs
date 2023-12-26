namespace MadWorld.Shared.Blazor.Authentications;

public interface IAccessTokenWriter
{
    void SetAccessToken(string token, DateTimeOffset expires);
    void RemoveToken();
}