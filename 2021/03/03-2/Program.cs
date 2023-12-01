var input = await File.ReadAllLinesAsync("./input.txt");

var o2GeneratorRatingAsBinary = GetO2GeneratorRatingAsBinary(input.ToList());
var co2ScrubberRatingAsBinary = GetCO2ScrubberRatingAsBinary(input.ToList());
var o2GeneratorRating = Convert.ToInt32(o2GeneratorRatingAsBinary, 2);
var co2ScrubberRating = Convert.ToInt32(co2ScrubberRatingAsBinary, 2);

// life support 
var lifeSupportRating = o2GeneratorRating * co2ScrubberRating;
Console.WriteLine($"Life Support Rating: {lifeSupportRating}");

// o2
string GetO2GeneratorRatingAsBinary(List<string> lines)
{
    // loop through input columns
    for (int i = 0; i < input[0].Count(); i++)
    {
        // return only remaining line
        if (lines.Count == 1)
            return lines[0];

        // get common bit in column
        var commonBit = GetCommonBitInColumn(lines, i, 1, true);

        // remove lines which don't start with the common bit
        lines = lines.Where(x => Char.GetNumericValue(x[i]) == commonBit).ToList();
    }

    return lines.FirstOrDefault() ?? "";
}

// co2
string GetCO2ScrubberRatingAsBinary(List<string> lines)
{
    // loop through input columns
    for (int i = 0; i < lines[0].Count(); i++)
    {
        // return only remaining line
        if (lines.Count == 1)
            return lines[0];

        // get least common bit in column
        var uncommonBit = GetCommonBitInColumn(lines, i, 0, false);

        // remove lines which don't start with the least common bit
        lines = lines.Where(x => Char.GetNumericValue(x[i]) == uncommonBit).ToList();
    }
    return lines.FirstOrDefault() ?? "";
}

int GetCommonBitInColumn(List<string> lines, int column, int defaultValue, bool mostCommon)
{
    var zeroes = 0;
    var ones = 0;

    foreach (var line in lines)
    {
        var bit = line[column];
        switch (bit)
        {
            case '0':
                zeroes++;
                break;
            case '1':
                ones++;
                break;
        }
    }
    if (zeroes > ones)
        return mostCommon ? 0 : 1;
    else if (ones > zeroes)
        return mostCommon ? 1 : 0;
    else
        return defaultValue;
}