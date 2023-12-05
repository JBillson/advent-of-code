namespace _2023._04;

public abstract class Day4
{
    private const string Input = "04/input.txt";

    public static async Task Run(Program.Part part)
    {
        var input = await Program.ReadInputAsLinesAsync(Input);
        if (!input.Any()) return;

        var cards = input.Select(ParseCard).ToList();

        if (part == Program.Part.PartOne)
        {
            PartOne(cards);
        }
        else
        {
            PartTwo(cards);
        }
    }

    private static void PartOne(List<Card> cards)
    {
        var totalPoints = 0;
        foreach (var card in cards)
        {
            var cardPoints = 0;
            foreach (var _ in card.MyNumbers.Where(number => card.WinningNumbers.Contains(number.Trim())))
            {
                if (cardPoints == 0)
                    cardPoints++;
                else
                    cardPoints *= 2;
            }

            totalPoints += cardPoints;
        }

        Console.WriteLine($"Part One: {totalPoints}");
    }

    private static void PartTwo(List<Card> cards)
    {
        var finalCards = new List<Card>();
        var newCards = cards;
        while (newCards.Count > 0)
        {
            finalCards.AddRange(newCards);
            newCards = ProcessCards(newCards, cards);
        }

        Console.WriteLine($"Part Two: {finalCards.Count}");
    }

    private static List<Card> ProcessCards(List<Card> cards, List<Card> originalCards)
    {
        var newCards = new List<Card>();
        cards = cards.Where(x => x.MatchingNumbers?.Count != 0).ToList();
        foreach (var card in cards)
        {
            for (var j = card.CardNumber; j < card.CardNumber + card.MatchingNumbers?.Count; j++)
            {
                newCards.Add(originalCards[j]);
            }
        }

        return newCards;
    }

    private static Card ParseCard(string card)
    {
        var cardNumber = int.Parse(card.Split(':')[0].Split(' ')[^1].Trim());
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

        return new Card(cardNumber, winningNumbers, myNumbers,
            myNumbers.Where(x => winningNumbers.Contains(x)).ToList());
    }
}

public class Card
{
    public Card(int cardNumber, List<string> myNumbers, List<string> winningNumbers, List<string>? matchingNumbers)
    {
        CardNumber = cardNumber;
        MyNumbers = myNumbers;
        WinningNumbers = winningNumbers;
        MatchingNumbers = matchingNumbers;
    }

    public int CardNumber { get; }
    public List<string> MyNumbers { get; }
    public List<string> WinningNumbers { get; }
    public List<string>? MatchingNumbers { get; }
}