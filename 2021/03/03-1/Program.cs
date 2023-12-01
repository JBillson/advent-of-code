var lines = await File.ReadAllLinesAsync("./input.txt");

string gammaBinary = "";
string epsilonBinary = "";
int gammaAsInt = 0;
int epsilonAsInt = 0;
int powerConsumption = 0;


for (int x = 0; x < lines[0].Length; x++)
{
    var zeroes = 0;
    var ones = 0;
    for (int i = 0; i < lines.Length; i++)
    {
        if (lines[i][x] == '0')
            zeroes++;
        else if (lines[i][x] == '1')
            ones++;
        else
        {

            Console.WriteLine("Input is not binary!");
            return;
        }
    }

    if (zeroes > ones)
    {

        gammaBinary += 0;
        epsilonBinary += 1;
    }
    else if (ones > zeroes)
    {

        gammaBinary += 1;
        epsilonBinary += 0;
    }
    else
        Console.WriteLine($"Even amount of ones and zeroes in column {x}");
}

gammaAsInt = Convert.ToInt32(gammaBinary, 2);
epsilonAsInt = Convert.ToInt32(epsilonBinary, 2);
powerConsumption = gammaAsInt * epsilonAsInt;
Console.WriteLine($"Power Consumption: {powerConsumption}");