using System.Numerics;

namespace AdventOfCode2022.Day11;

public class MonkeyInTheMiddle : ChallengeBase<long>
{   
    public MonkeyInTheMiddle(string[] data) : base(data)
    {
    }

    protected override long Part1() => RunSimulation(20, false);

    protected override long Part2() => RunSimulation(10000, true);

    public long RunSimulation(int rounds, bool useCustomWorryLevelCalculation)
    {
        var monkeys = CreateMonkeysFromInput();

        var worryLevelCalculation = GetWorryLevelCalculation(useCustomWorryLevelCalculation, monkeys);

        for (var round = 0; round < rounds; round++)
        {
            foreach (var monkey in monkeys)
            {
                monkey.WithDamageReliefCalculation(worryLevelCalculation).InspectAndThrowToMonkey((worryLevel, monkeyId) =>
                {
                    monkeys[monkeyId].Items.Enqueue(worryLevel);
                });
            }
        }

        return monkeys
            .OrderByDescending(m => m.Inspections)
            .Take(2)
            .Aggregate((long)1, (prev, curr) => prev * curr.Inspections);
    }

    private Func<long, long> GetWorryLevelCalculation(bool customCalculation, List<Monkey> monkeys)
    {
        if (customCalculation == false)
        {
            return (newWorryLevel) => (long)Math.Floor((decimal)newWorryLevel / 3);
        }
        
        var lcm = monkeys.Aggregate((long)1, (acc, curr) => acc * curr.Test.DivisibleBy);

        return (newWorryLevel) => newWorryLevel % lcm;
    }

    private List<Monkey> CreateMonkeysFromInput()
    {
        var position = 0;
        var monkeys = new List<Monkey>();

        do
        {
            var monkeyData = ChallengeDataRows.Skip(position).Take(6).ToArray();

            monkeys.Add(MonkeyParser.CreateMonkey(monkeyData));

            position += 7;
        }
        while (position < ChallengeDataRows.Length);

        return monkeys;
    }

    
}