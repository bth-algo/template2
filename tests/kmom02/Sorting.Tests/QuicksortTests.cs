namespace Sorting.Tests;

using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Sorting;
public class QuicksortTests
{

    private BusStop d = new BusStop(23, "D");
    private BusStop b = new BusStop(11, "B");
    private BusStop a = new BusStop(23, "A");
    private BusStop c = new BusStop(23, "C");
    private void shuffle<T>(T[] array)
    {
        Random random = new Random();
        for (int i=0; i< array.Length; i++)
        {
            int index1 = random.Next(0, array.Length);
            int index2 = random.Next(0, array.Length);
            (array[index1], array[index2]) = (array[index2], array[index1]);
        }
    } 

    private bool IsOrdered<T>(T[] array) where T : IComparable<T>
    {
        for (int i=0; i<array.Length - 1; i++)
            if (array[i].CompareTo(array[i+1]) > 0)
                return false;
        return true;
    }

    [Test]
    public void Quicksort_SameValueArray()
    {
        int[] sameValueArray = {5, 5, 5, 5};
        SortingAlgorithms.Quicksort(sameValueArray, 0, sameValueArray.Length - 1);

        Assert.That(IsOrdered(sameValueArray), Is.True);
    }


    [Test]
    public void Quicksort_ReversedOrderedArray_Sorted()
    {
        int[] revOrderedArray = {9, 8, 7, 6, 5, 4, 3, 2, 1};
        SortingAlgorithms.Quicksort(revOrderedArray, 0, revOrderedArray.Length - 1);

        Assert.That(IsOrdered(revOrderedArray), Is.True);
    }

    [Test]
    public void Quicksort_orderedArray_Sorted()
    {
        int[] orderedArray = {1, 2, 3, 4, 5, 6, 7, 8, 9};
        SortingAlgorithms.Quicksort(orderedArray, 0, orderedArray.Length - 1);

        Assert.That(IsOrdered(orderedArray), Is.True);
    }



    [Test]
    public void Quicksort_randomArray_Sorted()
    {
        int[] randomArray = {3, 5, 1, 7, 8, 2, 4, 6, 9};
        SortingAlgorithms.Quicksort(randomArray, 0, randomArray.Length - 1);
        Assert.That(IsOrdered(randomArray), Is.True);
        shuffle(randomArray);
        SortingAlgorithms.Quicksort(randomArray, 0, randomArray.Length - 1);
        Assert.That(IsOrdered(randomArray), Is.True);
        shuffle(randomArray);
        SortingAlgorithms.Quicksort(randomArray, 0, randomArray.Length - 1);
        Assert.That(IsOrdered(randomArray), Is.True);
    }

    [Test]
    public void Quicksort_NrOfComparisons_OrderedArray()
    {
        BusStop[] stops = {a, b, c, d};
        BusStop.ResetNrOfComparisons();
        SortingAlgorithms.Quicksort(stops, 0, 3);
        Assert.That(BusStop.GetNrOfComparisons(), Is.EqualTo(6));
    }

    
    [Test]
    public void Quicksort_NrOfComparisons_RevOrderedArray()
    {
        BusStop[] stops = {d, c, b, a};
        BusStop.ResetNrOfComparisons();
        SortingAlgorithms.Quicksort(stops, 0, 3);
        Assert.That(BusStop.GetNrOfComparisons(), Is.EqualTo(6));
    }

}