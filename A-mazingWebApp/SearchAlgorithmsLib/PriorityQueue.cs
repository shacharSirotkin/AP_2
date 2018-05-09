using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    // Taken from http://stackoverflow.com/questions/102398/priority-queue-in-net
    //<summary> class for priority queue </summary>
    public class PriorityQueue<T>
    {
        /*===================================================================*/
        /*                            Data Members                           */
        /*===================================================================*/

        //<summary> Comparator for <T> type. </summary>
        IComparer<T> m_comparer;

        //<summary> The queue istelf </summary>
        T[] m_heap;

        //<summary> The queue size </summary>
        public int Count
        {
            get;
            private set;
        }

        /*===================================================================*/
        /*                               Methods                             */
        /*===================================================================*/

        //<summary> Constructor </summary>
        public PriorityQueue(): this(null) { }

        //<summary> Constructor </summary>
        public PriorityQueue(int capacity): this(capacity, null) { }

        //<summary> Constructor </summary>
        public PriorityQueue(IComparer<T> comparer): this(16, comparer) { }

        //<summary> Constructor </summary>
        public PriorityQueue(int capacity, IComparer<T> comparer)
        {
            m_comparer = (comparer == null) ? Comparer<T>.Default : comparer;
            m_heap = new T[capacity];
        }

        //<summary> method for pushing into the queue </summary>
        public void push(T value)
        {
            if (Count >= m_heap.Length)
            {
                Array.Resize(ref m_heap, Count * 2);
            }

            m_heap[Count] = value;

            ShiftUp(Count++);
        }

        //<summary> method for popping from the queue </summary>
        public T pop()
        {
            var value = top();
            m_heap[0] = m_heap[--Count];
            if (Count > 0)
            {
                ShiftDown(0);
            }

            return value;
        }

        //<summary> method that gives the top of the queue </summary>
        public T top()
        {
            if (Count > 0)
            {
                return m_heap[0];
            }
            throw new InvalidOperationException("");
        }

        //<summary> method that moves an element nearer to the top </summary>
        void ShiftUp(int n)
        {
            var v = m_heap[n];
            for (var n2 = n / 2; n > 0 && m_comparer.Compare(v, m_heap[n2]) < 0; n = n2, n2 /= 2)
            {
                m_heap[n] = m_heap[n2];
            }

            m_heap[n] = v;
        }

        //<summary> method that moves an element further from the top </summary>
        void ShiftDown(int n)
        {
            var v = m_heap[n];
            for (var n2 = n * 2; n2 < Count; n = n2, n2 *= 2)
            {
                if (n2 + 1 < Count && m_comparer.Compare(m_heap[n2 + 1], m_heap[n2]) < 0)
                {
                    n2++;
                }
                if (m_comparer.Compare(v, m_heap[n2]) <= 0)
                {
                    break;
                }
                m_heap[n] = m_heap[n2];
            }
            m_heap[n] = v;
        }

        /*===================================================================*/
        /*                              Extentions                           */
        /*===================================================================*/
        //<summary> method that checks whether the queue contains a value </summary>
        public bool Contains(T value)
        {
            return m_heap.Contains(value);
        }

        //<summary> Get the value "value" from the heap - for READ-ONLY usage!!! </summary>
        public T watch(T value)
        {
            foreach (T v in m_heap)
            {
                if (v.Equals(value))
                {
                    return v;
                }
            }

            return default(T);
        }

        //<summary> Get a value, find it in the priority queue and move it to the right position 
        //          (Maybe its priority improved). </summary>
        public void updatePriority(T value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (m_heap[i].Equals(value))
                {
                    // If v > value, replace v by value, and shift it up to the right place in the queue.
                    if (m_comparer.Compare(m_heap[i], value) > 0)
                    {
                        m_heap[i] = value;
                        ShiftUp(i);
                    }
                    break;
                }
            }
        }
    }
}