using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day05;

public class SupplyStacks : ChallengeBase<string>
{
    private static string InstructionRegex = @"move\s(\d*)\sfrom\s(\d*)\sto\s(\d*)";

    public SupplyStacks(string[] data) : base(data) {}

    protected override string Part1() => MoveCrates(ByStacking);

    protected override string Part2() => MoveCrates(ByRetainingOrder);

    private string MoveCrates(Action<List<Stack<char>>,Instruction> moveCrates)
    {
        var indexOfBlankLine = ChallengeDataRows
            .ToList()
            .IndexOf(string.Empty);

        var stacks = CreateStacks(indexOfBlankLine);

        for (var i = indexOfBlankLine + 1; i < ChallengeDataRows.Length; i++)
        {
            moveCrates(stacks, CreateInstruction(i));
        }

        return GetTopCrates(stacks);
    }

    private Instruction CreateInstruction(int rowIndex)
    {
        var result = Regex.Match(ChallengeDataRows[rowIndex], InstructionRegex)
            .Groups.Values
            .Skip(1).Take(3)
            .Select(x => int.Parse(x.Value))
            .ToList();

        return new Instruction(result[0], result[1] - 1, result[2] - 1);
    }

    private List<Stack<char>> CreateStacks(int indexOfBlankLine)
    {
        var stackList = ChallengeDataRows[indexOfBlankLine-1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(_ => new Stack<char>())
            .ToList();

        for (var i = indexOfBlankLine - 2; i >= 0; i--) 
        {
            var charIndex = 1;
            for (var r = 0; r < stackList.Count; r++)
            {
                var crate = ChallengeDataRows[i].Substring(charIndex, 1);

                if (crate != " ")
                {
                    stackList[r].Push(Convert.ToChar(crate));
                }

                charIndex += 4;
            }
        }

        return stackList;
    }

    private string GetTopCrates(List<Stack<char>> stacks) => stacks.Aggregate("",(prev, curr) => $"{prev}{curr.Pop().ToString()}");

    private void ByStacking(List<Stack<char>> stacks, Instruction instruction)
    {
        for (var x = 0; x < instruction.Move; x++)
        {
            var c = stacks[instruction.From].Pop();
            stacks[instruction.To].Push(c);
        }
    }

    private void ByRetainingOrder(List<Stack<char>> stacks, Instruction instruction)
    {
        var cratesToPush = new List<char>();

        for (var x = 0; x < instruction.Move; x++)
        {
            cratesToPush.Insert(0, stacks[instruction.From].Pop());
        }

        foreach (var crate in cratesToPush)
        {
            stacks[instruction.To].Push(crate);
        }
    }
}

public record Instruction(int Move, int From, int To);