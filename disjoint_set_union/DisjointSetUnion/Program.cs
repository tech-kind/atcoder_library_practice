using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        DisjointSet<int> ds = new DisjointSet<int>();

        string[] split = Console.ReadLine().Split(' ');
        int n = int.Parse(split[0]);
        int q = int.Parse(split[1]);
        Node<int>[] nodes = new Node<int>[n];

        for (int i = 0; i < n; i++)
        {
            nodes[i] = ds.MakeSet(-1);
        }

        for (int i = 0; i < q; i++)
        {
            string[] query = Console.ReadLine().Split(' ');
            int t = int.Parse(query[0]);
            int u1 = int.Parse(query[1]);
            int u2 = int.Parse(query[2]);

            if (nodes[u1].Data != u1)
                nodes[u1] = ds.MakeSet(u1);
            if (nodes[u2].Data != u2)
                nodes[u2] = ds.MakeSet(u2);

            if (t == 0)
            {
                ds.UnionSet(nodes[u1], nodes[u2]);
            }
            else
            {
                bool same = ds.FindSet(nodes[u1]).Data == ds.FindSet(nodes[u2]).Data;
                if (same)
                {
                    Console.WriteLine("1");
                }
                else
                {
                    Console.WriteLine("0");
                }
            }
        }
    }
}

/// <summary>
/// node class to be used by disjoint set to represent nodes in Disjoint Set forest.
/// </summary>
/// <typeparam name="T">generic type for data to be stored.</typeparam>
public class Node<T>
{
    public int Rank { get; set; }

    public Node<T> Parent { get; set; }

    public T Data { get; set; }

    public Node(T data)
    {
        Data = data;
        Parent = this;
    }
}

/// <summary>
/// Implementation of Disjoint Set with Union By Rank and Path Compression heuristics.
/// </summary>
/// <typeparam name="T"> generic type for implementation.</typeparam>
public class DisjointSet<T>
{
    /// <summary>
    /// make a new set and return its representative.
    /// </summary>
    /// <param name="x">element to add in to the DS.</param>
    /// <returns>representative of x.</returns>
    public Node<T> MakeSet(T x) => new Node<T>(x);

    /// <summary>
    /// find the representative of a certain node.
    /// </summary>
    /// <param name="node">node to find representative.</param>
    /// <returns>representative of x.</returns>
    public Node<T> FindSet(Node<T> node)
    {
        if (node != node.Parent)
        {
            node.Parent = FindSet(node.Parent);
        }

        return node.Parent;
    }

    /// <summary>
    /// merge two sets.
    /// </summary>
    /// <param name="x">first set member.</param>
    /// <param name="y">second set member.</param>
    public void UnionSet(Node<T> x, Node<T> y)
    {
        Node<T> nx = FindSet(x);
        Node<T> ny = FindSet(y);
        if (nx == ny)
        {
            return;
        }

        if (nx.Rank > ny.Rank)
        {
            ny.Parent = nx;
        }
        else if (ny.Rank > nx.Rank)
        {
            nx.Parent = ny;
        }
        else
        {
            nx.Parent = ny;
            ny.Rank++;
        }
    }
}
