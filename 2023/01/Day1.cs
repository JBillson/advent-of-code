using System.Text;

namespace _2023._01;

public static class Day1
{
    private const string Input = "01/input.txt";

    private static readonly Dictionary<string, int> WordNumbers = new()
    {
        { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 },
        { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 },
    };

    public static async Task Run(Program.Part part)
    {
        var input = await Program.ReadInputAsLinesAsync(Input);
        var outputs = new List<int>();
        foreach (var line in input)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine($"Line: {line}");
            var numberString = GetNumbersFromLine(line, part == Program.Part.PartTwo);
            if (string.IsNullOrEmpty(numberString)) continue;
            var outputString = $"{numberString[0]}{numberString[^1]}";
            Console.WriteLine($"{numberString} => {outputString}");

            outputs.Add(int.Parse(outputString));
        }

        var output = outputs.Sum();
        Console.WriteLine($"Sum of Calibration Values: {output}");
    }

    private static string GetNumbersFromLine(string line, bool includeWordNumbers)
    {
        var tempWord = new StringBuilder();
        var numbers = new StringBuilder();
        for (var i = 0; i < line.Length; i++)
        {
            var x = line[i];
            if (char.IsDigit(x))
            {
                numbers.Append(x);
            }
            else if (includeWordNumbers)
            {
                if (!IsValidStartingLetter(x)) continue;

                for (var j = i; j < line.Length; j++)
                {
                    if (char.IsDigit(line[j])) continue;
                    tempWord.Append(line[j]);
                    if (WordNumbers.TryGetValue(tempWord.ToString(), out var result))
                    {
                        numbers.Append(result);
                        i = j;
                        break;
                    }
                }

                tempWord.Clear();
            }
        }

        return numbers.ToString();
    }

    private static bool IsValidStartingLetter(char letter)
    {
        var isValidStartingLetter = false;
        foreach (var s in WordNumbers.Keys.Where(x => x.StartsWith(letter)))
        {
            isValidStartingLetter = true;
        }

        return isValidStartingLetter;
    }
}