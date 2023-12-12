using Newtonsoft.Json;

namespace _2023._06;

public class Day6
{
    private const string Input = "06/input.txt";

    public static async Task Run(Program.Part part)
    {
        var input = await Program.ReadInputAsLinesAsync(Input);
        if (!input.Any())
        {
            Console.WriteLine("ERROR: No input text found");
            return;
        }

        var times = input[0].Split(':')[1].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(int.Parse)
            .ToList();
        var distances = input[1].Split(':')[1].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(int.Parse)
            .ToList();

        var totalWaysToBeatRecord = new List<int>();
        for (var i = 0; i < times.Count; i++)
        {
            var duration = times[i];
            var record = distances[i];

            var waysToBeatRecord = 0;
            for (var j = 0; j < duration; j++)
            {
                var distance = j * (duration - j);
                if (distance > record)
                {
                    waysToBeatRecord++;
                }
            }

            totalWaysToBeatRecord.Add(waysToBeatRecord);
        }


        var answer = totalWaysToBeatRecord.Aggregate(1, (current, i) => current * i);
        Console.WriteLine($"Part One: {answer}");
    }
}