namespace AdventOfCode2022.Day06;

public class TuningTrouble : ChallengeBase<int>
{
    public TuningTrouble(string[] data) : base(data) {}

    protected override int Part1() => GetStartOfMessageIndex(4);

    protected override int Part2() => GetStartOfMessageIndex(14);

    private int GetStartOfMessageIndex(int startMarkerLen)
    {
        for (var index = 0; index >= ChallengeDataRows.Length - startMarkerLen; index++)
        {
            if (IsStartOfMessageMarker(index, startMarkerLen)) return index + startMarkerLen;
        }

        return -1;
    }

    private bool IsStartOfMessageMarker(int index, int markerLen) =>
        ChallengeDataRows[0].Substring(index, markerLen).ToCharArray().Distinct().Count() == markerLen;
}
