using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    //<summary> class of solutions </summary>
    public class Solution<T>
    {
        /*===================================================================*/
        /*                            Data Members                           */
        /*===================================================================*/
        private List<State<T>> m_solution;
        private int m_numOfNodesEvaluated;

        /*===================================================================*/
        /*                               Methods                             */
        /*===================================================================*/
        //<summary> constructor </summary>
        //<param name = solutionStates> list of states which compose the solution </param>
        //<param name = nodesEvaluated> number of nodes evaluated </param>
        public Solution(List<State<T>> solutionStates, int nodesEvaluated)
        {
            m_solution = solutionStates;
            m_numOfNodesEvaluated = nodesEvaluated;
        }

        //<summary> writes the number of nodes evaluated </summary>
        public void toString()
        {
            Console.WriteLine("Number of states evaluated: " + m_numOfNodesEvaluated.ToString());
        }

        /*===================================================================*/
        /*                            Properties                             */
        /*===================================================================*/
        public List<State<T>> SolutionValue
        {
            get { return m_solution; }
        }

        public int NumOfNodesEvaluated
        {
            get { return m_numOfNodesEvaluated;  }
        }
    }
}