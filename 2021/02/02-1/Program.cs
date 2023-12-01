var input = await File.ReadAllLinesAsync("./input.txt");

int pos = 0;
int depth = 0;

foreach (var line in input)
{
    var dir = line.Split(' ')[0];
    var amt = Int32.Parse(line.Split(' ')[1]);
    switch (dir)
    {
        case "forward":
            pos += amt;
            break;
        case "down":
            depth += amt;
            break;
        case "up":
            depth -= amt;
            break;
        default:
            Console.WriteLine($"Unknown direction: {dir}");
            break;
    }
}
int final = pos * depth;
Console.WriteLine($"{final}");