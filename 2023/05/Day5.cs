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

        var seeds = input[0].Split(':')[1].Trim().Split(' ').Select(long.Parse).ToList();
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

        foreach (var seed in seeds)
        {
            var value = seed;
            foreach (var (key, map) in maps)
            {
                Console.WriteLine($"Map: {key}");
                var originalVal = value;
                foreach (var instruction in map)
                {
                    Console.WriteLine(
                        $"Checking value {originalVal} -> {instruction.SourceRangeStart}:{instruction.SourceRangeStart + instruction.RangeLength}");
                    if (originalVal >= instruction.SourceRangeStart &&
                        originalVal <= instruction.SourceRangeStart + instruction.RangeLength)
                    {
                        Console.WriteLine(
                            $"Value {originalVal} falls between {instruction.SourceRangeStart}:{instruction.SourceRangeStart + instruction.RangeLength}");
                        var diff = originalVal - instruction.SourceRangeStart;
                        value = instruction.DestinationRangeStart + diff;
                        var sourceMap = key.Split('-')[0];
                        var destinationMap = key.Split('-')[2];
                        Console.WriteLine($"{sourceMap} {originalVal} -> {destinationMap} {value}");
                    }
                }
            }

            locationValues.Add(value);
        }

        Console.WriteLine(JsonConvert.SerializeObject(locationValues));
        Console.WriteLine($"Part One: {locationValues.Min()}");
    }

    public class MapInstruction
    {
        public long DestinationRangeStart { get; set; }
        public long SourceRangeStart { get; set; }
        public long RangeLength { get; set; }
    }
}