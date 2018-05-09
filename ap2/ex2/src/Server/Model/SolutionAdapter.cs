using MazeLib;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Adapter for the Solution class
    /// </summary>
    public class SolutionAdapter
    {
        /// <summary>
        /// Construcrot
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <param name="solution">The solution.</param>
        public SolutionAdapter(Maze maze, Solution<Position> solution)
        {
            Name = maze.Name;
            NodesEvaluated = solution.NumOfNodesEvaluated;
            Solution = Convert(solution);
        }

        /// <summary>
        /// Converts the solution to string.
        /// </summary>
        /// <param name="solution">The solution.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Direction does not exists!</exception>
        private static string Convert(Solution<Position> solution)
        {
            State<Position> nextMove = null;
            State<Position> currentMove = null;
            string solutionString = "";
            
            // Convert every move to a different number.
            for (int i = 0; i < solution.SolutionValue.Count - 1; i++)
            {
                nextMove = solution.SolutionValue[i + 1];
                currentMove = solution.SolutionValue[i];

                int rowsDirection = currentMove.StateValue.Row - nextMove.StateValue.Row;
                int colsDirection = currentMove.StateValue.Col - nextMove.StateValue.Col;

                // Left
                if ((rowsDirection == 0) && (colsDirection > 0))
                {
                    solutionString += "0";
                }
                // Right
                else if ((rowsDirection == 0) && (colsDirection < 0))
                {
                    solutionString += "1";
                }
                // Up
                else if ((rowsDirection > 0) && (colsDirection == 0))
                {
                    solutionString += "2";
                }
                // Down
                else if ((rowsDirection < 0) && (colsDirection == 0))
                {
                    solutionString += "3";
                }
                else
                {
                    throw new Exception("Direction does not exists!");
                }
            }

            return solutionString;
        }

        /// <summary>
        /// Returns the JSON representation.
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            JObject solutionObj = new JObject();
            solutionObj["Name"] = Name;
            solutionObj["Solution"] = Solution;
            solutionObj["NodesEvaluated"] = NodesEvaluated;

            return solutionObj.ToString();
        }

        /*===================================================================*/
        /*                            Properties                             */
        /*===================================================================*/
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get;
        }

        /// <summary>
        /// Gets the solution.
        /// </summary>
        /// <value>
        /// The solution.
        /// </value>
        public string Solution
        {
            get;
        }

        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <value>
        /// The number of nodes evaluated.
        /// </value>
        public int NodesEvaluated
        {
            get;
        }
    }
}
