namespace AdventOfCode2022.Day05;

public class SupplyStacksTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, "CMZ")]
    [InlineData(ChallengePart.Part1, InputTypes.Input, "VRWBSFZWM")]
    [InlineData(ChallengePart.Part2, InputTypes.Example, "MCD")]
    [InlineData(ChallengePart.Part2, InputTypes.Input, "RBTWJWMCF")]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, string expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(5, inputType);

        var answer = new SupplyStacks(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}