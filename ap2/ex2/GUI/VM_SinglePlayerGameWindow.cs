using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace GUI
{
    class VM_SinglePlayerGameWindow
    {
        SinglePlayerModel m_singlePlayerModel;
        Maze m_maze;

        public VM_SinglePlayerGameWindow(Maze maze)
        {
            m_singlePlayerModel = new SinglePlayerModel();
            m_maze = maze;
        }

        public String SolveMaze()
        {
            // Set the solve algorithm.
            int defaultSolver;
            if (Properties.Settings.Default.DefaultAlgorithm == "BFS")
            {
                defaultSolver = 0;
            }
            else
            {
                defaultSolver = 1;
            }

            //connect the server for handling the command.
            //////////////////////////////////////////////////////
            /**/ m_singlePlayerModel = new SinglePlayerModel(); // TODO maybe need to remove.
            //////////////////////////////////////////////////////
            m_singlePlayerModel.Connect();
            //create generation command from the details.
            string solveCommand = "solve " + m_maze.Name + " " + defaultSolver;
            //send the solveCommand to the server and return the maze solution from the server.
            String solutionJSON = m_singlePlayerModel.SendAndReceive(solveCommand);

            dynamic solution = JsonConvert.DeserializeObject<dynamic>(solutionJSON);
            var solutionString = solution.Solution;

            return solutionString;
        }
    }
}
