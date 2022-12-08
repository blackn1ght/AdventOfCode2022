namespace AdventOfCode2022.Day07;

public class DriveSpaceTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 95437)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 1644735)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 24933642)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 1300850)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, long expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(7, inputType);

        var answer = new DriveSpace(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}