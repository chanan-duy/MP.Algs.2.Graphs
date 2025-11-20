namespace MP.Algs.Final;

public static class Logic
{
    public static void Run()
    {
        using var reader = new StreamReader(Console.OpenStandardInput());
        using var writer = new StreamWriter(Console.OpenStandardOutput());

        var n = ReadInt(reader);
        var m = ReadInt(reader);

        var edges = new Edge[m];

        for (var i = 0; i < m; i++)
        {
            edges[i] = new Edge(ReadInt(reader), ReadInt(reader), ReadInt(reader));
        }

        Array.Sort(edges);

        var dsu = new Dsu(n);
        long mstWeight = 0;
        var edgesCount = 0;

        var treeAdj = new List<(int to, int w)>[n + 1];
        for (var i = 1; i <= n; i++)
        {
            treeAdj[i] = [];
        }

        foreach (var edge in edges)
        {
            if (dsu.Union(edge.U, edge.V))
            {
                mstWeight += edge.Weight;
                edgesCount++;

                treeAdj[edge.U].Add((edge.V, edge.Weight));
                treeAdj[edge.V].Add((edge.U, edge.Weight));
            }
        }

        if (edgesCount < n - 1 && n > 1)
        {
            writer.WriteLine("-1");
            return;
        }

        if (n == 1 && edgesCount == 0)
        {
            writer.WriteLine("0 0");
            return;
        }

        var (farthestNode, _) = BfsMaxDist(1, n, treeAdj);

        var (_, diameter) = BfsMaxDist(farthestNode, n, treeAdj);

        writer.WriteLine($"{mstWeight} {diameter}");
    }

    private static (int node, long dist) BfsMaxDist(int startNode, int n, List<(int to, int w)>[] adj)
    {
        var dist = new long[n + 1];
        Array.Fill(dist, -1);

        var queue = new Queue<int>();
        queue.Enqueue(startNode);
        dist[startNode] = 0;

        var maxNode = startNode;
        long maxDist = 0;

        while (queue.Count > 0)
        {
            var u = queue.Dequeue();

            if (dist[u] > maxDist)
            {
                maxDist = dist[u];
                maxNode = u;
            }

            foreach (var edge in adj[u])
            {
                if (dist[edge.to] == -1)
                {
                    dist[edge.to] = dist[u] + edge.w;
                    queue.Enqueue(edge.to);
                }
            }
        }

        return (maxNode, maxDist);
    }

    private static int ReadInt(StreamReader reader)
    {
        var c = reader.Read();
        while (c <= ' ' && c != -1)
        {
            c = reader.Read();
        }

        if (c == -1)
        {
            return 0;
        }

        var negative = false;
        if (c == '-')
        {
            negative = true;
            c = reader.Read();
        }

        var res = 0;
        while (c > ' ')
        {
            res = res * 10 + (c - '0');
            c = reader.Read();
        }

        return negative ? -res : res;
    }

    private readonly struct Edge : IComparable<Edge>
    {
        public int U { get; }
        public int V { get; }
        public int Weight { get; }

        public Edge(int u, int v, int weight)
        {
            U = u;
            V = v;
            Weight = weight;
        }

        public int CompareTo(Edge other)
        {
            return Weight.CompareTo(other.Weight);
        }
    }

    private class Dsu
    {
        private readonly int[] _parent;
        private readonly int[] _rank;

        public Dsu(int size)
        {
            _parent = new int[size + 1];
            _rank = new int[size + 1];
            for (var i = 1; i <= size; i++)
            {
                _parent[i] = i;
            }
        }

        private int Find(int i)
        {
            if (_parent[i] != i)
            {
                _parent[i] = Find(_parent[i]);
            }

            return _parent[i];
        }

        public bool Union(int i, int j)
        {
            var rootI = Find(i);
            var rootJ = Find(j);

            if (rootI == rootJ)
            {
                return false;
            }

            if (_rank[rootI] < _rank[rootJ])
            {
                _parent[rootI] = rootJ;
            }
            else
            {
                _parent[rootJ] = rootI;
                if (_rank[rootI] == _rank[rootJ])
                {
                    _rank[rootI]++;
                }
            }

            return true;
        }
    }
}
