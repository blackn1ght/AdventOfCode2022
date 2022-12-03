namespace AdventOfCode2022.Day03;

public class RucksackReorganization : ChallengeBase
{
    public RucksackReorganization(string[] data) : base(data)
    {
    }

    protected override int Part1() => ChallengeDataRows
        .Select(SplitRucksackIntoTwoCompartments)
        .Select(CommonItemBetweenTwoCompartments)
        .Sum(NumericalValueOfItem);

    private static string[] SplitRucksackIntoTwoCompartments(string rucksack) => new[] { 
        rucksack.Substring(0, rucksack.Length / 2), 
        rucksack.Substring(rucksack.Length / 2)
    };

    private static char CommonItemBetweenTwoCompartments(string[] compartments) => compartments[0].Intersect(compartments[1]).First();

    private static int NumericalValueOfItem(char item) => (int)item >= 65 && (int)item <= 90 ? (int)item - 38 : (int)item - 96;

    protected override int Part2() => 0;
}