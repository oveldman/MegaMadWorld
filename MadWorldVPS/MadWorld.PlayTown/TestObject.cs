namespace MadWorld.PlayTown;

public class TestObject
{
    public string Name { get; set; } = string.Empty;

    public TestObject Clone()
    {
        return (TestObject)MemberwiseClone();
    }
}