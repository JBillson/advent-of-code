namespace day8;

class Program{
    private static List<string> _input = new List<string>();
    private static int[,] _matrix;
    static void Main(string[] args){
        _input = File.ReadAllLines("input.txt").ToList();

        // create 2d array from input
        _matrix = new int[_input.Count, _input[0].Count()];

        // populate matrix from input
        for (int i = 0; i < _input.Count; i++)
        {
            for (int j = 0; j < _input[i].Length; j++)
            {
                _matrix[i,j] = int.Parse(_input[i][j].ToString());
            }
        }

        GPT3();
    }

    // this answer was provided by GPT3 in response to the puzzle
    // it is a valid answer for the test case but not for the actual input
    private static void GPT3(){
        int visibleTrees = 0;

        // Iterate over each row of the _matrix
        for (int y = 0; y < _matrix.GetLength(1); y++)
        {
            // Keep track of the maximum tree height seen so far
            int maxHeight = -1;
            
            // Iterate over each column of the _matrix
            for (int x = 0; x < _matrix.GetLength(0); x++)
            {
                int treeHeight = _matrix[x, y];

                // If this tree is taller than the maximum height seen so far,
                // it is visible from the left/right.
                if (treeHeight > maxHeight)
                {
                    visibleTrees++;
                    maxHeight = treeHeight;
                }
            }
        }
        
        // Iterate over each column of the _matrix
        for (int x = 0; x < _matrix.GetLength(0); x++)
        {
            // Keep track of the maximum tree height seen so far
            int maxHeight = -1;
            
            // Iterate over each row of the _matrix
            for (int y = 0; y < _matrix.GetLength(1); y++)
            {
                int treeHeight = _matrix[x, y];
                
                // If this tree is taller than the maximum height seen so far,
                // it is visible from the top/bottom.
                if (treeHeight > maxHeight)
                {
                    visibleTrees++;
                    maxHeight = treeHeight;
                }
            }
        }
        
        // Print the number of visible trees
        Console.WriteLine($"Visible trees: {visibleTrees}");
    }
}