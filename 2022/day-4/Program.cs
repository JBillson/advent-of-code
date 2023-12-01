namespace day4;
class Program{
    private static List<string> _input = new List<string>();

    static void Main(string[] args){
        _input = File.ReadAllLines("input.txt").ToList();

        // PartOne();
        PartTwo();
    }

    static void PartOne(){
        var pairs = 0;

        foreach (var line in _input)
        {
            var split = line.Split(',');
            var left = split[0].Split('-');
            var right = split[1].Split('-');

            // left & right are the same
            if (int.Parse(left[0]) == int.Parse(right[0]) &&
                int.Parse(left[1]) == int.Parse(right[1])){
                    System.Console.WriteLine("LEFT = RIGHT");
                    pairs ++;
                    continue;
                }

            // left fully contains right
            if (int.Parse(left[0]) <= int.Parse(right[0]) &&
                int.Parse(left[1]) >= int.Parse(right[1])){
                    pairs ++;
                    System.Console.WriteLine($"LEFT: {split[0]}:{split[1]}");
                }
            
            // right fully contains left
            if (int.Parse(right[0]) <= int.Parse(left[0]) &&
                int.Parse(right[1]) >= int.Parse(left[1])){
                    System.Console.WriteLine($"RIGHT: {split[0]}:{split[1]}");
                    pairs ++;
                }
        }

        System.Console.WriteLine($"Total Pairs: {pairs}");
    }

    static void PartTwo(){
        var pairs = 0;

        foreach (var line in _input)
        {
            var split = line.Split(',');
            var left = split[0].Split('-');
            var right = split[1].Split('-');

            // left 0 falls in between right 0 and 1
            if (int.Parse(left[0]) >= int.Parse(right[0]) &&
                int.Parse(left[0]) <= int.Parse(right[1])){
                    pairs ++;
                    continue;
                }

            // left 1 falls in between right 0 and 1
            if (int.Parse(left[1]) >= int.Parse(right[0]) &&
                int.Parse(left[1]) <= int.Parse(right[1])){
                    pairs ++;
                    continue;
                }

            // right 0 falls in between left 0 and 1
            if (int.Parse(right[0]) >= int.Parse(left[0]) &&
                int.Parse(right[0]) <= int.Parse(left[1])){
                    pairs ++;
                    continue;
                }

            // right 1 falls in between left 0 and 1
            if (int.Parse(right[1]) >= int.Parse(left[0]) &&
                int.Parse(right[1]) <= int.Parse(left[1])){
                    pairs ++;
                    continue;
                }
        }

        System.Console.WriteLine($"Total Pairs: {pairs}");
    }
}
