namespace AdventOfCode2022.Day12;

public class HillClimbingAlgorithmTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 31)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 449)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 29)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 443)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, long expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(12, inputType);

        var answer = new HillClimbingAlgorithm(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}