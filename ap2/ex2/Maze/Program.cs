using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;

namespace SolveMaze
{
    /// <summary>
    /// The main program of SolveMaze.
    /// </summary>
    class Program
    {
        private static void PrintSolution(Solution<Position> solution)
        {
            foreach (State<Position> s in solution.SolutionValue)
            {
                Console.Out.Write("(" + s.StateValue.Row + ", " + s.StateValue.Col + ")\t\t");
                if (s.ComeFrome != null)
                {
                    Console.Out.Write("(" + s.ComeFrome.StateValue.Row + ", " + s.ComeFrome.StateValue.Col + ")\t\t");
                }
                Console.Out.WriteLine(s.Cost);
            }
        }

        /// <summary>
        /// Compares the two solvers.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        public static void CompareSolvers(int row, int col)
        {
            // Generate a maze.
            IMazeGenerator generator = new DFSMazeGenerator();
            Maze maze = generator.Generate(row, col);

            // Print the maze.
            Console.Out.WriteLine(maze.ToString());

            ISearchable<Position> seacrchableMaze = new MazeToSearchableAdapter<Position>(maze);

            // BFS.
            ISearcher<Position> bfs = new BestFirstSearch<Position>();
            Solution<Position> bfsSolution = bfs.search(seacrchableMaze);

            // DFS.
            ISearcher<Position> dfs = new DFSSearcher<Position>();
            Solution<Position> dfsSolution = dfs.search(seacrchableMaze);
            
            // Print the number of nodes evaluated.
            Console.Out.WriteLine("Number of states evaluated by BFS:\t" + bfs.getNumberOfNodesEvaluated());
            Console.Out.WriteLine("Number of states evaluated by DFS:\t" + dfs.getNumberOfNodesEvaluated());
        }

        /// <summary>
        /// Compare between two solvers.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            CompareSolvers(3, 3);
        }
    }
}
