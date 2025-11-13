namespace MP.BellmanFord.Algorithm;

public static class Logic
{
    private static readonly Dictionary<char, Dictionary<char, int>> Graph = new()
    {
        ['S'] = new Dictionary<char, int> { ['A'] = 5 },
        ['A'] = new Dictionary<char, int> { ['C'] = -3, ['E'] = 2 },
        ['B'] = new Dictionary<char, int> { ['F'] = -5 },
        ['C'] = new Dictionary<char, int> { ['B'] = 7, ['H'] = -3 },
        ['D'] = new Dictionary<char, int>(),
        ['E'] = new Dictionary<char, int>(),
        ['F'] = new Dictionary<char, int> { ['G'] = 2 },
        ['G'] = new Dictionary<char, int> { ['B'] = 4 },
        ['H'] = new Dictionary<char, int> { ['D'] = 1 },
    };

    private static readonly List<char> Vertices = Graph.Keys.ToList();

    public static void Run()
    {
        BellmanFordAlgorithm('S');
    }

    private static void BellmanFordAlgorithm(char startVertex)
    {
        var distance = new Dictionary<char, int>();
        var predecessor = new Dictionary<char, char?>();

        foreach (var v in Vertices)
        {
            distance[v] = int.MaxValue;
            predecessor[v] = null;
        }

        distance[startVertex] = 0;

        Console.WriteLine("initial state:");
        DisplayCurrentState(distance, predecessor);

        for (var i = 0; i < Vertices.Count - 1; i++)
        {
            Console.WriteLine($"\niteration {i + 1}:");
            foreach (var u in Vertices)
            {
                if (distance[u] == int.MaxValue)
                {
                    continue;
                }

                foreach (var (v, weight) in Graph[u])
                {
                    Console.Write(
                        $"  - edge ({u}, {v}): checking if {FormatDistance(distance[u])} + {weight} < {FormatDistance(distance[v])}.");
                    if (distance[u] + weight < distance[v])
                    {
                        distance[v] = distance[u] + weight;
                        predecessor[v] = u;
                        Console.WriteLine(" Distance updated");
                    }
                    else
                    {
                        Console.WriteLine(" No update");
                    }
                }
            }

            DisplayCurrentState(distance, predecessor);
        }

        foreach (var u in Vertices)
        {
            foreach (var (v, weight) in Graph[u])
            {
                if (distance[u] != int.MaxValue && distance[u] + weight < distance[v])
                {
                    Console.WriteLine("\ngraph contains a negative-weight cycle");
                    return;
                }
            }
        }

        Console.WriteLine("\nfinished");
        DisplayFinalDistances(startVertex, distance, predecessor);
    }

    private static void DisplayCurrentState(Dictionary<char, int> distance, Dictionary<char, char?> predecessor)
    {
        Console.WriteLine("   vertex | distance   | predecessor");
        foreach (var v in Vertices)
        {
            var distStr = FormatDistance(distance[v]);
            var predStr = predecessor[v].HasValue ? predecessor[v].ToString() : "null";
            Console.WriteLine($"  {v,-7} | {distStr,-11}| {predStr}");
        }
    }

    private static string FormatDistance(int val)
    {
        return val == int.MaxValue ? "infinity" : val.ToString();
    }

    private static void DisplayFinalDistances(char startVertex, Dictionary<char, int> distance, Dictionary<char, char?> predecessor)
    {
        Console.WriteLine($"\nshortest paths from '{startVertex}':");
        Console.WriteLine("   vertex | distance   | path");
        foreach (var v in Vertices)
        {
            var distStr = FormatDistance(distance[v]);
            var path = GetPath(startVertex, v, predecessor);
            Console.WriteLine($"  {v,-7} | {distStr,-11}| {path}");
        }
    }

    private static string GetPath(char startVertex, char endVertex, Dictionary<char, char?> predecessor)
    {
        if (predecessor[endVertex] == null && endVertex != startVertex)
        {
            return "no path";
        }

        var path = new List<char>();
        var current = endVertex;
        while (current != startVertex && predecessor[current].HasValue)
        {
            path.Add(current);
            current = predecessor[current]!.Value;
        }

        path.Add(startVertex);
        path.Reverse();
        return string.Join(" -> ", path);
    }
}
