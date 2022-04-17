using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        var sw = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false };
        Console.SetOut(sw);

        string[] split = Console.ReadLine().Split(' ');
        int n = int.Parse(split[0]);
        int q = int.Parse(split[1]);

        long[] array = new long[n];

        string[] valueSplit = Console.ReadLine().Split(' ');
        for (int i = 0; i < n; i++)
        {
            array[i] = long.Parse(valueSplit[i]);
        }
        var tree = new BinaryIndexedTree(array);
        

        for (int i = 0; i < q; i++)
        {
            string[] vs = Console.ReadLine().Split(' ');
            int t = int.Parse(vs[0]);
            int x = int.Parse(vs[1]);
            long y = long.Parse(vs[2]);

            if (t == 0)
            {
                tree.UpdateTree(x, y);
            }
            else
            {
                Console.WriteLine($"{tree.GetSum(x, (int)y)}");
            }
        }
        Console.Out.Flush();
    }
}


/// <summary>
/// Represent classical realization of Fenwiсk tree or Binary Indexed tree.
///
/// BITree[0..n] --> Array that represents Binary Indexed Tree.
/// arr[0..n-1] --> Input array for which prefix sum is evaluated.
/// </summary>
public class BinaryIndexedTree
{
    private readonly long[] fenwickTree;

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryIndexedTree"/> class.
    /// Create Binary indexed tree from the given array.
    /// </summary>
    /// <param name="array">Initial array.</param>
    public BinaryIndexedTree(long[] array)
    {
        fenwickTree = new long[array.Length];

        for (var i = 0; i < array.Length; i++)
        {
            UpdateTree(i, array[i]);
        }
    }

    public long GetSum(int s, int e)
    {
        return GetSum(e) - GetSum(s);
    }

    /// <summary>
    /// This method assumes that the array is preprocessed and
    /// partial sums of array elements are stored in BITree[].
    /// </summary>
    /// <param name="index">The position to sum from.</param>
    /// <returns>Returns sum of arr[0..index].</returns>
    public long GetSum(int index)
    {
        long sum = 0;
        var startFrom = index;

        while (startFrom > 0)
        {
            sum += fenwickTree[startFrom - 1];
            startFrom -= startFrom & (-startFrom);
        }

        return sum;
    }

    /// <summary>
    /// Updates a node in Binary Index Tree at given index.
    /// The given value 'val' is added to BITree[i] and all of its ancestors in tree.
    /// </summary>
    /// <param name="index">Given index.</param>
    /// <param name="val">Value to be update on.</param>
    public void UpdateTree(int index, long val)
    {
        var startFrom = index + 1;

        while (startFrom <= fenwickTree.Length)
        {
            fenwickTree[startFrom - 1] += val;
            startFrom += startFrom & (-startFrom);
        }
    }
}
