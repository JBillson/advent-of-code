using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day1;
class Program{
    static void Main(string[] args){

        // read input text
        var input = File.ReadAllLines("day-1/input.txt");
        // create list to hold total calories
        List<int> totals = new List<int>();

        // calculate totals
        var currentCalorieCount = 0;
        foreach (var line in input)
        {
            if (line == "") {
                totals.Add(currentCalorieCount);
                currentCalorieCount = 0;
                continue;
            }

            var isValid = int.TryParse(line, out int calories);
            if (!isValid) continue;
            currentCalorieCount += calories;
        }

        // order by descending and take top 3
        totals = totals.OrderByDescending(x => x).ToList();
        var top3 = totals.Take(3);

        // log top 3 carriers
        Console.WriteLine("Top 3 Carriers:");
        foreach (var calories in top3)
            Console.WriteLine(calories);

        // sum total in top 3
        var summedTotal = 0;
        foreach (var total in top3)
            summedTotal += total;

        // log summed total
        Console.WriteLine($"Summed Total: {summedTotal}");
    }
}