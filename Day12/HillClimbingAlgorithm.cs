namespace AdventOfCode2022.Day12;

public class HillClimbingAlgorithm : ChallengeBase<int>
{   
    public HillClimbingAlgorithm(string[] data) : base(data)
    {
    }

    protected override int Part1()
    {
        var grid = ChallengeDataRows.Select(row => row.ToCharArray()).ToArray();

        Node start = null;
        Node end = null;

        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] == 'S') start = new Node(x, y, 'S');
                if (grid[y][x] == 'E') end = new Node(x, y, 'E');
            }
        }

        var pathsTraversed = ShortestPathFunction(grid, start)(end);

        return pathsTraversed.Count() - 1;
    }

    protected override int Part2()
    {
        var grid = ChallengeDataRows.Select(row => row.ToCharArray()).ToArray();

        var startingPoints = new List<Node>();

        Node end = null;

        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] == 'a') startingPoints.Add(new Node(x, y, 'a'));
                if (grid[y][x] == 'E') end = new Node(x, y, 'E');
            }
        }

        var pathLengths = new List<int>();

        foreach (var startingPoint in startingPoints)
        {
            var pathsTraversed = ShortestPathFunction(grid, startingPoint)(end);
            if (pathsTraversed.Any())
            {
                pathLengths.Add(pathsTraversed.Count() - 1);
            }
        }

        return pathLengths.Min();
    }

    private Func<Node, IEnumerable<Node>> ShortestPathFunction(char[][] grid, Node start)
    {
        var previous = new Dictionary<Node, Node>();

        var queue = new Queue<Node>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var parent = queue.Dequeue();
            var neigbours = GetNeigbouringNodes(grid, parent);

            foreach (var neighbour in neigbours)
            {
                if (previous.ContainsKey(neighbour)) continue;

                previous[neighbour] = parent;
                queue.Enqueue(neighbour);
            }
        }

        Func<Node, IEnumerable<Node>> shortestPathFunction = n => 
        {
            var path = new List<Node>();

            var current = n;

            while (!current.Equals(start))
            {
                path.Add(current);
                if (previous.ContainsKey(current))
                {
                    current = previous[current];
                }
                else
                {
                    return new List<Node>();
                }
            };

            path.Add(start);
            path.Reverse();

            return path;
        };

        return shortestPathFunction;
    }

    private IEnumerable<Node> GetNeigbouringNodes(char[][] grid, Node node)
    {
        var results = new List<Node>();

        if (node.Y > 0) results.Add(new Node(node.X, node.Y - 1, grid[node.Y - 1][node.X]));
        if (node.Y < grid.Length - 1) results.Add(new Node(node.X, node.Y + 1, grid[node.Y + 1][node.X]));
        if (node.X > 0) results.Add(new Node(node.X - 1, node.Y, grid[node.Y][node.X - 1]));
        if (node.X < grid[0].Length - 1) results.Add(new Node(node.X + 1, node.Y, grid[node.Y][node.X + 1]));

        return results
            .Where(n => node.Value + 1 == n.Value || node.Value == n.Value || node.Value > n.Value && n.Value != 'E' || n.Value == 'E' && node.Value == 'z' || node.Value == 'S' && n.Value == 'a');
    }
}

internal record Node(int X, int Y, char Value);