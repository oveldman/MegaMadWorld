// See https://aka.ms/new-console-template for more information

using MadWorld.PlayTown;

var testObject = new TestObject()
{
    Name = "RandomName"
};

var testObjectCloned = testObject.Clone();
testObjectCloned.Name = "ClonedName";

Console.WriteLine("Hello, World!");