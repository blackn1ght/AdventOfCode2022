namespace AdventOfCode2022.Day02;

public class RockPaperScissors : ChallengeBase
{
    private enum Shape
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    private enum Result
    {
        Win,
        Draw,
        Lose
    }

    private static readonly Dictionary<char, Shape> ShapeMap = new()
    { 
        { 'A', Shape.Rock},
        { 'B', Shape.Paper },
        { 'C', Shape.Scissors },
        { 'X', Shape.Rock },
        { 'Y', Shape.Paper },
        { 'Z', Shape.Scissors }
    };

    private static readonly Dictionary<char, Result> ResultMap = new()
    {
        { 'X', Result.Lose },
        { 'Y', Result.Draw },
        { 'Z', Result.Win }
    };

    public RockPaperScissors(string[] data) : base(data)
    {
    }

    protected override int Part1() => ParseDataIntoPlayerShapes().Sum(GetRoundScore);

    private List<KeyValuePair<Shape, Shape>> ParseDataIntoPlayerShapes() => ChallengeDataRows
        .Select(x => x.ToCharArray())
        .Select(x => new KeyValuePair<Shape, Shape>(ShapeMap[x[0]], ShapeMap[x[2]]))
        .ToList();

    private static int GetRoundScore(KeyValuePair<Shape, Shape> round)
    {
        var opponent = round.Key;
        var player = round.Value;

        return (opponent) switch
        {
            Shape.Rock when player == Shape.Paper => Win(player),
            Shape.Rock when player == Shape.Scissors => Lose(player),
            Shape.Paper when player == Shape.Scissors => Win(player),
            Shape.Paper when player == Shape.Rock => Lose(player),
            Shape.Scissors when player == Shape.Rock => Win(player),
            Shape.Scissors when player == Shape.Paper => Lose(player),
            _ => Draw(player)
        };
    }

    protected override int Part2() => ParseDataIntoPlayerShapesAndResults().Sum(GetScoreForRound2);

    private List<KeyValuePair<Shape, Result>> ParseDataIntoPlayerShapesAndResults() => ChallengeDataRows
        .Select(x => x.ToCharArray())
        .Select(x => new KeyValuePair<Shape, Result>(ShapeMap[x[0]], ResultMap[x[2]]))
        .ToList();

    private static int GetScoreForRound2(KeyValuePair<Shape, Result> round)
    {
        var opponent = round.Key;
        var endResult = round.Value;

        return (opponent) switch
        {
            Shape.Rock when endResult == Result.Draw => Draw(Shape.Rock),
            Shape.Rock when endResult == Result.Win => Win(Shape.Paper),
            Shape.Rock when endResult == Result.Lose => Lose(Shape.Scissors),
            Shape.Paper when endResult == Result.Draw => Draw(Shape.Paper),
            Shape.Paper when endResult == Result.Win => Win(Shape.Scissors),
            Shape.Paper when endResult == Result.Lose => Lose(Shape.Rock),
            Shape.Scissors when endResult == Result.Draw => Draw(Shape.Scissors),
            Shape.Scissors when endResult == Result.Win => Win(Shape.Rock),
            Shape.Scissors when endResult == Result.Lose => Lose(Shape.Paper),
            _ => 0
        };
    }

    private static int Win(Shape shape) => (int)shape + 6;
    private static int Draw(Shape shape) => (int)shape + 3;
    private static int Lose(Shape shape) => (int)shape;
}