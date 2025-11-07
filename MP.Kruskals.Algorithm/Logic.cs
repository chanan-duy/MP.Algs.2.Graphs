namespace MP.Kruskals.Algorithm;

public record Edge(char Source, char Destination, int Weight);

public class DisjointSet
{
    private readonly Dictionary<char, char> _parent;
    private readonly Dictionary<char, int> _rank;

    public DisjointSet(IEnumerable<char> vertices)
    {
        _parent = new Dictionary<char, char>();
        _rank = new Dictionary<char, int>();
        foreach (var v in vertices)
        {
            _parent[v] = v;
            _rank[v] = 0;
        }
    }

    public char Find(char i)
    {
        if (_parent[i] == i)
        {
            return i;
        }

        return _parent[i] = Find(_parent[i]);
    }

    public void Union(char a, char b)
    {
        var rootA = Find(a);
        var rootB = Find(b);

        if (rootA != rootB)
        {
            if (_rank[rootA] < _rank[rootB])
            {
                _parent[rootA] = rootB;
            }
            else if (_rank[rootA] > _rank[rootB])
            {
                _parent[rootB] = rootA;
            }
            else
            {
                _parent[rootB] = rootA;
                _rank[rootA]++;
            }
        }
    }
}

public static class Logic
{
    private static readonly Dictionary<char, Dictionary<char, int>> Graph = new()
    {
        ['a'] = new Dictionary<char, int> { ['b'] = 1, ['c'] = 7 },
        ['b'] = new Dictionary<char, int> { ['a'] = 1, ['c'] = 5, ['d'] = 4, ['e'] = 3 },
        ['c'] = new Dictionary<char, int> { ['a'] = 7, ['b'] = 5, ['e'] = 6 },
        ['d'] = new Dictionary<char, int> { ['b'] = 4, ['e'] = 2 },
        ['e'] = new Dictionary<char, int> { ['b'] = 3, ['c'] = 6, ['d'] = 2 },
    };

    private static readonly List<char> Vertices = Graph.Keys.ToList();

    public static void Run()
    {
        KruskalsAlgorithm();
    }

    private static void KruskalsAlgorithm()
    {
        var mst = new List<Edge>();
        var allEdges = new List<Edge>();

        var addedEdges = new HashSet<(char, char)>();
        foreach (var u in Graph.Keys)
        {
            foreach (var (v, weight) in Graph[u])
            {
                var edge = u < v ? (u, v) : (v, u);
                if (addedEdges.Add(edge))
                {
                    allEdges.Add(new Edge(u, v, weight));
                }
            }
        }

        allEdges.Sort((e1, e2) => e1.Weight.CompareTo(e2.Weight));

        var disjointSet = new DisjointSet(Vertices);

        Console.WriteLine("initial state: all edges sorted by weight");
        Console.WriteLine("  Edge   | Weight");
        foreach (var edge in allEdges)
        {
            Console.WriteLine($"  {edge.Source} - {edge.Destination}   | {edge.Weight}");
        }

        Console.WriteLine();

        var step = 0;
        foreach (var edge in allEdges)
        {
            step++;
            Console.WriteLine($"\nstep {step}: evaluating edge {edge.Source} - {edge.Destination} with weight {edge.Weight}");

            var root1 = disjointSet.Find(edge.Source);
            var root2 = disjointSet.Find(edge.Destination);

            Console.Write($"  - Find({edge.Source}) -> '{root1}', Find({edge.Destination}) -> '{root2}'.");

            if (root1 != root2)
            {
                mst.Add(edge);
                disjointSet.Union(edge.Source, edge.Destination);
                Console.WriteLine(" Vertices are in different sets. Edge accepted.");
                DisplayCurrentMst(mst);
            }
            else
            {
                Console.WriteLine(" Vertices are in the same set. Edge rejected (forms a cycle).");
            }
        }

        Console.WriteLine("\nfinished");
        DisplayFinalMst(mst);
    }

    private static void DisplayCurrentMst(List<Edge> mst)
    {
        Console.WriteLine("  current mst edges:");
        foreach (var edge in mst)
        {
            Console.WriteLine($"    {edge.Source} - {edge.Destination} ({edge.Weight})");
        }
    }

    private static void DisplayFinalMst(List<Edge> mst)
    {
        Console.WriteLine("final mst:");
        var totalWeight = 0;
        Console.WriteLine("   Edge   | Weight");
        foreach (var edge in mst)
        {
            totalWeight += edge.Weight;
            Console.WriteLine($"  {edge.Source} - {edge.Destination}   | {edge.Weight}");
        }

        Console.WriteLine($"final mst weight: {totalWeight}");
    }
}
