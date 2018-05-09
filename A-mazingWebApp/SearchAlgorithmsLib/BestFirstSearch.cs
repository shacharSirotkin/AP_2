using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    //<summary> class for best-first-search algorithm </summary>
    public class BestFirstSearch<T> : PQSearcher<T>
    {
        /*===================================================================*/
        /*                            Data Members                           */
        /*===================================================================*/
        /*===================================================================*/
        /*                               Methods                             */
        /*===================================================================*/
        //<summary> Searcher's abstract method overriding </summary>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            Reset();
            // Add the initial state to the "OPEN" list (the priority queue).
            pushPriorityQueue(searchable.getInitialState());

            // Initialization of the "CLOSE" list.
            HashSet<T> closeList = new HashSet<T>();

            while (PriorityQueueSize > 0)
            {
                // Get the best state from the "OPEN" list.
                State<T> currentState = popPriorityQueue();
                closeList.Add(currentState.StateValue);

                if (currentState.Equals(searchable.getGoalState()))
                {
                    return BackTrace(searchable.getInitialState(), currentState);
                }

                List<State<T>> successors = searchable.getAllPossibleStates(currentState);
                foreach (State<T> s in successors)
                {
                    if (!closeList.Contains(s.StateValue))
                    {
                        // If s is not in the "OPEN" list - add it.
                        if (!m_priorityQueue.Contains(s))
                        {
                            pushPriorityQueue(s);
                        }
                        // The successors already in the "OPEN" list - update its cost if needed.
                        else
                        {
                            updatePriority(s);
                        }
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
