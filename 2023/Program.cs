namespace _2023;

public static class Program
{
    private static string Input = "input.txt";
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
                Day1.Run(ReadInputAsLines(Input), Enum.Parse<Part>(args[1]));
                break;
            default:
                Console.WriteLine("Invalid Day.  Please indicate which day to run.");
                break;
        }
    }

    public static string[] ReadInputAsLines(string path)
    {
        var input = File.ReadAllLines("01/input.txt");
        return input;
    }

    public enum Part
    {
        PartOne = 1,
        PartTwo = 2
    }
}