using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using SolveMaze;

namespace Server
{
    ///<summary>
    /// class for managing maze creation, solving and playing. </summary>
    class Model : IModel
    {
        /*===================================================================*/
        /*                            Data Members                           */
        /*===================================================================*/
        private Dictionary<string, Maze> m_nameToMaze;

        private HashSet<Maze> m_multiGames;

        private Dictionary<Maze, SolutionAdapter> m_mazeToSolution;

        private Dictionary<string, MultiPlayer> m_nameToGame;

        private Dictionary<TcpClient, MultiPlayer> m_playerToGame;

        /*===================================================================*/
        /*                               Methods                             */
        /*===================================================================*/
        ///<summary>
        /// the model's Constructor. </summary>
        public Model()
        {
            m_multiGames = new HashSet<Maze>();
            m_nameToMaze = new Dictionary<string, Maze>();
            m_mazeToSolution = new Dictionary<Maze, SolutionAdapter>();
            m_nameToGame = new Dictionary<string, MultiPlayer>();
            m_playerToGame = new Dictionary<TcpClient, MultiPlayer>();
        }

        ///<summary>
        /// A method for creating mazes. </summary>
        ///<param name ="name"> the name of the maze </param>
        ///<param name ="numberOfRows"> rows' number for maze </param>
        ///<param name ="numberOfColumns"> columns' number for maze </param>
        public Maze GenerateProblem(string name, int numberOfRows, int numberOfColumns)
        {
            // Generate a new maze.
            IMazeGenerator generator = new DFSMazeGenerator();
            Maze maze = generator.Generate(numberOfRows, numberOfColumns);
            maze.Name = name;
            m_nameToMaze.Add(name, maze);

            return maze;
        }

        ///<summary>
        /// A method for solving mazes. </summary>
        ///<param name = "name"> the name of the maze </param>
        ///<param name = "algorithm"> the algorithm with which we solve </param>
        public SolutionAdapter SolveProblem(string name, int algorithm)
        {
            if (!m_nameToMaze.ContainsKey(name))
            {
                throw new Exception("Unavailable Maze");
            }

            Maze maze = m_nameToMaze[name];
            
            if (m_mazeToSolution.ContainsKey(maze))
            {
                return m_mazeToSolution[maze];
            }

            MazeToSearchableAdapter<Position> mazeAdapter = new MazeToSearchableAdapter<Position>(m_nameToMaze[name]);
            SolutionAdapter solution = null;

            // DFS
            if (algorithm == 1)
            {
                ISearcher<Position> dfs = new DFSSearcher<Position>();
                Solution<Position> dfsSolution = dfs.Search(mazeAdapter);
                solution = new SolutionAdapter(maze, dfsSolution);
                m_mazeToSolution.Add(maze, solution);

                return solution;
            }

            // BFS
            else if (algorithm == 0)
            {
                ISearcher<Position> bfs = new BestFirstSearch<Position>();
                Solution<Position> bfsSolution = bfs.Search(mazeAdapter);
                solution = new SolutionAdapter(maze, bfsSolution);
                m_mazeToSolution.Add(maze, solution);

                return solution;
            }

            throw new Exception("Algorithm: " + algorithm + " isn't legal number of search algorithm.");
        }

        ///<summary>
        /// getter for the mazes' list. </summary>
        public List<Maze> GetProblems()
        {
            List<Maze> myMazes = new List<Maze>();

            foreach (Maze m in m_nameToMaze.Values)
            {
                if (m_multiGames.Contains(m))
                {
                    myMazes.Add(m);
                }
            }

            return myMazes;
        }

        /// <summary>
        /// Multiplayer-games starter.
        /// </summary>
        /// <param name="requester">the host player</param>
        /// <param name="name">the name of the maze</param>
        /// <param name="numberOfRows">rows' number for maze</param>
        /// <param name="numberOfColumns">columns' number for maze</param>
        /// <returns>A game</returns>
        public MultiPlayer StartGame(TcpClient requester, string name, int numberOfRows, int numberOfColumns)
        {
            Maze maze = GenerateProblem(name, numberOfRows, numberOfColumns);
            m_multiGames.Add(maze);
            MultiPlayer game = new MultiPlayer(maze, new Player(requester, maze.InitialPos));
            m_nameToGame.Add(name, game);
            m_playerToGame.Add(requester, game);

            return game;
        }

        /// <summary>
        /// A method for joining a multiplayer game.
        /// </summary>
        /// <param name="requester">the host player</param>
        /// <param name="name">the name of the maze</param>
        /// <returns>A maze</returns>
        /// <exception cref="System.Exception">Game " + name + " wasn't found!</exception>
        public Maze JoinGame(TcpClient requester, string name)
        {
            if (!m_nameToGame.ContainsKey(name))
            {
                throw new Exception("Game " + name + " wasn't found!");
            }

            m_nameToGame[name].addOpponent(requester);
            m_playerToGame.Add(requester, m_nameToGame[name]);

            return m_nameToMaze[name];
        }

        ///<summary>
        /// getter of multiplayer game. </summary>
        ///<param name ="requester"> the player </param>
        public MultiPlayer getGame(TcpClient requester)
        {
            if (!m_playerToGame.ContainsKey(requester))
            {
                throw new Exception("This player doesn't have a game");
            }

            return m_playerToGame[requester];
        }

        ///<summary>
        /// A method for ending a multiplayer game. </summary>
        ///<param name ="requester"> the player </param>
        ///<param name ="name"> the name of the maze </param>
        public TcpClient FinishGame(TcpClient requester, string name)
        {
            if (!m_nameToGame.ContainsKey(name))
            {
                throw new Exception("Game " + name + " wasn't found!");
            }

            Maze game = m_nameToGame[name].Maze;
            Player opponent = m_nameToGame[name].Opponent(requester);
            m_playerToGame.Remove(requester);
            m_playerToGame.Remove(m_nameToGame[name].Opponent(requester).Id);
            m_nameToGame.Remove(name);
            m_mazeToSolution.Remove(game);

            return opponent.Id;
        }
    }
}
