namespace AdventOfCode2022;

public abstract class ChallengeBase
{
    protected readonly string[] ChallengeDataRows;

    public ChallengeBase(string[] data)
    {
        ChallengeDataRows = data;
    }

    public int GetAnswerForPart(ChallengePart part)
    {
        return part switch
        {
            ChallengePart.Part1 => Part1(),
            ChallengePart.Part2 => Part2()
        };
    }

    protected abstract int Part1();
    protected abstract int Part2();
}