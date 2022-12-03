namespace AdventOfCode2022.Day03;

public class RucksackReorganizationTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 157)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 7875)]
    // [InlineData(ChallengePart.Part2, InputTypes.Example, 0)]
    // [InlineData(ChallengePart.Part2, InputTypes.Input, 0)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(3, inputType);

        var answer = new RucksackReorganization(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}