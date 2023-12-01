namespace day2;
public class Program{
    private static List<string> _input = new();
    private static int _totalScore;
    private static int _roundScore;

    private static void Main(string[] args){
        _input = File.ReadAllLines("input.txt").ToList();

        PartOne();
        PartTwo();
    }

    private static void PartOne(){
        _totalScore = 0;
        foreach (var line in _input)
        {
            _roundScore = 0;

            var opponent = line[0];
            var myShape = line[2];

            var winner = GetWinner(opponent, myShape);

            _roundScore += GetOutcomeScore(winner);
            _roundScore += GetShapeScore(line[2]);

            _totalScore += _roundScore;
        }

        System.Console.WriteLine($"Total Score: {_totalScore}");
    }
    
    private static void PartTwo(){
        _totalScore = 0;
        foreach (var line in _input)
        {
            _roundScore = 0;

            var opponent = line[0];
            var outcome = line[2];

            var shape = GetShapeToPlay(opponent, outcome);
            var winner = GetWinner(opponent, shape);
            _roundScore += GetOutcomeScore(winner);
            _roundScore += GetShapeScore(shape);

            _totalScore += _roundScore;
        }

        System.Console.WriteLine($"Total Adjusted Score: {_totalScore}");
    }
    
    private static char GetShapeToPlay(char opponent, char outcome){
        switch (outcome)
        {
            // lose
            case 'X':
                switch (opponent)
                {
                    case 'A':
                        return 'Z';
                    case 'B':
                        return 'X';
                    case 'C':
                        return 'Y';
                }
            break;
            // draw
            case 'Y':
                switch (opponent)
                {
                    case 'A':
                        return 'X';
                    case 'B':
                        return 'Y';
                    case 'C':
                        return 'Z';
                }
            break;
            // win
            case 'Z':
                switch (opponent)
                {
                    case 'A':
                        return 'Y';
                    case 'B':
                        return 'Z';
                    case 'C':
                        return 'X';
                }
            break;
        }

        throw new Exception("Error");
    }

    private static int GetOutcomeScore(int winner){
            switch (winner){
                case 0:
                    return 3;
                case 1:
                    return 0;
                case 2:
                    return 6;
                default: 
                    System.Console.WriteLine("Something went wrong!");
                break;
            }

            throw new Exception("Error");
    }

    private static int GetShapeScore(char shape){
        switch (shape)
        {
            case 'X':
                return 1;
            case 'Y':
                return 2;
            case 'Z':
                return 3;
        }
        throw new Exception("Error");
    }

    private static int GetWinner(char a, char b){

        switch (a)
        {
            // rock
            case 'A':
                switch (b)
                {
                    // rock
                    case 'X':
                        return 0;
                    // paper
                    case 'Y':
                        return 2;
                    // scissors
                    case 'Z':
                        return 1;
                }
            break;
            // paper
            case 'B':
                switch (b)
                {
                    // rock
                    case 'X':
                        return 1;
                    // paper
                    case 'Y':
                        return 0;
                    // scissors
                    case 'Z':
                        return 2;
                }
            break;
            // scissors
            case 'C':
                switch (b)
                {
                    // rock
                    case 'X':
                        return 2;
                    // paper
                    case 'Y':
                        return 1;
                    // scissors
                    case 'Z':
                        return 0;
                }
            break;
        }
        throw new Exception("Error");
    }
}
