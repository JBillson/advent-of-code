namespace day7;

class Program{
    private static List<string> _input = new(); 
    private static List<Directory> _allDirs = new();
    private static Directory _currentDir; 
    private static int _sumOfDirSizes = 0;
    static void Main(string[] args){
        _input = System.IO.File.ReadAllLines("input.txt").ToList();

        // create root directory
        var root = new Directory("/");
        _allDirs.Add(root);

        // parse input
        ParseInput();

        // LogFileStructure(_allDirs);

        // start depth first search from root directory
        DFS(root);

        System.Console.WriteLine($"Sum of Directory Sizes: {_sumOfDirSizes}");
    }

    static void DFS(Directory directory){
        System.Console.WriteLine($"Analysing directory {directory.name}");

        // sum file sizes in dir
        foreach (var file in directory.files)
            directory.totalSize += file.size;

        foreach (var dir in directory.directories){
            // continue dfs
            DFS(dir);

            // directory sizes can count files twice
            directory.totalSize += dir.totalSize;
        }

        // add to size of valid directories 
        if (directory.totalSize <= 100000){
            _sumOfDirSizes += directory.totalSize;
            System.Console.WriteLine($"[{directory.name}] Size: {directory.totalSize}");
        }
    }

    static void ParseInput(){
        System.Console.WriteLine("Parsing Input");
        foreach (var line in _input)
        {
            System.Console.WriteLine($"{line}");
            // command
            if (line.Contains("$")){
                // nav somewhere
                if (line.Contains("$ cd ")){
                    // nav back one dir
                    if (line.Contains("$ cd ..")){
                        // we are at root and trying to navigate back a directory?
                        if (_currentDir.parentDirectory == null){
                            continue;
                        }

                        _currentDir = _currentDir.parentDirectory;
                        continue;
                    }

                    // nav new dir
                    var dirName = line.Remove(0, 5);
                    var matchingDirs = _allDirs.Where(x => x.name == dirName).ToList();
                    if (matchingDirs.Count > 1){
                        // this approach for parsing the input doesn't account for mutliple directories 
                        // with the same name.  I'll have to rethink my strategy for this.

                        // depth first search (DFS) is the correct approach for looping through the directories
                        // and summing their sizes.
                        throw new Exception($"Multiple directories matching name {dirName}");
                    }
                    _currentDir = matchingDirs[0];
                }
                continue;
            }

            // directory
            if (line.Contains("dir ")){
                // create dir and set parent dir
                var dir = new Directory(line.Remove(0, 4));
                dir.parentDirectory = _currentDir;

                _allDirs.Add(dir);
                _currentDir.directories.Add(dir);
                continue;
            }

            // file
            var split = line.Split(" ");
            var file = new File(split[1], int.Parse(split[0]));
            _currentDir.files.Add(file);
        }
    }
    static void LogFileStructure(List<Directory> allDirs){
        // log file structure
        foreach (var dir in allDirs)
        {
            System.Console.WriteLine("--------------------");
            System.Console.WriteLine($"Directory: {dir.name}");

            if (dir.files.Count > 0){
                System.Console.WriteLine("-------");
                System.Console.WriteLine("Files:");
                foreach (var file in dir.files)
                {
                    System.Console.WriteLine($"{file.name}:{file.size}");
                }
            }

            if (dir.directories.Count > 0){
                System.Console.WriteLine("-------");
                System.Console.WriteLine("Directories:");
                foreach (var directory in dir.directories)
                {
                    System.Console.WriteLine(directory.name);
                }
            }
        }
    }
}

class Directory{
    public string name = string.Empty;
    public Directory parentDirectory;
    public List<Directory> directories = new();
    public List<File> files = new();
    public int totalSize = 0;

    public Directory(string name){
        this.name = name;
    }
}

class File{
    public string name = string.Empty;
    public int size = 0;

    public File(string name, int size){
        this.name = name;
        this.size = size;
    }
}