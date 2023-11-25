namespace MadWorld.Backend.Domain.CommonExceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message) {}
}