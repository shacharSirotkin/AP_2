using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace SolveMaze
{
    //<summary> an adapter from maze to searchable problem </summary>
    public class MazeToSearchableAdapter<MazeLibPosition> : ISearchable<Position>
    {
        /*===================================================================*/
        /*                               Constants                           */
        /*===================================================================*/
        public const int numberOfDirections = 4;

        /*===================================================================*/
        /*                            Data Members                           */
        /*===================================================================*/
        private Maze m_maze;
        private int[,] m_offset = new int[numberOfDirections, 2] { {0, -1}, {-1, 0}, {0, 1}, {1, 0} };

        /*===================================================================*/
        /*                               Methods                             */
        /*===================================================================*/
        //<summary> Constructor </summary>
        public MazeToSearchableAdapter(Maze maze)
        {
            m_maze = maze;
        }

        //<summary> getter of all the states that can follow a given state </summary>
        public List<State<Position>> getAllPossibleStates(State<Position> state)
        {
            List<State<Position>> allPossibleStates = new List<State<Position>>();
            
            int row = state.StateValue.Row;
            int col = state.StateValue.Col;

            for (int i = 0; i < numberOfDirections; i++)
            {
                if (row + m_offset[i, 0] >= 0 && row + m_offset[i, 0] < m_maze.Rows &&
                    col + m_offset[i, 1] >= 0 && col + m_offset[i, 1] < m_maze.Cols &&
                    m_maze[row + m_offset[i, 0], col + m_offset[i, 1]] == CellType.Free)
                {
                    allPossibleStates.Add(State<Position>.StatePool.getState(new Position(row + m_offset[i, 0], col + m_offset[i, 1]), state, state.Cost + 1));
                }
            }

            return allPossibleStates;
        }

        //<summary> getter </summary>
        public State<Position> getGoalState()
        {
            return State<Position>.StatePool.getState(m_maze.GoalPos, null, Int64.MaxValue);
        }

        //<summary> getter </summary>
        public State<Position> getInitialState()
        {
            return State<Position>.StatePool.getState(m_maze.InitialPos, null, 0);
        }

        public override string ToString()
        {
            return "InitialPos = " + m_maze.InitialPos + ", GoalPos = " + m_maze.GoalPos;
        }
    }
}
