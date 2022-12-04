namespace AdventOfCode2022.Day04;

public class CampCleanup : ChallengeBase
{
    public CampCleanup(string[] data) : base(data) {}

    protected override int Part1() => ChallengeDataRows
        .Select(PairAssigmmentsToRanges)
        .Count(PairsThatContainOtherPair);

    private static bool PairsThatContainOtherPair((Range pair1, Range pair2) pairs) => 
        pairs.pair1.Contains(pairs.pair2) 
        || pairs.pair2.Contains(pairs.pair1);

    private static (Range pair1, Range pair2) PairAssigmmentsToRanges(string row)
    {
        Func<string, Range> ToRange = (pair) => {
            var assignments = pair.Split('-').Select(int.Parse).ToArray();
            return new Range(assignments[0], assignments[1]);
        };

        var pairs = row.Split(',');

        return new (ToRange(pairs[0]), ToRange(pairs[1]));
    }

    protected override int Part2() => ChallengeDataRows
        .Select(PairAssigmmentsToRanges)
        .Count(PairsThatOverlap);

    private static bool PairsThatOverlap((Range pair1, Range pair2) pairs) => 
        pairs.pair1.Overlaps(pairs.pair2);
}

public static class RangeExtensions
{
    public static bool Contains(this Range thisRange, Range otherRange) => 
        thisRange.Start.Value >= otherRange.Start.Value
        && thisRange.End.Value <= otherRange.End.Value;

    public static bool Overlaps(this Range thisRange, Range otherRange) =>
        thisRange.End.Value >= otherRange.Start.Value
        && thisRange.Start.Value <= otherRange.End.Value;
}