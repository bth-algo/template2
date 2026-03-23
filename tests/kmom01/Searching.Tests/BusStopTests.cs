using Searching;

namespace Searching.Tests;

public class BusStopTests
{
    [Test]
    public void ToString_ReturnsExpectedFormat()
    {
        var busStop = new BusStop(42, "Central", "Near station");

        Assert.That(busStop.ToString().Contains("42"));
        Assert.That(busStop.ToString().Contains("Central"));
        Assert.That(busStop.ToString().Contains("Near station"));
        TestContext.Out.WriteLine($"✅ ToString() in BusStop");
    }

    [Test]
    public void CompareTo_ReturnsNegative_WhenNameIsLess()
    {
        var stopA = new BusStop(1, "A", "first");
        var stopB = new BusStop(2, "B", "second");

        var result = stopA.CompareTo(stopB);

        Assert.That(result, Is.LessThan(0));
        TestContext.Out.WriteLine($"✅ CompareTo() return < 0 when less than");
    }

    [Test]
    public void CompareTo_ReturnsPositive_WhenNameIsGreater()
    {
        var stopA = new BusStop(1, "X", "first");
        var stopB = new BusStop(2, "A", "second");

        var result = stopA.CompareTo(stopB);

        Assert.That(result, Is.GreaterThan(0));
        TestContext.Out.WriteLine($"✅ CompareTo() return > 0 when bigger than");
    }

    [Test]
    public void CompareTo_ReturnsZero_WhenNameIsTheSame()
    {
        var stopA = new BusStop(1, "X", "first");
        var stopB = new BusStop(2, "X", "second");

        var result = stopA.CompareTo(stopB);

        Assert.That(result, Is.EqualTo(0));
        TestContext.Out.WriteLine($"✅ CompareTo() return 0 when same");
    }


    [Test]
    public void GetCopy_ReturnsEquivalentObject_AndNotSameReference()
    {
        var original = new BusStop(55, "Mid", "note");

        var copy = original.GetCopy();

        Assert.That(copy, Is.Not.Null);
        Assert.That(copy, Is.Not.SameAs(original));
        Assert.That(copy.GetNr(), Is.EqualTo(original.GetNr()));
        Assert.That(copy.GetName(), Is.EqualTo(original.GetName()));
        Assert.That(copy.GetNote(), Is.EqualTo(original.GetNote()));
        TestContext.Out.WriteLine($"✅ GetCopy() returns a correct copy");
    }
}
