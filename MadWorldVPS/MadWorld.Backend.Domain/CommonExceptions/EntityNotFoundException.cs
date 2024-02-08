namespace MadWorld.Backend.Domain.CommonExceptions;

public sealed class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message) {}
}