var input = await File.ReadAllLinesAsync("./input.txt");

int previous = 0;
int largerCount = 0;

foreach (var line in input)
{
    var number = Int32.Parse(line);

    if (previous == 0)
    {

        previous = number;
        continue;
    }

    if (number > previous)
        largerCount++;

    previous = number;
}

Console.WriteLine(largerCount);