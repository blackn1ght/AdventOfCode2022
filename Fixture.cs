using System;

namespace AdventOfCode2022;

public class Fixture
{
    public string[] Input { get; private set; }
    public string[] Example { get; private set; }

    public Fixture SetupForDay(int day)
    {
        Input = ReadFile(day, "input.txt");
        Example = ReadFile(day, "example.txt");

        return this;
    }

    public string[] GetData(InputTypes inputType)
    {
        return inputType switch 
        {
            InputTypes.Input => Input,
            InputTypes.Example => Example,
            _ => throw new ArgumentException($"Invalid inputType {Enum.GetName(inputType)}")
        };
    }

    private static string[] ReadFile(int day, string filename)
    {
        var file = "";
        var filepath = $"Day{day.ToString().PadLeft(2, '0')}/{filename}";
        using (var reader = new StreamReader(filepath))
        {
            file = reader.ReadToEnd();
        }

        return file.Split(Environment.NewLine);
    }
}