using System;
using System.Diagnostics;

class SearchAlgorithms
{
    static void Main(string[] args)
    {
        // Sample data set
        int[] dataSet = GenerateSortedData(1000000); // Generate a sorted array for binary and interpolation search
        int target = 567890; // Target to search

        // Linear Search
        Console.WriteLine("===== Linear Search =====");
        DescribeAlgorithm("Linear Search", "Iterates through each element sequentially until the target is found or the list ends.",
            "O(n)", "O(n)");
        RunSearch("Linear Search", LinearSearch, dataSet, target);
        DisplayPseudocode("Linear Search", @"
        For each element in the array:
            If element == target, return index
        Return -1 if not found.");

        // Binary Search
        Console.WriteLine("\n===== Binary Search =====");
        DescribeAlgorithm("Binary Search", "Searches a sorted array by repeatedly dividing the search interval in half.",
            "O(log n)", "O(log n)");
        RunSearch("Binary Search", BinarySearch, dataSet, target);
        DisplayPseudocode("Binary Search", @"
        Set low = 0, high = n - 1
        While low <= high:
            mid = (low + high) / 2
            If array[mid] == target, return mid
            If array[mid] < target, set low = mid + 1
            Else, set high = mid - 1
        Return -1 if not found.");

        // Interpolation Search
        Console.WriteLine("\n===== Interpolation Search =====");
        DescribeAlgorithm("Interpolation Search", "Improves upon binary search by estimating the likely position of the target in a sorted array.",
            "O(log log n)", "O(n)");
        RunSearch("Interpolation Search", InterpolationSearch, dataSet, target);
        DisplayPseudocode("Interpolation Search", @"
        Set low = 0, high = n - 1
        While low <= high and target is in range:
            pos = low + [(target - array[low]) * (high - low)] / (array[high] - array[low])
            If array[pos] == target, return pos
            If array[pos] < target, set low = pos + 1
            Else, set high = pos - 1
        Return -1 if not found.");

        // Summary Comparison
        Console.WriteLine("\n===== Summary Comparison =====");
        Console.WriteLine("Linear Search is the slowest because it inspects each element one by one, resulting in O(n) complexity.");
        Console.WriteLine("Binary Search is faster for sorted arrays, as it halves the search space each time, resulting in O(log n) complexity.");
        Console.WriteLine("Interpolation Search is the fastest for uniformly distributed sorted data, as it estimates the position, achieving O(log log n) complexity in the best case.");
    }

    // Algorithm Descriptions
    static void DescribeAlgorithm(string name, string description, string bestCase, string worstCase)
    {
        Console.WriteLine($"Algorithm: {name}");
        Console.WriteLine($"Description: {description}");
        Console.WriteLine($"Best Case: {bestCase}");
        Console.WriteLine($"Worst Case: {worstCase}");
    }

    // Pseudocode Display
    static void DisplayPseudocode(string name, string pseudocode)
    {
        Console.WriteLine($"\nPseudocode for {name}:");
        Console.WriteLine(pseudocode);
    }

    // Search Execution and Timing
    static void RunSearch(string name, Func<int[], int, int> searchFunction, int[] array, int target)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        int result = searchFunction(array, target);
        stopwatch.Stop();

        Console.WriteLine($"{name} Result: {(result != -1 ? $"Found at index {result}" : "Not Found")}");
        Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
    }

    // Linear Search Implementation
    static int LinearSearch(int[] array, int target)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == target)
                return i;
        }
        return -1;
    }

    // Binary Search Implementation
    static int BinarySearch(int[] array, int target)
    {
        int low = 0, high = array.Length - 1;
        while (low <= high)
        {
            int mid = low + (high - low) / 2;
            if (array[mid] == target)
                return mid;
            if (array[mid] < target)
                low = mid + 1;
            else
                high = mid - 1;
        }
        return -1;
    }

    // Interpolation Search Implementation
    static int InterpolationSearch(int[] array, int target)
    {
        int low = 0, high = array.Length - 1;
        while (low <= high && target >= array[low] && target <= array[high])
        {
            int pos = low + ((target - array[low]) * (high - low)) / (array[high] - array[low]);
            if (array[pos] == target)
                return pos;
            if (array[pos] < target)
                low = pos + 1;
            else
                high = pos - 1;
        }
        return -1;
    }

    // Helper Method to Generate Sorted Data
    static int[] GenerateSortedData(int size)
    {
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = i;
        }
        return array;
    }
}

