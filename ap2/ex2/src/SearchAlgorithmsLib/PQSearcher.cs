using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    //<summary> class for priority-queue based algorithms </summary>
    public abstract class PQSearcher<T> : Searcher<T>
    {
        public const int maxStatesNumber = 1024;

        /*===================================================================*/
        /*                            Data Members                           */
        /*===================================================================*/

        // The priority queue
        protected PriorityQueue<State<T>> m_priorityQueue;

        /*===================================================================*/
        /*                               Methods                             */
        /*===================================================================*/

        //<summary> Constructor </summary>
        public PQSearcher()
        {
            m_priorityQueue = new PriorityQueue<State<T>>(maxStatesNumber, State<T>.compareByCost());
        }

        //<summary> Pop </summary>
        protected State<T> popPriorityQueue()
        {
            m_evaluatedNodes++;
            return m_priorityQueue.pop();
        }

        //<summary> Push </summary>
        protected void pushPriorityQueue(State<T> state)
        {
            m_priorityQueue.push(state);
        }

        //<summary> update priority </summary>
        protected void updatePriority(State<T> state)
        {
            m_priorityQueue.updatePriority(state);
        }
           
        // Property of priorityQueue
        public int PriorityQueueSize
        {
            get { return m_priorityQueue.Count; }
        }
    }
}
