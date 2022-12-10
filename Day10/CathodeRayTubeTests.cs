namespace AdventOfCode2022.Day10;

public class CathodeRayTubeTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 13140)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 13720)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 0)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 0)]      // The answer is 'FBURHZCH'
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, long expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(10, inputType);

        var answer = new CathodeRayTube(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}