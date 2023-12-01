var input = await File.ReadAllLinesAsync("./input.txt");

int pos = 0;
int depth = 0;
int aim = 0;

foreach (var line in input)
{
    var dir = line.Split(' ')[0];
    var amt = Int32.Parse(line.Split(' ')[1]);
    switch (dir)
    {
        case "forward":
            pos += amt;
            depth += aim * amt;
            break;
        case "down":
            aim += amt;
            break;
        case "up":
            aim -= amt;
            break;
        default:
            Console.WriteLine($"Unknown direction: {dir}");
            break;
    }
}
int final = pos * depth;
Console.WriteLine($"p: {pos}, d:{depth}, f:{final}");