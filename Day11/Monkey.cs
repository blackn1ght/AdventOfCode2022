namespace AdventOfCode2022.Day11;

internal class Monkey
{
    private readonly MonkeyOperation _operation;

    public Monkey(IEnumerable<long> startingItems, MonkeyOperation operation, MonkeyTest test)
    {
        Items = new Queue<long>(startingItems);
        _operation = operation;
        Test = test;
    }
    
    public Queue<long> Items { get; }

    public long Inspections { get; private set; }

    public MonkeyTest Test { get; private set; }

    private Func<long, long> _damageReliefCalculation;

    public Monkey WithDamageReliefCalculation(Func<long, long> calculation)
    {
        _damageReliefCalculation = calculation;

        return this;
    }

    public void InspectAndThrowToMonkey(Action<long, int> onThrowToMonkey)
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

    private long Inspect(long worryLevel)
    {
        var val1 = _operation.Val1 == "old" 
            ? worryLevel 
            : int.Parse(_operation.Val1);

        var newWorryLevel = _operation.Operation == "+"
            ? worryLevel + val1
            : worryLevel * val1;

        return _damageReliefCalculation(newWorryLevel);
    }

    private int GetIndexMonkeyToThrowWorryLevelAt(long newWorryLevel)
    {
        return newWorryLevel % Test.DivisibleBy == 0 
            ? Test.MonkeyIndexTrue 
            : Test.MonkeyIndexFalse;
    }
}

internal record MonkeyOperation(string Val0, string Operation, string Val1);

internal record MonkeyTest(int DivisibleBy, int MonkeyIndexTrue, int MonkeyIndexFalse);