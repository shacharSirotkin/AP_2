using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    //<summary>
    // class for States of a search problem. </summary>
    public class State<T>
    {
        /*===================================================================*/
        /*                            Data Members                           */
        /*===================================================================*/
        // The state
        private T m_state;

        // Cost to reach this state (set by a setter)
        private double m_cost;

        // The state we came from to this state (setter)
        private State<T> m_cameFrom;

        /*===================================================================*/
        /*                               Methods                             */
        /*===================================================================*/
        //<summary>
        // Constructor. </summary>
        //<param name = state> the state itself </param>
        //<param name = cameFrom> the parent state </param>
        //<param name = cost> the cost to get to this state </param>
        private State(T state, State<T> cameFrom, double cost)
        {
            m_state = state;
            m_cameFrom = cameFrom;
            m_cost = cost;
        }

        //<summary> comparing method </summary>
        //<param name = other> the other state to which we compare </param>
        public override bool Equals(object other)
        {
            State<T> otherState = (State<T>)other;
            return m_state.Equals(otherState.m_state);
        }

        //<summary> class which compares costs </summary>
        private class CompareByCost : IComparer<State<T>>
        {
            public int Compare(State<T> state1, State<T> state2)
            {
                if (state1.Cost > state2.Cost)
                    return 1;
                if (state1.Cost < state2.Cost)
                    return -1;
                else
                    return 0;
            }
        }

        //<summary> returns a comparer </summary>
        public static IComparer<State<T>> compareByCost()
        {
            return new CompareByCost();
        }

        /*===================================================================*/
        /*                            Properties                             */
        /*===================================================================*/
        public T StateValue
        {
            get { return m_state; }
        }

        public double Cost
        {
            get { return m_cost; }
            set { m_cost = value; }
        }

        public State<T> ComeFrome
        {
            get { return m_cameFrom; }

            set { m_cameFrom = value; }
        }

        /*===================================================================*/
        /*                           Inner Class                             */
        /*===================================================================*/
        //<summary> class of StatePools </summary>
        public class StatePool
        {
            /*===================================================================*/
            /*                            Data Members                           */
            /*===================================================================*/
            private static Dictionary<T, State<T>> statePool = new Dictionary<T, State<T>>();

            /*===================================================================*/
            /*                               Methods                             */
            /*===================================================================*/
            //<summary> getter for states - from statepool </summary>
            //<param name = state> the state itself </param>
            //<param name = cameFrom> the parent state </param>
            //<param name = cost> the cost to get to this state </param>
            public static State<T> getState(T state, State<T> cameFrom, double cost)
            {
                // If the state is in the pool, return it.
                if (statePool.ContainsKey(state))
                {
                    if (cost < statePool[state].Cost)
                    {
                        statePool[state].Cost = cost;
                        statePool[state].ComeFrome = cameFrom;
                    }
                    
                    return statePool[state];
                }

                // Else, call the constructor.
                State<T> newState = new State<T>(state, cameFrom, cost);
                statePool.Add(state, newState);

                return newState;
            }

            public static void ResetPool()
            {
                statePool = new Dictionary<T, State<T>>();
            }
        }
    }
}
