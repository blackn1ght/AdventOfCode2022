namespace AdventOfCode2022.Day09;

public class RopeBridgeTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 13)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 6384)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 1)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 2734)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, long expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(9, inputType);

        var answer = new RopeBridge(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}