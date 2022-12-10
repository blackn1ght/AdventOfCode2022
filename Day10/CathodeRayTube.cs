namespace AdventOfCode2022.Day10;

public class CathodeRayTube : ChallengeBase<int>
{   
    public CathodeRayTube(string[] data) : base(data)
    {
    }

    protected override int Part1()
    {
        var cycle = 0;
        var cyclePoint = 20;
        var signalStrengths = new List<int>();
        var x = 1;

        Action executeCycle = () => 
        {
            cycle++;
            if (cycle == cyclePoint)
            {
                signalStrengths.Add(x * cyclePoint);
                cyclePoint += 40;
            }
        };

        foreach (var instruction in ChallengeDataRows)
        {
            var details = instruction.Split(' ');

            if (details[0] == "addx")
            {
                for (var i = 0 ; i < 2; i++)
                {
                    executeCycle();
                }
                
                x += int.Parse(details[1]);
            }
            else
            { 
                executeCycle();
            }
        }

        return signalStrengths.Sum();
    }

    protected override int Part2()
    {
        var cycle = 0;
        var row = 0;
        var x = 1;

        var display = Enumerable
            .Range(0, 6)
            .Select(_ => "")
            .ToList();

        Action executeCycle = () => 
        {
            if (cycle > 39)
            {
                row++;
                cycle = 0;
            }
            display[row] = $"{display[row]}{(x == cycle || x == cycle - 1 || x == cycle + 1 ? "▓" : " ")}";
            cycle++;
        };

        foreach (var instruction in ChallengeDataRows)
        {
            var details = instruction.Split(' ');

            if (details[0] == "addx")
            {
                for (var i = 0 ; i < 2; i++)
                {
                    executeCycle();
                }
                
                x += int.Parse(details[1]);
            }
            else
            { 
                executeCycle();
            }
        }

        display.ForEach(row => Console.WriteLine(row));

        return 0;
    }
}