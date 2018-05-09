using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    //<summary> class for DFS algorithm </summary>
    public class DFSSearcher<T> : Searcher<T>
    {
        /*===================================================================*/
        /*                            Data Members                           */
        /*===================================================================*/
        private Stack<State<T>> m_stack = new Stack<State<T>>();

        /*===================================================================*/
        /*                               Methods                             */
        /*===================================================================*/
        //<summary> the search method </summary>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            Reset();
            // Initialization of the "SEEN" list - the state that already pushed.
            HashSet<T> seenList = new HashSet<T>();
            m_stack.Push(searchable.getInitialState());

            /*
             * Search for the goal state - in each iteration, get all neighoburs
             * in the stack if we haven't reached the goal, keep going.
             */
            while (m_stack.Count > 0)
            {
                State<T> currentState = m_stack.Pop();
                m_evaluatedNodes++;
                seenList.Add(currentState.StateValue);

                if (currentState.Equals(searchable.getGoalState()))
                {
                    return BackTrace(searchable.getInitialState(), currentState);
                }

                List<State<T>> successors = searchable.getAllPossibleStates(currentState);
                foreach (State<T> s in successors)
                {
                    if (!m_stack.Contains(s) && !seenList.Contains(s.StateValue))
                    {
                        m_stack.Push(s);
                    }
                }
            }

            // Got out from the loop without solution - throw exception.
            throw new NoSolutionException();
        }

        private void Reset()
        {
            State<T>.StatePool.ResetPool();
        }
    }
}
