using System.Numerics;

namespace AdventOfCode2022.Day11;

internal static class MonkeyParser
{
    public static Monkey CreateMonkey(string[] monkeyLines, bool damageRelief)
    {
        var startingItems = ParseStartingItems(monkeyLines);
        var operation = ParseOperation(monkeyLines);
        var test = ParseTest(monkeyLines);

        return new Monkey(startingItems, operation, test, damageRelief);
    }

    private static List<BigInteger> ParseStartingItems(string[] monkeyLines)
    {
        return monkeyLines[1]
            .Split(':')[1]
            .Split(',')
            .Select(value => BigInteger.Parse(value.Trim()))
            .ToList();
    }

    private static MonkeyOperation ParseOperation(string[] monkeyLines)
    {
        var parts = monkeyLines[2].Split(':')[1].Split(' ');
        return new MonkeyOperation(parts[3], parts[4], parts[5]);
    }

    private static MonkeyTest ParseTest(string[] monkeyLines)
    {
        var divisible = int.Parse(monkeyLines[3].Split(' ').Last());
        var trueMonkey = int.Parse(monkeyLines[4].Split(' ').Last());
        var falseMonkey = int.Parse(monkeyLines[5].Split(' ').Last());

        return new MonkeyTest(divisible, trueMonkey, falseMonkey);
    }
}