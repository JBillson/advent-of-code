var input = await File.ReadAllLinesAsync("./input.txt");

int previousCollection = 0;
int larger = 0;

for (int i = 0; i < input.Length; i++)
{
    if (i + 3 > input.Length) break;

    var collection = Int32.Parse(input[i]) + Int32.Parse(input[i + 1]) + Int32.Parse(input[i + 2]);
    if (collection > previousCollection && previousCollection != 0)
        larger++;

    previousCollection = collection;
}
Console.WriteLine(larger);


