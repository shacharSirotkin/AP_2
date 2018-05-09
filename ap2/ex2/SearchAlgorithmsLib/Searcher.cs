using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    //<summary> class for Search algorithms </summary>
    public abstract class Searcher<T> : ISearcher<T>
    {
        /*===================================================================*/
        /*                            Data Members                           */
        /*===================================================================*/

        protected int m_evaluatedNodes;

        /*===================================================================*/
        /*                               Methods                             */
        /*===================================================================*/

        //<summary> Constructor </summary>
        public Searcher()
        {
            m_evaluatedNodes = 0;
        }

        //<summary> getter for number of nodes evaluated </summary>
        public int getNumberOfNodesEvaluated()
        {
            return m_evaluatedNodes;
        }

        //<summary> the search method </summary>
        public abstract Solution<T> Search(ISearchable<T> searchable);

        //<summary> Back traces through the parents calling the delegated method, 
        //          returns a list of states with n as a parent. </summary>
        protected Solution<T> BackTrace(State<T> initialState, State<T> currentState)
        {
            List<State<T>> solution = new List<State<T>>();

            if (initialState == currentState)
            {
                return new Solution<T>(solution, m_evaluatedNodes);
            }

            Stack<State<T>> solutionStack = new Stack<State<T>>();
            do
            {
                // add the current state to the solution.
                solutionStack.Push(currentState);
                currentState = currentState.ComeFrome;
            }
            while (currentState != null && !currentState.Equals(initialState));

            solutionStack.Push(initialState);

            // Reverse the solution, to get it from start point to end point.
            while (solutionStack.Count > 0)
            {
                solution.Add(solutionStack.Pop());
            }

            // Return the list
            return new Solution<T>(solution, m_evaluatedNodes);
        }
    }
}