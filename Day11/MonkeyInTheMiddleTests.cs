namespace AdventOfCode2022.Day11;

public class MonkeyInTheMiddleTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 10605)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 110264)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 2713310158)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 23612457316)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, long expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(11, inputType);

        var answer = new MonkeyInTheMiddle(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}