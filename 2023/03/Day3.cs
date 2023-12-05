﻿namespace _2023._03;

public class Day3
{
    private const string Input = "03/input.txt";

    public static async Task Run(Program.Part part)
    {
        var lines = await Program.ReadInputAsLinesAsync(Input);
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

        switch (part)
        {
            case Program.Part.PartOne:
                Console.WriteLine("Part One: " + finalNumbers.Sum(x => x.value));
                break;
            case Program.Part.PartTwo:
            {
                var stars = GetStarPositions(lines);
                foreach (var star in stars)
                {
                    Console.WriteLine($"Star: {star.Item1},{star.Item2}");
                }

                var totalGearRatioSum = 0;
                foreach (var (x, y)in stars)
                {
                    var adjacent = CountAdjacentNumbers(x, y, lines, numbers);
                    if (adjacent.Count == 2)
                    {
                        totalGearRatioSum += adjacent[0].value * adjacent[1].value;
                    }
                }

                Console.WriteLine("Part Two: " + totalGearRatioSum);
                break;
            }
        }
    }

    private static List<Number> CountAdjacentNumbers(int x, int y, List<string> lines, List<Number> numbers)
    {
        var results = new List<Number>();
        // left
        if (y != 0 && char.IsDigit(lines[x][y - 1]))
        {
            var number = numbers.FirstOrDefault(number => number.line == x && number.indices.Contains(y - 1));
            if (number != null && !results.Contains(number))
                results.Add(number);
        }

        // up left
        if (x != 0 && y != 0 && char.IsDigit(lines[x - 1][y - 1]))
        {
            var number = numbers.FirstOrDefault(number => number.line == x - 1 && number.indices.Contains(y - 1));
            if (number != null && !results.Contains(number))
                results.Add(number);
        }

        // up 
        if (x != 0 && char.IsDigit(lines[x - 1][y]))
        {
            var number = numbers.FirstOrDefault(number => number.line == x - 1 && number.indices.Contains(y));
            if (number != null && !results.Contains(number))
                results.Add(number);
        }

        // up right
        if (x != 0 && y != lines[x].Length - 1 && char.IsDigit(lines[x - 1][y + 1]))
        {
            var number = numbers.FirstOrDefault(number => number.line == x - 1 && number.indices.Contains(y + 1));
            if (number != null && !results.Contains(number))
                results.Add(number);
        }

        // right
        if (y != lines[x].Length - 1 && char.IsDigit(lines[x][y + 1]))
        {
            var number = numbers.FirstOrDefault(number => number.line == x && number.indices.Contains(y + 1));
            if (number != null && !results.Contains(number))
                results.Add(number);
        }

        // right down
        if (x != lines.Count - 1 && y != lines[x].Length - 1 && char.IsDigit(lines[x + 1][y + 1]))
        {
            var number = numbers.FirstOrDefault(number => number.line == x + 1 && number.indices.Contains(y + 1));
            if (number != null && !results.Contains(number))
                results.Add(number);
        }

        // down
        if (x != lines.Count - 1 && char.IsDigit(lines[x + 1][y]))
        {
            var number = numbers.FirstOrDefault(number => number.line == x + 1 && number.indices.Contains(y));
            if (number != null && !results.Contains(number))
                results.Add(number);
        }

        // left down
        if (x != lines.Count - 1 && y != 0 && char.IsDigit(lines[x + 1][y - 1]))
        {
            var number = numbers.FirstOrDefault(number => number.line == x + 1 && number.indices.Contains(y - 1));
            if (number != null && !results.Contains(number))
                results.Add(number);
        }

        return results;
    }

    private static List<(int, int)> GetStarPositions(List<string> lines)
    {
        var stars = new List<(int, int)>();
        for (var i = 0; i < lines.Count; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == '*')
                    stars.Add(new ValueTuple<int, int>(i, j));
            }
        }

        return stars;
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

    private static List<Number> GetNumbersFromInput(IReadOnlyList<string> lines)
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