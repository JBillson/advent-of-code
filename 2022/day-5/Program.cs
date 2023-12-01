using System.Text.RegularExpressions;
namespace day5;

class Program{
    private static List<Instruction> _instructions = new List<Instruction>();
    private static int _stackNameLine = 0;
    private static List<string> _input = new List<string>();
    private static List<Stack> _stacks = new List<Stack>();
    static void Main(string[] args){
        _input = File.ReadAllLines("input.txt").ToList();

        Setup();

        // Part One
        // ExecuteInstructions(true);

        // Part Two
        ExecuteInstructions(false);
    }

    private static void Setup(){

        // get stack name line
        _stackNameLine = 0;
        for (int i = 0; i < _input.Count; i++)
        {
            if (_input[i] == ""){
                _stackNameLine = i - 1;
            }
        }

        // create empty stacks 
        for (int i = 0; i < _input[_stackNameLine].Count(); i++)
        {
            var line = _input[_stackNameLine];
            var character = line[i];
            if (character == ' ') continue;
            var stack = new Stack(){
                name = int.Parse(char.ToString(character)),
                position = i
            };
            _stacks.Add(stack);
        }

        // populate stacks with crates
        for (int i = 0; i < _stacks.Count(); i++)
        {
            var crateList = new List<Crate>();
            for (int j = 0; j < _input.Count(); j++)
            {
                if (j >= _stackNameLine) break;
                var crateName = _input[j][_stacks[i].position];
                if (crateName == ' ') continue;
                crateList.Add(new Crate(){
                    name = crateName,
                    stack = i + 1,
                    position = j + 1
                });
            }
            _stacks[i].crates = crateList;
        }
    }

    private static void ExecuteInstructions(bool isPartOne){
        for (int i = 0; i < _input.Count; i++)
        {
            if (i <= _stackNameLine + 1) continue;
            _instructions.Add(new Instruction(_input[i]));
        }

        foreach (var instruction in _instructions)
        {
            System.Console.WriteLine("-------------------");
            System.Console.WriteLine(instruction.instruction);

            var fromStack = _stacks[instruction.fromStack - 1];
            var toStack = _stacks[instruction.toStack - 1];
            
            System.Console.WriteLine("Stacks before moving");
            System.Console.WriteLine("Stack: " + fromStack.name);
            foreach (var crate in fromStack.crates)
                System.Console.WriteLine(crate.name);

            System.Console.WriteLine("Stack: " + toStack.name);
            foreach (var crate in toStack.crates)
                System.Console.WriteLine(crate.name);

            var cratesBeingMoved = fromStack.crates.Take(instruction.numberOfCratesToMove).ToList();
            System.Console.WriteLine($"Moving first {cratesBeingMoved.Count()} crates");

            foreach (var crate in cratesBeingMoved)
            {
                System.Console.WriteLine(crate.name);
                fromStack.crates.Remove(crate);
                if (isPartOne)
                    toStack.crates.Insert(0, crate);
            }

            if (!isPartOne)
                toStack.crates.InsertRange(0, cratesBeingMoved);

            System.Console.WriteLine("Stacks after moving");
            foreach (var stack in _stacks)
            {
                if (stack != fromStack && stack != toStack) continue;
                System.Console.WriteLine("Stack: " + stack.name);
                foreach (var crate in stack.crates)
                {
                    System.Console.WriteLine(crate.name);
                }
            }
        }

        var topCrates = new List<Crate>();
        foreach (var stack in _stacks)
        {
            topCrates.Add(stack.crates[0]);
        }

        System.Console.WriteLine("Top Crates:");
        foreach (var crate in topCrates)
        {
            System.Console.WriteLine(crate.name);
        }
    }
}

class Instruction{
    public string instruction = string.Empty;
    public int numberOfCratesToMove;
    public int fromStack;
    public int toStack;

    public Instruction(string instruction){
        this.instruction = instruction;
        string pattern = @"\d+";
        var matches = Regex.Matches(instruction, pattern);
        numberOfCratesToMove = int.Parse(matches[0].Value);
        fromStack = int.Parse(matches[1].Value);
        toStack = int.Parse(matches[2].Value);
    }
}

class Stack{
    public int position;
    public int name;
    public List<Crate> crates = new();
}

class Crate{
    public char name;
    public int stack;
    public int position;
}