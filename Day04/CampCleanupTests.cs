namespace AdventOfCode2022.Day04;

public class CampCleanupTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 2)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 595)]
    // [InlineData(ChallengePart.Part2, InputTypes.Example, 70)]
    // [InlineData(ChallengePart.Part2, InputTypes.Input, 0)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(4, inputType);

        var answer = new CampCleanup(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}