namespace AdventOfCode2022.Day09;

public class RopeBridge : ChallengeBase<int>
{   public RopeBridge(string[] data) : base(data)
    {
    }

    protected override int Part1() => GetNumberOfTimesTailVisitedUniquePositions(2);

    protected override int Part2() => GetNumberOfTimesTailVisitedUniquePositions(10);

    private Coordinate MoveKnot(Coordinate knotInFront, Coordinate knot)
    {
        var xDiff = knotInFront.X - knot.X;
        var yDiff = knotInFront.Y - knot.Y;

        // Tail is on top of head, or directly next to it
        if (yDiff == 0 && xDiff == 0) return knot;
        if (yDiff == 0 && (xDiff == 1 || xDiff == -1)) return knot;
        if ((yDiff == 1 || yDiff == -1) && xDiff == 0) return knot;

        // Same y, moved right
        if (yDiff == 0 && xDiff > 1) return new Coordinate(knot.X+1, knot.Y);

        // Same y, moved left
        if (yDiff == 0 && xDiff < 1) return new Coordinate(knot.X-1, knot.Y);

        // Same x, moved up
        if (yDiff > 1 && xDiff == 0) return new Coordinate(knot.X, knot.Y+1);

        // Same x, moved Down
        if (yDiff < 1 && xDiff == 0) return new Coordinate(knot.X, knot.Y-1);

        // Diag nearby, do nothing
        if ((yDiff == 1 || yDiff == -1) && (xDiff == 1 || xDiff == -1)) return knot;

        // 2 to the right, 1 up
        if (xDiff >= 1 && yDiff >= 1) return new Coordinate(knot.X+1, knot.Y+1);

        // 2 to the right, 1 down
        if (xDiff >= 1 && yDiff <= 1) return new Coordinate(knot.X+1, knot.Y-1);

        // 2 to the left, 1 up
        if (xDiff < 1 && yDiff >= 1) return new Coordinate(knot.X-1, knot.Y+1);

        // 2 to the left, 1 down
        if (xDiff < 1 && yDiff < 1) return new Coordinate(knot.X-1, knot.Y-1);

        return knot;
    }

    private int GetNumberOfTimesTailVisitedUniquePositions(int knotCount)
    {
        var tailPositions = new HashSet<Coordinate>
        {
            new Coordinate(0,0)
        };

        var knots = Enumerable.Range(0, knotCount)
            .Select(_ => new Coordinate(0, 0))
            .ToList();

        var head = knots.First();
        var tail = knots.Last();

        foreach (var input in ChallengeDataRows)
        {
            var row = input.Split(' ');
            var direction = row[0];
            var move = int.Parse(row[1]);

            for (var i = 0; i < move; i++)
            {
                if (direction == "R") head.X++;
                if (direction == "L") head.X--;
                if (direction == "U") head.Y++;
                if (direction == "D") head.Y--;

                for (var knotIndex = 1; knotIndex < knotCount; knotIndex++)
                {
                    var previousKnot = knots[knotIndex - 1];
                    var currentKnot = knots[knotIndex];

                    var moveResult = MoveKnot(previousKnot, currentKnot);
                    if (moveResult.X != currentKnot.X || moveResult.Y != currentKnot.Y)
                    {
                        knots[knotIndex] = moveResult;

                        if (knotIndex == knotCount - 1) tailPositions.Add(moveResult);
                    }
                }
            }

        }

        return tailPositions.Count;
    }
}

internal class Coordinate
{
    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public override bool Equals(object? obj)
    {
        var coord = (Coordinate)obj;

        return coord != null && coord.X == X && coord.Y == Y;
    }

    public override int GetHashCode() => this.X.GetHashCode() ^ this.Y.GetHashCode();
}