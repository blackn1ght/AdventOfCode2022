namespace AdventOfCode2022.Day02;

public class RockPaperScissorsTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 15)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 15572)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 12)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 16098)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(2, inputType);

        var answer = new RockPaperScissors(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}