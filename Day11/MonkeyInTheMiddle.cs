namespace AdventOfCode2022.Day11;

public class MonkeyInTheMiddle : ChallengeBase<int>
{   
    public MonkeyInTheMiddle(string[] data) : base(data)
    {
    }

    protected override int Part1()
    {
        var monkeys = CreateMonkeysFromInput();

        for (var round = 0; round < 20; round++)
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
            .Aggregate(1, (prev, curr) => prev * curr.Inspections);
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

    protected override int Part2() => 0;
}