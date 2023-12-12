using System.Text;

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

        var times = new List<long>();
        var distances = new List<long>();
        if (part == Program.Part.PartOne)
        {
            times = input[0].Split(':')[1].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(long.Parse).ToList();
            distances = input[1].Split(':')[1].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(long.Parse).ToList();
        }
        else
        {
            var timeNumbers = input[0].Split(':')[1].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();
            var timeSb = new StringBuilder();
            foreach (var number in timeNumbers)
            {
                timeSb.Append(number);
            }

            var time = long.Parse(timeSb.ToString());

            var distanceNumbers = input[1].Split(':')[1].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();
            var distanceSb = new StringBuilder();
            foreach (var number in distanceNumbers)
            {
                distanceSb.Append(number);
            }

            var distance = long.Parse(distanceSb.ToString());

            times.Add(time);
            distances.Add(distance);
        }

        var totalWaysToBeatRecord = new List<long>();
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


        var answer = totalWaysToBeatRecord.Aggregate<long, long>(1, (current, l) => current * l);

        Console.WriteLine($"Answer: {answer}");
    }
}