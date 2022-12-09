namespace AdventOfCode2022.Day09;

public class RopeBridge : ChallengeBase<int>
{   public RopeBridge(string[] data) : base(data)
    {
    }

    protected override int Part1()
    {
        var results = new Dictionary<(int x, int y),int>
        {
            { (0,0), 1}
        };

        int tailX = 0;
        int tailY = 0;

        int headX = 0;
        int headY = 0;

        foreach (var input in ChallengeDataRows)
        {
            var row = input.Split(' ');
            var direction = row[0];
            var move = int.Parse(row[1]);

            for (var i = 0; i < move; i++)
            {
                if (direction == "R") headX++;
                if (direction == "L") headX--;
                if (direction == "U") headY++;
                if (direction == "D") headY--;

                var (resultX, resultY) = MoveTail(headX, headY, tailX, tailY);
                if (resultX != tailX || resultY != tailY)
                {
                    tailX = resultX;
                    tailY = resultY;

                    results.Increment((resultX, resultY));
                }
            }
        }

        return results.Count;
    }

    private (int x, int y) MoveTail(int headX, int headY, int tailX, int tailY)
    {
        var xDiff = headX - tailX;
        var yDiff = headY - tailY;

        // Tail is on top of head, or directly next to it
        if (yDiff == 0 && xDiff == 0) return (tailX, tailY);
        if (yDiff == 0 && (xDiff == 1 || xDiff == -1)) return (tailX, tailY);
        if ((yDiff == 1 || yDiff == -1) && xDiff == 0) return (tailX, tailY);

        // Same y, moved right
        if (yDiff == 0 && xDiff > 1) return (tailX+1, tailY);

        // Same y, moved left
        if (yDiff == 0 && xDiff < 1) return (tailX-1, tailY);

        // Same x, moved up
        if (yDiff > 1 && xDiff == 0) return (tailX, tailY+1);

        // Same x, moved Down
        if (yDiff < 1 && xDiff == 0) return (tailX, tailY-1);

        // Diagnolly nearby, do nothing
        if ((yDiff == 1 || yDiff == -1) && (xDiff == 1 || xDiff == -1)) return (tailX, tailY);

        // 1 to the right, 2 up
        if (xDiff == 1 && yDiff > 1) return (tailX+1, tailY+1);

        // 1 to the left, 2 up
        if (xDiff == -1 && yDiff > 1) return (tailX-1, tailY+1);

        // 1 to the right, 2 down
        if (xDiff == 1 && yDiff < 1) return (tailX+1, tailY-1);

        // 1 to the left, 2 down
        if (xDiff == -1 && yDiff < 1) return (tailX-1, tailY-1);

        // 2 to the right, 1 up
        if (xDiff > 1 && yDiff == 1) return (tailX+1, tailY+1);

        // 2 to the right, 1 down
        if (xDiff > 1 && yDiff == -1) return (tailX+1, tailY-1);

        // 2 to the left, 1 up
        if (xDiff < 1 && yDiff == 1) return (tailX-1, tailY+1);

        // 2 to the left, 1 down
        if (xDiff < 1 && yDiff == -1) return (tailX-1, tailY-1);

        return (tailX, tailY);
    }


    protected override int Part2() => throw new NotImplementedException();
}

public static class DictionaryExtensions
{
    public static void Increment(this Dictionary<(int x, int y), int> dictionary, (int x, int y) key)
    {
        if (!dictionary.ContainsKey(key)) dictionary.Add(key, 0);

        dictionary[key]++;
    }
}