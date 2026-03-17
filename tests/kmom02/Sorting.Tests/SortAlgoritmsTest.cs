
﻿namespace Sorting.Tests;

using System.IO.Enumeration;
using Sorting;
public class SortingAlgoritmsTest
{
    [SetUp]
    public void Setup()
    {
    }

    private BusStop[] ReadFromFile(string filename, int nrOf = -1)
    {
        string[] stops = File.ReadAllLines(filename);
        if (nrOf == -1 || nrOf > stops.Length)
            nrOf = stops.Length - 1;
        BusStop[] busStops = new BusStop[nrOf];
        for (int i=0; i<nrOf; i++)
        {
            string[] parts = stops[i+1].Split(",");
            busStops[i] = new BusStop(parts[1], int.Parse(parts[0].Substring(8)));
        }
        return busStops;
    }
    private bool CheckSorted<T>(T[] arr, int n) where T: IComparable<T>
    {
        for (int i=1; i<arr.Length-1; i++)
        {
            if (arr[i].CompareTo(arr[i+1])>0)
                return false;
        }
        return true;
    }

    [TestCase(new int[]{4, 6, 1, 8, 33, 24, 88}, 1, 88)]
    [TestCase(new int[]{4, 4, 4, 4 , 4, 2, 7, 4, 4, 4}, 2, 7)]
    [TestCase(new int[]{10, 11, 12, 13, 14, 15}, 10, 15)]
    [TestCase(new int[]{15, 14, 13, 12, 11}, 11, 15)]
    public void TestInsertionsortOnSmallIntegerArrays(int[] nrs, int first, int last)
    {
        SortingAlgorithms.Insertionsort(nrs, nrs.Length);
        Assert.That(nrs[0], Is.EqualTo(first));
        Assert.That(nrs[nrs.Length-1], Is.EqualTo(last));
        Assert.That(CheckSorted(nrs, nrs.Length), Is.True);       
    }

    [TestCase(new int[]{4, 6, 1, 8, 33, 24, 88}, 1, 88)]
    [TestCase(new int[]{4, 4, 4, 4 , 4, 2, 7, 4, 4, 4}, 2, 7)]
    [TestCase(new int[]{10, 11, 12, 13, 14, 15}, 10, 15)]
    [TestCase(new int[]{15, 14, 13, 12, 11}, 11, 15)]
    public void TestInsertionsortWith3ParametersOnSmallIntegerArrays(int[] nrs, int first, int last)
    {
        SortingAlgorithms.Insertionsort(nrs, 0, nrs.Length - 1);
        Assert.That(nrs[0], Is.EqualTo(first));
        Assert.That(nrs[nrs.Length-1], Is.EqualTo(last));
        Assert.That(CheckSorted(nrs, nrs.Length), Is.True);       
    }

    [TestCase(new string[]{"cow", "dog", "snake", "elephant", "snail", "cat", "horse"}, "cat", "snake")]
    [TestCase(new string[]{"sun", "sun", "sun", "cloud", "sun", "tornado", "sun"}, "cloud", "tornado")]
    public void TestInsertionsortOnSmallStringArrays(string[] words, string first, string last)
    {
        SortingAlgorithms.Insertionsort(words, words.Length);
        Assert.That(words[0], Is.EqualTo(first));
        Assert.That(words[words.Length-1], Is.EqualTo(last));
        Assert.That(CheckSorted(words, words.Length), Is.True);
    }

    [TestCase(new string[]{"Stop U", "Stop T", "Stop C", "Stop R", "Stop E"}, new int[]{32, 99, 11, 42, 44}, 11, 32)]
    public void TestInsertionsortOnSmallBusStopArrays(string[] names, int[] nrs, int firstNr, int lastNr)
    {
        BusStop[] busStops = new BusStop[names.Length];
        for (int i=0; i<names.Length; i++)
            busStops[i] = new BusStop(names[i], nrs[i]);
        SortingAlgorithms.Insertionsort(busStops, busStops.Length);
        Assert.That(busStops[0].GetNr(), Is.EqualTo(firstNr));
        Assert.That(busStops[busStops.Length-1].GetNr(), Is.EqualTo(lastNr));
        Assert.That(CheckSorted(busStops, busStops.Length), Is.True);
    }

    [TestCase(new string[]{"Stop W", "Stop B", "Stop P", "Stop D", "Stop Q"}, new int[]{32, 99, 11, 42, 44}, 8)]
    public void TestInsertionsortNrOfComparisonsSmallArray(string[] names, int[] nrs, int nrOfComps)
    {
        BusStop[] busStops = new BusStop[names.Length];
        for (int i=0; i<names.Length; i++)
            busStops[i] = new BusStop(names[i], nrs[i]);
        BusStop.ResetNrOfComparisons();   
        SortingAlgorithms.Insertionsort(busStops, busStops.Length);
        Assert.That(BusStop.GetNrOfComparisons(), Is.EqualTo(nrOfComps));
    }

    [TestCase("stops.txt", 20, 133)]
    [TestCase("stops.txt", 50, 721)]
    [TestCase("stops.txt", 100, 2571)]
    [TestCase("stops.txt", 1000, 254995)]
    public void TestInsertionsortNrOfComparisonsArray(string filename, int n, int expectedNrOfComparisons)
    {
        BusStop[] busStops = ReadFromFile(filename, n);
        BusStop.ResetNrOfComparisons();   
        SortingAlgorithms.Insertionsort(busStops, busStops.Length);
        Assert.That(BusStop.GetNrOfComparisons(), Is.EqualTo(expectedNrOfComparisons));
    }
    // Quicksort-tester

    [TestCase(new int[]{4, 6, 1, 8, 33, 24, 88}, 1, 88)]
    [TestCase(new int[]{4, 4, 4, 4 , 4, 2, 7, 4, 4, 4}, 2, 7)]
    [TestCase(new int[]{10, 11, 12, 13, 14, 15}, 10, 15)]
    [TestCase(new int[]{15, 14, 13, 12, 11}, 11, 15)]
    public void TestQuicksortOnSmallIntegerArrays(int[] nrs, int first, int last)
    {
        SortingAlgorithms.Quicksort(nrs, 0, nrs.Length-1);
        Assert.That(nrs[0], Is.EqualTo(first));
        Assert.That(nrs[nrs.Length-1], Is.EqualTo(last));
        Assert.That(CheckSorted(nrs, nrs.Length), Is.True);
    }

    [TestCase(new string[]{"Stop I", "Stop K", "Stop C", "Stop W", "Stop B"}, new int[]{32, 99, 11, 42, 44}, 8)]
    public void TestQuicksortNrOfComparisons(string[] names, int[] nrs, int nrOfComps)
    {
        BusStop[] busStops = new BusStop[names.Length];
        for (int i=0; i<names.Length; i++)
            busStops[i] = new BusStop(names[i], nrs[i]);
        BusStop.ResetNrOfComparisons();   
        SortingAlgorithms.Quicksort(busStops,0 , busStops.Length-1);
        Assert.That(BusStop.GetNrOfComparisons(), Is.EqualTo(nrOfComps));
    }

    [TestCase("stops.txt", 20, 77)]
    [TestCase("stops.txt", 50, 234)]
    [TestCase("stops.txt", 100, 628)]
    [TestCase("stops.txt", 1000, 10482)] 
    public void TestQuicksortNrOfComparisonsRandomArray(string filename, int n, int expectedNrOfComparisons)
    {
        BusStop[] busStops = ReadFromFile(filename, n);
        BusStop.ResetNrOfComparisons();   
        SortingAlgorithms.Quicksort(busStops,0 , busStops.Length - 1);
        Assert.That(BusStop.GetNrOfComparisons(), Is.EqualTo(expectedNrOfComparisons));
    }
    

    [TestCase("stops.txt", 20, 190)]
    [TestCase("stops.txt", 50, 1225)]
    [TestCase("stops.txt", 100, 4927)]
    [TestCase("stops.txt", 1000, 474879)] 
    public void TestQuicksortNrOfComparisonsSortedArray(string filename, int n, int expectedNrOfComparisons)
    {
        BusStop[] busStops = ReadFromFile(filename, n);
        Array.Sort(busStops);
        //Console.Write(busStops[0].Get)

        BusStop.ResetNrOfComparisons();   
        SortingAlgorithms.Quicksort(busStops, 0 , busStops.Length - 1);
        Assert.That(BusStop.GetNrOfComparisons(), Is.EqualTo(expectedNrOfComparisons));
    }

    [TestCase("stops.txt", 20, 78)]
    [TestCase("stops.txt", 50, 255)]
    [TestCase("stops.txt", 100, 622)]
    [TestCase("stops.txt", 1000, 9430)] 
    public void TestQuicksortMedianOfThreeNrOfComparisonsSortedArray(string filename, int n, int expectedNrOfComparisons)
    {
        BusStop[] busStops = ReadFromFile(filename, n);
        Array.Sort(busStops);

        BusStop.ResetNrOfComparisons();   
        SortingAlgorithms.QuicksortUsingMedianOfThree(busStops, 0 , busStops.Length - 1);
        Assert.That(BusStop.GetNrOfComparisons(), Is.EqualTo(expectedNrOfComparisons));
    }

    [TestCase("stops.txt", 20, 10, 145)]
    [TestCase("stops.txt", 50, 10, 1180)]
    [TestCase("stops.txt", 100, 10, 4882)] 
    public void TestQuicksortUsingInsertionsortNrOfComparisonsSortedArray(string filename, int n, int limit, int expectedNrOfComparisons)
    {
        BusStop[] busStops = ReadFromFile(filename, n);
        Array.Sort(busStops);

        BusStop.ResetNrOfComparisons();   
        SortingAlgorithms.QuicksortUsingInsertionsort(busStops, 0 , busStops.Length - 1);
        Assert.That(BusStop.GetNrOfComparisons(), Is.EqualTo(expectedNrOfComparisons));
    }
}
