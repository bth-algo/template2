using Sorting;

namespace Sorting.Tests;

public class BusStopTestNrOfComparisons
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        BusStop busStopA = new BusStop(11, "A", "aaa");
        BusStop busStopB = new BusStop(44, "B", "bbb");

        BusStop.ResetNrOfComparisons();
        Assert.That(BusStop.GetNrOfComparisons(), Is.EqualTo(0));
        busStopA.CompareTo(busStopB);
        Assert.That(BusStop.GetNrOfComparisons(), Is.EqualTo(1));
        busStopA.CompareTo(busStopB);
        Assert.That(BusStop.GetNrOfComparisons(), Is.EqualTo(2));
        TestContext.Out.WriteLine($"✅ BusStop: nr of comparisons");
    }
}
