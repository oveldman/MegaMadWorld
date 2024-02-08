namespace MadWorld.Backend.Domain.CommonExceptions;

public sealed class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}