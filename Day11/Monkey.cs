namespace AdventOfCode2022.Day11;

internal class Monkey
{
    private readonly MonkeyOperation _operation;
    private readonly MonkeyTest _test;

    public Monkey(IEnumerable<int> startingItems, MonkeyOperation operation, MonkeyTest test)
    {
        Items = new Queue<int>(startingItems);
        _operation = operation;
        _test = test;
    }
    
    public Queue<int> Items { get; }

    public int Inspections { get; private set; }

    public void InspectAndThrowToMonkey(Action<int, int> onThrowToMonkey)
    {
        foreach (var item in Items)
        {
            var newWorryLevel = Inspect(item);
            var monkeyIndexToThrowTo = GetIndexMonkeyToThrowWorryLevelAt(newWorryLevel);

            onThrowToMonkey(newWorryLevel, monkeyIndexToThrowTo);
            Inspections++;
        }

        Items.Clear();
    }

    private int Inspect(int worryLevel)
    {
        var val1 = _operation.Val1 == "old" ? worryLevel : int.Parse(_operation.Val1);

        var newWorryLevel = _operation.Operation switch
        {
            "+" => worryLevel + val1,
            "*" => worryLevel * val1
        };

        return (int)Math.Floor((decimal)newWorryLevel / 3);
    }

    private int GetIndexMonkeyToThrowWorryLevelAt(int newWorryLevel)
    {
        return newWorryLevel % _test.DivisibleBy == 0 
            ? _test.MonkeyIndexTrue 
            : _test.MonkeyIndexFalse;
    }
}

internal record MonkeyOperation(string Val0, string Operation, string Val1);

internal record MonkeyTest(int DivisibleBy, int MonkeyIndexTrue, int MonkeyIndexFalse);