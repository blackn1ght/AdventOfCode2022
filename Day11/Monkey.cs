using System.Numerics;

namespace AdventOfCode2022.Day11;

internal class Monkey
{
    private readonly MonkeyOperation _operation;
    private readonly MonkeyTest _test;
    private readonly bool _damageRelief;

    public Monkey(IEnumerable<BigInteger> startingItems, MonkeyOperation operation, MonkeyTest test, bool damageRelief)
    {
        Items = new Queue<BigInteger>(startingItems);
        _operation = operation;
        _test = test;
        _damageRelief = damageRelief;
    }
    
    public Queue<BigInteger> Items { get; }

    public BigInteger Inspections { get; private set; }

    public void InspectAndThrowToMonkey(Action<BigInteger, int> onThrowToMonkey)
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

    private BigInteger Inspect(BigInteger worryLevel)
    {
        var val1 = _operation.Val1 == "old" 
            ? worryLevel 
            : int.Parse(_operation.Val1);

        var newWorryLevel = _operation.Operation == "+"
            ? worryLevel + val1
            : worryLevel * val1;

        return _damageRelief
            ? (BigInteger)Math.Floor((decimal)newWorryLevel / 3)
            : newWorryLevel;
    }

    private int GetIndexMonkeyToThrowWorryLevelAt(BigInteger newWorryLevel)
    {
        return newWorryLevel % _test.DivisibleBy == 0 
            ? _test.MonkeyIndexTrue 
            : _test.MonkeyIndexFalse;
    }
}

internal record MonkeyOperation(string Val0, string Operation, string Val1);

internal record MonkeyTest(int DivisibleBy, int MonkeyIndexTrue, int MonkeyIndexFalse);