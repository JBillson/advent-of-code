namespace day6;

class Program{
    private static string _input = string.Empty;
    private static List<char> _charList = new();
    private static int _distinctCharsRequired;

    static void Main(string[] args){
        _input = File.ReadAllText("input.txt");

        // Part One
        // _distinctCharsRequired = 4;
        // Part Two
        _distinctCharsRequired = 14;

        var charCount = 0;
        foreach (var x in _input)
        {
            charCount ++;
            _charList.Add(x);

            if (_charList.Count > _distinctCharsRequired){
                _charList.RemoveAt(0);
            }

            if (_charList.Count == _charList.Distinct().Count() && _charList.Count == _distinctCharsRequired){
                System.Console.WriteLine($"Marker found after {charCount} characters");
                break;
            }
        }
    }
}