using Newtonsoft.Json;

namespace _2023._05;

public class Day5
{
    private const string Input = "05/input.txt";

    public static async Task Run(Program.Part part)
    {
        var input = await Program.ReadInputAsLinesAsync(Input);
        if (!input.Any())
        {
            Console.WriteLine("ERROR: No input text found");
            return;
        }

        BruteForceMethod(part, input);
    }

    private static void BruteForceMethod(Program.Part part, List<string> input)
    {
        var seeds = new List<long>();
        if (part == Program.Part.PartOne)
        {
            seeds = input[0].Split(':')[1].Trim().Split(' ').Select(long.Parse).ToList();
        }
        else
        {
            var seedInput = input[0].Split(':')[1].Trim().Split(' ').Select(long.Parse).ToList();
            for (var i = 0; i < seedInput.Count; i += 2)
            {
                var start = seedInput[i];
                var count = seedInput[i + 1];
                for (var j = start; j < start + count; j++)
                {
                    seeds.Add(j);
                }
            }
        }

        input.RemoveRange(0, 2);

        var maps = new Dictionary<string, List<MapInstruction>>();
        var lastMapFound = string.Empty;
        foreach (var line in input.Where(line => !string.IsNullOrWhiteSpace(line)))
        {
            if (line.Contains(':'))
            {
                var mapName = line.Split(' ')[0];
                lastMapFound = mapName;
                maps.Add(mapName, new List<MapInstruction>());
            }
            else
            {
                var numbers = line.Split(' ').Select(long.Parse).ToList();
                maps[lastMapFound].Add(new MapInstruction
                {
                    DestinationRangeStart = numbers[0],
                    SourceRangeStart = numbers[1],
                    RangeLength = numbers[2]
                });
            }
        }

        var locationValues = new List<long>();
        var orderedMaps = new Dictionary<string, List<MapInstruction>>();
        foreach (var (key, map) in maps)
        {
            orderedMaps.Add(key, map.OrderBy(x => x.SourceRangeStart).ToList());
        }

        Console.WriteLine($"Evaluating {seeds.Count} seeds");
        for (var i = 0; i < seeds.Count; i++)
        {
            var seed = seeds[i];
            var value = seed;
            foreach (var (key, map) in maps)
            {
                if (seed == 0)
                    Console.WriteLine($"Map: {key}");
                var originalVal = value;
                foreach (var instruction in map)
                {
                    if (seed == 0)
                        Console.WriteLine(
                            $"Checking value {originalVal} -> {instruction.SourceRangeStart}:{instruction.SourceRangeStart + instruction.RangeLength}");
                    if (originalVal >= instruction.SourceRangeStart &&
                        originalVal < instruction.SourceRangeStart + instruction.RangeLength)
                    {
                        if (seed == 0)
                            Console.WriteLine(
                                $"Value {originalVal} falls between {instruction.SourceRangeStart}:{instruction.SourceRangeStart + instruction.RangeLength}");
                        var diff = originalVal - instruction.SourceRangeStart;
                        value = instruction.DestinationRangeStart + diff;
                        var sourceMap = key.Split('-')[0];
                        var destinationMap = key.Split('-')[2];
                        if (seed == 0)
                            Console.WriteLine($"{sourceMap} {originalVal} -> {destinationMap} {value}");
                    }
                }
            }

            locationValues.Add(value);
            Console.WriteLine($"{i + 1}/{seeds.Count} seeds evaluated");
        }

        Console.WriteLine(JsonConvert.SerializeObject(locationValues));
        Console.WriteLine(part == Program.Part.PartOne
            ? $"Part One: {locationValues.Min()}"
            : $"Part Two: {locationValues.Min()}");
    }

    private class MapInstruction
    {
        public long DestinationRangeStart { get; set; }
        public long SourceRangeStart { get; set; }
        public long RangeLength { get; set; }
    }
}