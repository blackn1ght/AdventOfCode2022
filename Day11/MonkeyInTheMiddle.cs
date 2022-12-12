using System.Numerics;

namespace AdventOfCode2022.Day11;

public class MonkeyInTheMiddle : ChallengeBase<BigInteger>
{   
    public MonkeyInTheMiddle(string[] data) : base(data)
    {
    }

    protected override BigInteger Part1() => RunSimulation(20, true);

    private BigInteger RunSimulation(int rounds, bool damageRelief)
    {
        var monkeys = CreateMonkeysFromInput(damageRelief);

        for (var round = 0; round < rounds; round++)
        {
            foreach (var monkey in monkeys)
            {
                monkey.InspectAndThrowToMonkey((worryLevel, monkeyId) =>
                {
                    monkeys[monkeyId].Items.Enqueue(worryLevel);
                });
            }
        }

        return monkeys
            .OrderByDescending(m => m.Inspections)
            .Take(2)
            .Aggregate((BigInteger)1, (prev, curr) => prev * curr.Inspections);
    }

    private List<Monkey> CreateMonkeysFromInput(bool damageRelief)
    {
        var position = 0;
        var monkeys = new List<Monkey>();

        do
        {
            var monkeyData = ChallengeDataRows.Skip(position).Take(6).ToArray();

            monkeys.Add(MonkeyParser.CreateMonkey(monkeyData, damageRelief));

            position += 7;
        }
        while (position < ChallengeDataRows.Length);

        return monkeys;
    }

    protected override BigInteger Part2() => RunSimulation(1000, false);
}