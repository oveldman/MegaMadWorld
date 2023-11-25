namespace MadWorld.Backend.Domain.CommonExceptions;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}