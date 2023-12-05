namespace _2023._04;

public class Day4
{
    private const string Input = "04/input.txt";

    public static async Task Run(Program.Part part)
    {
        var cards = await Program.ReadInputAsLinesAsync(Input);
        if (!cards.Any()) return;

        var totalPoints = 0;
        foreach (var card in cards)
        {
            var numbers = card.Split(':')[1];
            var winningNumbers = numbers.Split('|')[0].Split(' ').ToList();
            var myNumbers = numbers.Split('|')[1].Split(' ').ToList();

            var tmpList = winningNumbers.ToList();
            foreach (var winningNumber in tmpList.Where(string.IsNullOrWhiteSpace))
            {
                winningNumbers.Remove(winningNumber);
            }

            tmpList = myNumbers.ToList();
            foreach (var number in tmpList.Where(string.IsNullOrWhiteSpace))
            {
                myNumbers.Remove(number);
            }

            Console.WriteLine("------------------------");
            Console.WriteLine($"{card.Split(':')[0]}");
            Console.WriteLine("Winning Numbers: " + string.Join(',', winningNumbers));
            Console.WriteLine("My Numbers: " + string.Join(',', myNumbers));

            var cardPoints = 0;
            foreach (var number in myNumbers)
            {
                if (winningNumbers.Contains(number.Trim()))
                {
                    if (cardPoints == 0)
                        cardPoints++;
                    else
                        cardPoints *= 2;
                }
            }

            Console.WriteLine($"Card Points: {cardPoints}");

            totalPoints += cardPoints;
        }

        Console.WriteLine($"Part One: {totalPoints}");
    }
}