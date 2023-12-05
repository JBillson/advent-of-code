using _2023._01;
using _2023._02;
using _2023._03;
using _2023._04;

namespace _2023;

public static class Program
{
    public static async Task Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Invalid Args.\n" +
                              "First Arg: Day (e.g. 01)\n" +
                              "Second Arg: Part (e.g. 1 or 2)");
            return;
        }

        var day = args[0];
        var part = Enum.Parse<Part>(args[1]);

        switch (day)
        {
            case "01":
            case "1":
                await Day1.Run(part);
                break;
            case "02":
            case "2":
                await Day2.Run(part);
                break;
            case "03":
            case"3":
                await Day3.Run(part);
                break;
            case "04":
            case"4":
                await Day4.Run(part);
                break;
            default:
                Console.WriteLine("Invalid Day.  Please indicate which day to run.");
                break;
        }
    }

    public static async Task<List<string>> ReadInputAsLinesAsync(string path)
    {
        var lines = await File.ReadAllLinesAsync(path);
        return lines.ToList();
    }

    public static async Task<string> ReadInputAsStringAsync(string path)
    {
        return await File.ReadAllTextAsync(path);
    }


    public enum Part
    {
        PartOne = 1,
        PartTwo = 2
    }
}