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

    protected override int Part2() => throw new NotImplementedException();
}