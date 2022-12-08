namespace AdventOfCode2022.Day08;

public class TreetopTreeHouse : ChallengeBase<int>
{
    private readonly int[][] _treeGrid;
    public TreetopTreeHouse(string[] data) : base(data)
    {
        _treeGrid = data.Select(row => row.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
    }

    protected override int Part1()
    {
        var visibleCount = 0;
        
        ForEachGridCoordinate((x, y) => visibleCount += IsVisible(x, y) ? 1 : 0);

        return visibleCount;
    }

    protected override int Part2()
    {
        var scenicScores = new List<int>();

        ForEachGridCoordinate((x, y) => {
            scenicScores.Add(CalculateScenicScore(x, y));
        });

        return scenicScores.Max();
    }

    private void ForEachGridCoordinate(Action<int, int> action)
    {
        for (var y = 0; y < _treeGrid.Length; y++)
        {
            for (var x = 0; x < _treeGrid[y].Length; x++)
            {
                action(x, y);
            }
        }
    }

    private bool IsEdge(int x, int y) => (y == 0 || x == 0 || y == _treeGrid.Length - 1 || x == _treeGrid[y].Length - 1);

    private bool IsVisible(int x, int y)
    {
        if (IsEdge(x, y)) return true;

        var curr = _treeGrid[y][x];

        var visibleCount = 4;

        Func<int, int, bool> deductIfHidden = (int localX, int localY) => 
        {
            var prev = _treeGrid[localY][localX];
            if (prev >= curr)
            {
                visibleCount--;
                return true;
            }
            return false;
        };

        for (var topToBottomY = 1; topToBottomY <= y; topToBottomY++)
        {
            if (deductIfHidden(x, topToBottomY - 1)) break;
        }

        for (var bottomToTopY = _treeGrid.Length - 2; bottomToTopY >= y; bottomToTopY--)
        {
            if (deductIfHidden(x, bottomToTopY + 1)) break;
        }

        for (var leftToRightX = 1; leftToRightX <= x; leftToRightX++)
        {
            if (deductIfHidden(leftToRightX - 1, y)) break;
        }

        for (var rightToLeftX = _treeGrid.Length - 2; rightToLeftX >= x; rightToLeftX--)
        {
            if (deductIfHidden(rightToLeftX + 1, y)) break;
        }

        return visibleCount > 0;
    }

    public int CalculateScenicScore(int x, int y)
    {
        var thisTreeHeight = _treeGrid[y][x];

        var scores = new List<int>();

        if (y > 0)
        {
            var upBlocked = false;
            for (var up = y - 1; up >= 0; up--)
            {
                var otherTreeHeight = _treeGrid[up][x];
                if (otherTreeHeight >= thisTreeHeight)
                {
                    scores.Add(y - up);
                    upBlocked = true;
                    break;
                }
            }

            if (!upBlocked) scores.Add(y);
        }
        else
        {
            scores.Add(0);
        }

        if (y < _treeGrid.Length - 1)
        {
            var downBlocked = false;
            for (var down = y + 1; down < _treeGrid.Length; down++)
            {
                var otherTreeHeight = _treeGrid[down][x];
                if (otherTreeHeight >= thisTreeHeight)
                {
                    scores.Add(down - y);
                    downBlocked = true;
                    break;
                }
            }

            if (!downBlocked) scores.Add((_treeGrid.Length - 1) - y);
        }
        else
        {
            scores.Add(0);
        }

        if (x < _treeGrid[y].Length)
        {
            var rightBlocked = false;
            for (var right = x + 1; right < _treeGrid[y].Length; right++)
            {
                var otherTreeHeight = _treeGrid[y][right];
                if (otherTreeHeight >= thisTreeHeight)
                {
                    scores.Add(right - x);
                    rightBlocked = true;
                    break;
                }
            }

            if (!rightBlocked) scores.Add((_treeGrid[x].Length - 1) - x);
        }
        else
        {
            scores.Add(0);
        }

        if (x > 0)
        {
            var leftBlocked = false;
            for (var left = x - 1; left >= 0; left--)
            {
                var otherTreeHeight = _treeGrid[y][left];
                if (otherTreeHeight >= thisTreeHeight)
                {
                    scores.Add(x - left);
                    leftBlocked = true;
                    break;
                }
            }

            if (!leftBlocked) scores.Add(x);
        }
        else
        {
            scores.Add(0);
        }

        return scores.Skip(1).Aggregate(scores[0], (prev, curr) => prev * curr);
    }
}