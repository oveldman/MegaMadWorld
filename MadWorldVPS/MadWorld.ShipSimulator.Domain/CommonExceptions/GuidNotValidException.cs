namespace MadWorld.ShipSimulator.Domain.CommonExceptions;

public class GuidNotValidException : Exception
{
    public string PropertyName { get; private set; }
    public GuidNotValidException(string propertyName)
    {
        PropertyName = propertyName;
    }
}