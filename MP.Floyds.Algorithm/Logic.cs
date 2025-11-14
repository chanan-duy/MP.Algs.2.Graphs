namespace MP.Floyds.Algorithm;

public static class Logic
{
    private static readonly Dictionary<char, Dictionary<char, int>> Graph = new()
    {
        ['a'] = new Dictionary<char, int> { ['b'] = 3, ['c'] = 4, ['e'] = 1 },
        ['b'] = new Dictionary<char, int> { ['a'] = 3, ['c'] = 5 },
        ['c'] = new Dictionary<char, int> { ['a'] = 4, ['b'] = 5, ['e'] = 6, ['d'] = 2 },
        ['d'] = new Dictionary<char, int> { ['c'] = 2, ['e'] = 7 },
        ['e'] = new Dictionary<char, int> { ['a'] = 1, ['c'] = 6, ['d'] = 7 },
    };

    private static readonly List<char> Vertices = Graph.Keys.ToList();
    private const int Infinity = int.MaxValue;

    public static void Run()
    {
        FloydWarshallAlgorithm();
    }

    private static void FloydWarshallAlgorithm()
    {
        var dist = new Dictionary<char, Dictionary<char, int>>();
        var next = new Dictionary<char, Dictionary<char, char?>>();

        foreach (var u in Vertices)
        {
            dist[u] = new Dictionary<char, int>();
            next[u] = new Dictionary<char, char?>();
            foreach (var v in Vertices)
            {
                dist[u][v] = Infinity;
                next[u][v] = null;
            }

            dist[u][u] = 0;
        }

        foreach (var u in Vertices)
        {
            foreach (var (v, weight) in Graph[u])
            {
                dist[u][v] = weight;
                next[u][v] = v;
            }
        }

        Console.WriteLine("initial state:");
        DisplayMatrices(dist, next);

        for (var kIdx = 0; kIdx < Vertices.Count; kIdx++)
        {
            var k = Vertices[kIdx];
            Console.WriteLine($"\nstep {kIdx + 1}: looking at vertex '{k}'");

            foreach (var i in Vertices)
            {
                foreach (var j in Vertices)
                {
                    if (dist[i][k] != Infinity && dist[k][j] != Infinity && dist[i][k] + dist[k][j] < dist[i][j])
                    {
                        Console.WriteLine(
                            $"  - updating distance between '{i}' and '{j}'. New path: {i} -> {k} -> {j}. New distance: {dist[i][k] + dist[k][j]}");
                        dist[i][j] = dist[i][k] + dist[k][j];
                        next[i][j] = next[i][k];
                    }
                }
            }

            Console.WriteLine("\nstate after step:");
            DisplayMatrices(dist, next);
        }

        Console.WriteLine("finished");
        DisplayFinalDistances(dist);
        DisplayAllPaths(dist, next);
    }

    private static void DisplayMatrices(Dictionary<char, Dictionary<char, int>> dist, Dictionary<char, Dictionary<char, char?>> next)
    {
        Console.WriteLine("distance matrix:");
        Console.Write("     ");
        foreach (var v in Vertices)
        {
            Console.Write($"{v,-9}");
        }

        Console.WriteLine();

        foreach (var u in Vertices)
        {
            Console.Write($"{u,-5}");
            foreach (var v in Vertices)
            {
                Console.Write($"{FormatValue(dist[u][v]),-9}");
            }

            Console.WriteLine();
        }

        Console.WriteLine("\npredecessor matrix:");
        Console.Write("     ");
        foreach (var v in Vertices)
        {
            Console.Write($"{v,-9}");
        }

        Console.WriteLine();

        foreach (var u in Vertices)
        {
            Console.Write($"{u,-5}");
            foreach (var v in Vertices)
            {
                Console.Write($"{next[u][v]?.ToString() ?? "null",-9}");
            }

            Console.WriteLine();
        }
    }

    private static string FormatValue(int val)
    {
        return val == Infinity ? "infinity" : val.ToString();
    }

    private static void DisplayFinalDistances(Dictionary<char, Dictionary<char, int>> dist)
    {
        Console.WriteLine("\nfinal shortest path distances:");
        Console.Write("     ");
        foreach (var v in Vertices)
        {
            Console.Write($"{v,-9}");
        }

        Console.WriteLine();

        foreach (var u in Vertices)
        {
            Console.Write($"{u,-5}");
            foreach (var v in Vertices)
            {
                Console.Write($"{FormatValue(dist[u][v]),-9}");
            }

            Console.WriteLine();
        }
    }

    private static void DisplayAllPaths(Dictionary<char, Dictionary<char, int>> dist, Dictionary<char, Dictionary<char, char?>> next)
    {
        Console.WriteLine("\nall-pairs shortest paths:");
        foreach (var u in Vertices)
        {
            foreach (var v in Vertices)
            {
                if (u != v)
                {
                    var path = GetPath(u, v, next);
                    var distance = dist[u][v];
                    Console.WriteLine($"  {u} -> {v} | path: {path} | distance: {FormatValue(distance)}");
                }
            }
        }
    }

    private static string GetPath(char u, char v, Dictionary<char, Dictionary<char, char?>> next)
    {
        if (next[u][v] == null)
        {
            return "no path";
        }

        var path = new List<char> { u };
        var current = u;
        while (current != v)
        {
            current = next[current][v]!.Value;
            path.Add(current);
        }

        return string.Join(" -> ", path);
    }
}
