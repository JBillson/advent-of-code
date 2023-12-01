namespace day3;
class Program{

    static List<string> _input = new List<string>();

    static void Main(string[] args){
        _input = File.ReadAllLines("input.txt").ToList();

        // PartOne();
        PartTwo();
    }

    static void PartTwo(){
        var sum = 0;
        var list = new List<string>();
        var groups = new List<List<string>>();

        foreach (var line in _input)
        {
            list.Add(line);
            if (list.Count < 3)
                continue;

            groups.Add(list.ToList());
            list.Clear();
        }

        foreach (var group in groups)
        {
            var commonChar = GetCommonEntry(group);
            sum += GetValue(commonChar);
        }

        System.Console.WriteLine($"Sum: {sum}");
    }

    static char GetCommonEntry(List<string> list){
        var common = list[0].Intersect(list[1]).Intersect(list[2]);
        System.Console.WriteLine($"Common Char: {common.First()}");
        return common.First();
    }

    static void PartOne(){
        var sum = 0;
        foreach (var line in _input)
        {
            var length = line.Length;
            var firstCompartment = line.Take(length/2);
            var secondCompartment = line.TakeLast(length/2);

            var duplicates = firstCompartment.Intersect(secondCompartment).ToList();
            foreach (var duplicate in duplicates)
            {
                var value = GetValue(duplicate);
                sum += value;
            }
        }
        System.Console.WriteLine($"Sum: {sum}");
    }

    static int GetValue(char letter){
        char[] lowercase = new char[] {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
        char[] uppercase = new char[] {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
        if (lowercase.Contains(letter))
            return Array.IndexOf(lowercase, letter) + 1;
        if (uppercase.Contains(letter))
            return Array.IndexOf(uppercase, letter) + lowercase.Length + 1;
        
        throw new Exception("Invalid Letter contained in the input");
    }
}