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
        var notVisible = new List<string>();
        
        for (var y = 0; y < _treeGrid.Length; y++)
        {
            for (var x = 0; x < _treeGrid[y].Length; x++)
            {
                if (IsVisible(x, y))
                {
                    visibleCount++;
                    
                } 
                else
                {
                    notVisible.Add($"{x},{y} => {_treeGrid[y][x]}");
                }
            }
        }

        return visibleCount;
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

    protected override int Part2() => 0;
}