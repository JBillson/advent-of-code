using _2023._01;

namespace _2023;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Invalid Args.\n" +
                              "First Arg: Day (e.g. 01)\n" +
                              "Second Arg: Part (e.g. 1 or 2)");
            return;
        }

        switch (args[0])
        {
            case "01":
            case "1":
                Day1.Run(Enum.Parse<Part>(args[1]));
                break;
            default:
                Console.WriteLine("Invalid Day.  Please indicate which day to run.");
                break;
        }
    }

    public static async Task<IEnumerable<string>> ReadInputAsLinesAsync(string path)
    {
        return await File.ReadAllLinesAsync(path);
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