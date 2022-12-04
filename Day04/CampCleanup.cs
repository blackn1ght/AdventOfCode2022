namespace AdventOfCode2022.Day04;

public class CampCleanup : ChallengeBase
{
    public CampCleanup(string[] data) : base(data)
    {
    }

    protected override int Part1() => ChallengeDataRows
            .Select(ThePairAssignments)
            .Count(OverlappingPairs);

    private static bool OverlappingPairs((Range pair1, Range pair2) pairs) => 
        pairs.pair1.Contains(pairs.pair2) 
        || pairs.pair2.Contains(pairs.pair1);

    private static bool RangeContains(Range range0, Range range1)
    {
        return range0.Start.Value >= range1.Start.Value
            && range0.End.Value <= range1.End.Value;
    }

    private static (Range pair1, Range pair2) ThePairAssignments(string row)
    {
        var pairs = row.Split(',');

        Func<string, Range> ToRange = (pair) => {
            var assignments = pair.Split('-').Select(int.Parse).ToArray();
            return new Range(assignments[0], assignments[1]);
        };

        return new (ToRange(pairs[0]), ToRange(pairs[1]));
    }

    protected override int Part2() => 0;


}

public static class RangeExtensions
{
    public static bool Contains(this Range value, Range otherRange) => 
        value.Start.Value >= otherRange.Start.Value
        && value.End.Value <= otherRange.End.Value;
}