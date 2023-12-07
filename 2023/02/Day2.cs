namespace _2023._02;

public class Day2
{
    private const string Input = "02/input.txt";

    private static readonly Dictionary<string, int> Maximums = new()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 }
    };

    public static async Task Run(Program.Part part)
    {
        var input = await Program.ReadInputAsLinesAsync(Input);
        if (!input.Any())
        {
            Console.WriteLine("ERROR: No input text found");
            return;
        }
        
        var gameIdTotal = 0;
        var totalPower = 0;
        foreach (var game in input)
        {
            var gameId = int.Parse(game.Split(':')[0].Split(' ')[1]);
            var gameTurns = game.Split(':')[1].Split(';');

            // part one
            var isValidGame = true;
            // part two
            var coloursUsed = new Dictionary<string, int>
            {
                { "red", 0 },
                { "green", 0 },
                { "blue", 0 },
            };

            foreach (var gameTurn in gameTurns)
            {
                var collections = gameTurn.Trim().Split(',');
                foreach (var collection in collections)
                {
                    var number = int.Parse(collection.Trim().Split(' ')[0]);
                    var colour = collection.Trim().Split(' ')[1];
                    switch (part)
                    {
                        case Program.Part.PartOne:
                            if (!Maximums.TryGetValue(colour, out var maxAmount)) continue;
                            if (number > maxAmount)
                                isValidGame = false;

                            break;
                        case Program.Part.PartTwo:
                            if (coloursUsed.TryGetValue(colour, out var count))
                            {
                                if (number > count)
                                    coloursUsed[colour] = number;
                            }

                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(part), part, null);
                    }

                    if (!isValidGame) break;
                }

                if (!isValidGame) break;
            }

            // part one
            if (isValidGame)
            {
                gameIdTotal += gameId;
            }

            // part two
            var values = coloursUsed.Values;
            var turnPower = 1;
            foreach (var value in values)
            {
                turnPower *= value;
            }

            totalPower += turnPower;
        }

        var answer = part == Program.Part.PartOne ? gameIdTotal.ToString() : totalPower.ToString();
        Console.WriteLine($"Answer: {answer}");
    }
}