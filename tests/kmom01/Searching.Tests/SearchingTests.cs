namespace Searching.Tests;
using Searching;

public class SearchingTests
{
    public static int[] RandomNumbers(int minValue, int maxValue, int nrOf, int seed = 1234)
    {
        Random random = new Random(seed);

        int[] nrs = new int[nrOf];
        for (int i = 0; i<nrOf; i++)
            nrs[i] = random.Next(minValue, maxValue + 1);
        //Array.Sort(nrs);
        return nrs;
    }
    [Test]
    public void LinearSearch_UnorderedNumbers_WorstCaseSmallArray()
    {
        int[] unOrderedNrs = RandomNumbers(1, 300, 10, 100);
        List<int> indexes = new List<int>();
        int index = SearchAlgorithms.LinearSearch(unOrderedNrs, 10, 888, indexes);
        Assert.That(index, Is.EqualTo(-1));
        Assert.That(unOrderedNrs.Length, Is.EqualTo(indexes.Count));
        for (int i=0; i<10; i++)
            Assert.That(i, Is.EqualTo(indexes[i]));
        TestContext.Out.WriteLine($"✅ LinearSearch worst case small array");

    }
    [Test]
    public void LinearSearch_OrderedNumbers_BestCaseSmallArray()
    {
        int[] nrs = {1, 2, 3, 4, 5, 6, 7, 8, 9};
        List<int> indexes = new List<int>();
        int index = SearchAlgorithms.LinearSearch(nrs, 9, 1, indexes);
        Assert.That(index, Is.EqualTo(0));
        Assert.That(indexes.Count, Is.EqualTo(1));
        Assert.That(indexes[0], Is.EqualTo(0));
        TestContext.Out.WriteLine($"✅ LinearSearch best case small array");
    }

    [Test]
    public void LinearSearch_UnorderedNumbers()
    {
        int[] unOrderedNrs = RandomNumbers(1, 300, 100, 55);
        List<int> indexes = new List<int>();
        int index = SearchAlgorithms.LinearSearch(unOrderedNrs, 100, 133, indexes);
        Assert.That(index, Is.EqualTo(12));
        Assert.That(indexes.Count, Is.EqualTo(13));
        for (int i=0; i<=12; i++)
            Assert.That(i, Is.EqualTo(indexes[i]));
        TestContext.Out.WriteLine($"✅ LinearSearch ok");
    }

    [Test]
    public void BinarySearch_WorstCaseSmallArray01()
    {
        int[] nrs = new int[]{10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85};
        List<int> indexes = new List<int>();
        int index = SearchAlgorithms.BinarySearch(nrs, 16, 888, indexes);
        Assert.That(index, Is.EqualTo(-1));
        Assert.That(indexes.Count == 5 || indexes.Count == 6, Is.True);
        int[] expectedIndexes = new int[]{7, 11, 13, 14, 15};
        for (int i=0; i<5; i++)
            Assert.That(expectedIndexes[i], Is.EqualTo(indexes[i]));
        TestContext.Out.WriteLine($"✅ BinarySearch worst case searching for element bigger than the biggest in the array");
    }

    [Test]
    public void BinarySearch_WorstCaseSmallArray02()
    {
        int[] nrs = new int[]{10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85};
        List<int> indexes = new List<int>();
        int index = SearchAlgorithms.BinarySearch(nrs, 16, 5, indexes);
        Assert.That(index, Is.EqualTo(-1));
        Assert.That(indexes.Count == 4 || indexes.Count == 5, Is.True);
        int[] expectedIndexes = new int[]{7, 3, 1, 0};
        for (int i=0; i<4; i++)
            Assert.That(expectedIndexes[i], Is.EqualTo(indexes[i]));
        TestContext.Out.WriteLine($"✅ BinarySearch worst case test searching for an element that is smaller than the smallest in the array");
    }

    [Test]
    public void BinarySearch_WorstCase()
    {
        int[] nrs = new int[1024];
        for (int i=0; i<1024; i++)
            nrs[i] = i*2;
        List<int> indexes = new List<int>();
        int index = SearchAlgorithms.BinarySearch(nrs, 1024, 77, indexes);
        Assert.That(index, Is.EqualTo(-1));
        Assert.That(indexes.Count == 10 || indexes.Count == 11, Is.True);
        int[] expectedIndexes = new int[]{511, 255, 127, 63, 31, 47, 39, 35, 37, 38};
        for (int i=0; i<expectedIndexes.Count; i++)
            Assert.That(expectedIndexes[i], Is.EqualTo(indexes[i]));
        TestContext.Out.WriteLine($"✅ BinarySearch worst case array");
    }
    [Test]
    public void BinarySearch_BestCase()
    {
        int[] nrs = new int[1024];
        for (int i=0; i<1024; i++)
            nrs[i] = i*2;
        List<int> indexes = new List<int>();
        int index = SearchAlgorithms.BinarySearch(nrs, 1024, 1022, indexes);
        Assert.That(index, Is.EqualTo(511));
        Assert.That(indexes.Count == 1, Is.True);
        int[] expectedIndexes = new int[]{511};
        for (int i=0; i<indexes.Count; i++)
            Assert.That(expectedIndexes[i], Is.EqualTo(indexes[i]));
        TestContext.Out.WriteLine($"✅ BinarySearch best case");
    }
    [Test]
    public void BinarySearch01()
    {
        int[] nrs = new int[1024];
        for (int i=0; i<1024; i++)
            nrs[i] = i*2;
        List<int> indexes = new List<int>();
        int index = SearchAlgorithms.BinarySearch(nrs, 1024, 1402, indexes);
        Assert.That(index, Is.EqualTo(701));
        Assert.That(indexes.Count == 9, Is.True);
        int[] expectedIndexes = new int[]{511, 767, 639, 703, 671, 687, 695, 699, 701};
        for (int i=0; i<expectedIndexes.Length; i++)
            Assert.That(indexes[i], Is.EqualTo(expectedIndexes[i]));
        TestContext.Out.WriteLine($"✅ BinarySearch test 01");
    }

    [Test]
    public void BinarySearch02()
    {
        int[] nrs = new int[1024];
        for (int i=0; i<1024; i++)
            nrs[i] = i*2;
        List<int> indexes = new List<int>();
        int index = SearchAlgorithms.BinarySearch(nrs, 1024, 1534, indexes);
        Assert.That(index, Is.EqualTo(767));
        Assert.That(indexes.Count, Is.EqualTo(2));
        int[] expectedIndexes = new int[]{511, 767};
        for (int i=0; i<expectedIndexes.Length; i++)
            Assert.That(indexes[i], Is.EqualTo(expectedIndexes[i]));
        TestContext.Out.WriteLine($"✅ BinarySearch test 02");
    }
}
