namespace MadWorld.Backend.Identity.Domain.CommonExceptions;

public class FieldRequiredException : Exception
{
    public string FieldName { get; init; }

    public FieldRequiredException(string fieldName)
    {
        FieldName = fieldName;
    }
}