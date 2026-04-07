using Sorting;

namespace Sorting.Tests;

public class BusStopTestNrOfComparisons
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestComparisons()
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

    [Test]
    public void TestReset()
    {
        BusStop busStopA = new BusStop(11, "A", "aaa");
        BusStop busStopB = new BusStop(44, "B", "bbb");

        BusStop.ResetNrOfComparisons();
        busStopA.CompareTo(busStopB);
        busStopA.CompareTo(busStopB);
        BusStop.ResetNrOfComparisons();
        Assert.That(BusStop.GetNrOfComparisons(), Is.EqualTo(0));
        TestContext.Out.WriteLine($"✅ BusStop: reset nr of comparisons");
    }
}
