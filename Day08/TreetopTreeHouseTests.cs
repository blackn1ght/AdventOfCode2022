namespace AdventOfCode2022.Day08;

public class TreetopTreeHouseTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 21)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 1695)]
    // [InlineData(ChallengePart.Part2, InputTypes.Example, 0)]
    // [InlineData(ChallengePart.Part2, InputTypes.Input, 0)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, long expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(8, inputType);

        var answer = new TreetopTreeHouse(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}