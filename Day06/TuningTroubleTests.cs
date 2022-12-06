namespace AdventOfCode2022.Day06;

public class TuningTroubleTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 7)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 1282)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 19)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 3513)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(6, inputType);

        var answer = new TuningTrouble(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}