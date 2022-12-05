namespace AdventOfCode2022.Day01;

public class CalorieCounting : ChallengeBase<int>
{
    public CalorieCounting(string[] data) : base(data)
    {
    }

    protected override int Part1() => GetCaloriesAvailablePerElf().Max();

    protected override int Part2() => GetCaloriesAvailablePerElf()
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();

    private List<int> GetCaloriesAvailablePerElf()
    {
        var noOfElves = ChallengeDataRows.Count(x => string.IsNullOrEmpty(x)) + 1;

        var caloriesPerElf = Enumerable.Repeat(0, noOfElves).ToList();

        var elfIndex = 0;

        foreach (var line in ChallengeDataRows)
        {
            if (!int.TryParse(line, out var calories))
            {
                elfIndex++;
                continue;
            }

            caloriesPerElf[elfIndex] += calories;
        }

        return caloriesPerElf;
    }

}