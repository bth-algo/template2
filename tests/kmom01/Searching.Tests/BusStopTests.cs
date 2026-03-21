namespace Searching.Tests;
using Searching;

public class BusStopTests
{
    [SetUp]
    public void Setup()
    {
    }
    [Test]
    public void TestBusStopToString()
    {
        BusStop stop = new BusStop(111, "Halmstad", "tjo hopp");
        Assert.That(stop.ToString().Contains("111"));
        Assert.That(stop.ToString().Contains("Halmstad"));
        Assert.That(stop.ToString().Contains("tjo hopp"));
    }

    [Test]
    public void TestBusStopCompareTo()
    {
        BusStop stopOne = new BusStop(111, "A", "a");
        BusStop stopTwo = new BusStop(102, "B", "b");
        BusStop stopThree = new BusStop(111, "A", "x");

        Assert.That(stopOne.CompareTo(stopTwo), Is.EqualTo(-1));
        Assert.That(stopTwo.CompareTo(stopOne), Is.EqualTo(1));
        Assert.That(stopOne.CompareTo(stopThree), Is.EqualTo(0));

    }

        [Test]
    public void TestBusStopGetCopy()
    {
        BusStop stopOne = new BusStop(111, "A", "a");
        BusStop stopTwo = stopOne.GetCopy();

        Assert.That(stopOne == stopTwo, Is.False);   
        Assert.That(stopOne.CompareTo(stopTwo), Is.EqualTo(0));
    }
}
