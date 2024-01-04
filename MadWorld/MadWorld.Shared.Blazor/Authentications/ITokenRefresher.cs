namespace MadWorld.Shared.Blazor.Authentications;

public interface ITokenRefresher
{
    Task Execute();
}