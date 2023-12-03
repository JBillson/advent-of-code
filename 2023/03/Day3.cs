using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace _2023._03;

public class Day3
{
    private const string Input = "03/input.txt";

    public static async Task Run()
    {
        var input = await Program.ReadInputAsLinesAsync(Input);
        var lines = input.ToList();
        if (!lines.Any()) return;

        var numbers = GetNumbersFromInput(lines);
        var finalNumbers = new List<Number>();
        foreach (var number in numbers)
        {
            if (number.indices.Any(index => IsSymbolSurrounding(number.line, index, lines)))
            {
                finalNumbers.Add(number);
            }
        }

        Console.WriteLine("Answer: " + finalNumbers.Sum(x => x.value));
    }

    private static bool IsSymbol(char curChar)
    {
        return !char.IsDigit(curChar) && curChar != '.';
    }

    private static bool IsSymbolSurrounding(int x, int y, List<string> lines)
    {
        // left
        if (y != 0 && IsSymbol(lines[x][y - 1]))
            return true;

        // up left
        if (x != 0 && y != 0 && IsSymbol(lines[x - 1][y - 1]))
            return true;

        // up 
        if (x != 0 && IsSymbol(lines[x - 1][y]))
            return true;

        // up right
        if (x != 0 && y != lines[x].Length - 1 && IsSymbol(lines[x - 1][y + 1]))
            return true;

        // right
        if (y != lines[x].Length - 1 && IsSymbol(lines[x][y + 1]))
            return true;

        // right down
        if (x != lines.Count - 1 && y != lines[x].Length - 1 && IsSymbol(lines[x + 1][y + 1]))
            return true;

        // down
        if (x != lines.Count - 1 && IsSymbol(lines[x + 1][y]))
            return true;

        // left down
        if (x != lines.Count - 1 && y != 0 && IsSymbol(lines[x + 1][y - 1]))
            return true;
        
        return false;
    }

    private static IEnumerable<Number> GetNumbersFromInput(IReadOnlyList<string> lines)
    {
        var numbers = new List<Number>();
        for (var i = 0; i < lines.Count; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (!char.IsDigit(lines[i][j])) continue;

                var currentNumber = new Number
                {
                    line = i,
                    indices = new List<int>()
                };

                while (j < lines[i].Length && char.IsDigit(lines[i][j]))
                {
                    currentNumber.indices.Add(j);
                    j++;
                }

                var numberString = string.Empty;
                foreach (var index in currentNumber.indices)
                {
                    numberString += lines[i][index];
                }

                currentNumber.value = int.Parse(numberString);
                numbers.Add(currentNumber);
            }
        }

        return numbers;
    }

    public class Number
    {
        public int line;
        public List<int> indices;
        public int value;
    }
}