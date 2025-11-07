namespace MP.Prims.Algorithm;

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

    public static void Run()
    {
        PrimsAlgorithm();
    }

    private static void PrimsAlgorithm()
    {
        var parent = new Dictionary<char, char?>();
        var key = new Dictionary<char, int>();
        var mstSet = new Dictionary<char, bool>();
        var priorityQueue = new PriorityQueue<char, int>();

        foreach (var v in Vertices)
        {
            parent[v] = null;
            key[v] = int.MaxValue;
            mstSet[v] = false;
        }

        var startVertex = Vertices[0];
        key[startVertex] = 0;

        foreach (var v in Vertices)
        {
            priorityQueue.Enqueue(v, key[v]);
        }

        Console.WriteLine("initial state:");
        DisplayCurrentState(key, parent, mstSet);

        var step = 0;
        while (priorityQueue.Count > 0)
        {
            var u = priorityQueue.Dequeue();

            if (mstSet[u])
            {
                continue;
            }

            mstSet[u] = true;
            step++;

            Console.WriteLine($"\nstep {step}: select Vertex '{u}'");

            foreach (var (neighbor, weight) in Graph[u])
            {
                if (mstSet[neighbor])
                {
                    continue;
                }

                Console.Write($"  - neighbor '{neighbor}': edge weight ({weight}) vs current key ({FormatKey(key[neighbor])}).");
                if (weight < key[neighbor])
                {
                    parent[neighbor] = u;
                    key[neighbor] = weight;
                    priorityQueue.Enqueue(neighbor, key[neighbor]);
                    Console.WriteLine(" Key updated");
                }
                else
                {
                    Console.WriteLine(" No update");
                }
            }

            Console.WriteLine("\nstate after step:");
            DisplayCurrentState(key, parent, mstSet);
        }

        Console.WriteLine("finished");
        DisplayFinalMst(parent);
    }

    private static void DisplayCurrentState(Dictionary<char, int> key, Dictionary<char, char?> parent, Dictionary<char, bool> mstSet)
    {
        Console.WriteLine("   Vertex | In MST | Key      | Parent");
        foreach (var v in Vertices)
        {
            var inMstStr = mstSet[v] ? "true" : "false";
            var keyStr = FormatKey(key[v]);
            var parentStr = parent[v].HasValue ? parent[v].ToString() : "null";
            Console.WriteLine($"  {v,-7} | {inMstStr,-7}| {keyStr,-9}| {parentStr}");
        }
    }

    private static string FormatKey(int val)
    {
        return val == int.MaxValue ? "infinity" : val.ToString();
    }

    private static void DisplayFinalMst(Dictionary<char, char?> parent)
    {
        Console.WriteLine("final mst:");
        var totalWeight = 0;
        Console.WriteLine("   Edge   | Weight");
        foreach (var v in Vertices)
        {
            if (parent[v].HasValue)
            {
                var p = parent[v]!.Value;
                var weight = Graph[v][p];
                totalWeight += weight;
                Console.WriteLine($"  {p} - {v}   | {weight}");
            }
        }

        Console.WriteLine($"final mst weight: {totalWeight}");
    }
}
