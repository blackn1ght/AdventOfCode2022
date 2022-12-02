namespace AdventOfCode2022.Day01;

public class CalorieCountingTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 24000)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 69912)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 45000)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 208180)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(1, inputType);

        var answer = new CalorieCounting(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}