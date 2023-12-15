namespace MadWorld.Backend.Identity.Domain.CommonExceptions;

public sealed class UserNotFoundException : Exception
{
    public string UserId { get; init; }

    public UserNotFoundException(string userId)
    {
        UserId = userId;
    }
}