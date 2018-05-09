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
using System.Collections.Concurrent;

namespace MazeModel
{
    ///<summary>
    /// class for managing maze creation, solving and playing. </summary>
    class Model : IModel
    {
        /*===================================================================*/
        /*                            Data Members                           */
        /*===================================================================*/
        private ConcurrentDictionary<string, Maze> m_nameToMaze;

        private IMazeGenerator m_generator;

        private ConcurrentDictionary<Maze, SolutionAdapter> m_mazeToSolution;

        private ConcurrentDictionary<string, MultiPlayer> m_nameToGame;

        private ConcurrentDictionary<string, MultiPlayer> m_playerToGame;

        /*===================================================================*/
        /*                               Methods                             */
        /*===================================================================*/
        ///<summary>
        /// the model's Constructor. </summary>
        public Model()
        {
            m_generator = new DFSMazeGenerator();
            m_nameToMaze = new ConcurrentDictionary<string, Maze>();
            m_mazeToSolution = new ConcurrentDictionary<Maze, SolutionAdapter>();
            m_nameToGame = new ConcurrentDictionary<string, MultiPlayer>();
            m_playerToGame = new ConcurrentDictionary<string, MultiPlayer>();
        }

        ///<summary>
        /// A method for creating mazes. </summary>
        ///<param name ="name"> the name of the maze </param>
        ///<param name ="numberOfRows"> rows' number for maze </param>
        ///<param name ="numberOfColumns"> columns' number for maze </param>
        public Maze GenerateProblem(string name, int numberOfRows, int numberOfColumns)
        {
            // Generate a new maze.
            Maze maze = m_generator.Generate(numberOfRows, numberOfColumns);
            maze.Name = name;
            m_nameToMaze[name] = maze;

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
            Maze forTryRemove;
            m_nameToMaze.TryRemove(name, out forTryRemove);
            // DFS
            if (algorithm == 1)
            {
                ISearcher<Position> dfs = new DFSSearcher<Position>();
                Solution<Position> dfsSolution = dfs.Search(mazeAdapter);
                solution = new SolutionAdapter(maze, dfsSolution);
                m_mazeToSolution[maze] =  solution;

                return solution;
            }

            // BFS
            else if (algorithm == 0)
            {
                ISearcher<Position> bfs = new BestFirstSearch<Position>();
                Solution<Position> bfsSolution = bfs.Search(mazeAdapter);
                solution = new SolutionAdapter(maze, bfsSolution);
                m_mazeToSolution[maze] = solution;

                return solution;
            }

            throw new Exception("Algorithm: " + algorithm + " isn't legal number of search algorithm.");
        }

        ///<summary>
        /// getter for the mazes' list. </summary>
        public List<string> GetProblems()
        {
            List<string> myMazes = new List<string>();

            foreach (string name in m_nameToGame.Keys)
            {
                myMazes.Add(name);
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
        public void StartGame(string requester, string name, int numberOfRows, int numberOfColumns)
        {
            Maze maze = m_generator.Generate(numberOfRows, numberOfColumns);
            MultiPlayer game = new MultiPlayer(maze, new Player(requester, maze.InitialPos));
            m_nameToGame[name] = game;
            m_playerToGame[requester] = game;
        }

        /// <summary>
        /// A method for joining a multiplayer game.
        /// </summary>
        /// <param name="requester">the host player</param>
        /// <param name="name">the name of the maze</param>
        /// <returns>A maze</returns>
        /// <exception cref="System.Exception">Game " + name + " wasn't found!</exception>
        public Maze JoinGame(string requester, string name)
        {
            if (!m_nameToGame.ContainsKey(name))
            {
                throw new Exception("Game " + name + " wasn't found!");
            }

            m_nameToGame[name].addOpponent(requester);
            m_playerToGame[requester] =  m_nameToGame[name];
            MultiPlayer multiPlayer = m_nameToGame[name];
            MultiPlayer forTryRemove;
            m_nameToGame.TryRemove(name, out forTryRemove);
            return multiPlayer.Maze;
        }

        ///<summary>
        /// getter of multiplayer game. </summary>
        ///<param name ="requester"> the player </param>
        public MultiPlayer getGame(string requester)
        {
            if (!m_playerToGame.ContainsKey(requester))
            {
                throw new Exception("This player doesn't have a game");
            }

            return m_playerToGame[requester];
        }

        public string GetOpponenetId(string playerId)
        {
            return m_playerToGame[playerId].OpponentId(playerId);
        }

        ///<summary>
        /// A method for ending a multiplayer game. </summary>
        ///<param name ="requester"> the player </param>
        ///<param name ="name"> the name of the maze </param>
        public string FinishGame(string requester, string name)
        {
            if (!m_nameToGame.ContainsKey(name))
            {
                throw new Exception("Game " + name + " wasn't found!");
            }

            Maze game = m_nameToGame[name].Maze;
            Player opponent = m_nameToGame[name].Opponent(requester);
            MultiPlayer m;
            m_playerToGame.TryRemove(requester,out m);
            m_playerToGame.TryRemove(m_nameToGame[name].Opponent(requester).ClientId, out m);
            m_nameToGame.TryRemove(name, out m);
            return opponent.ClientId;
        }
    }
}