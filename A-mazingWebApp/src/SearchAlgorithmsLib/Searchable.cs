using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    //<summary> class for Searchable problems </summary>
    public abstract class Searchable<T> : ISearchable<T>
    {
        /*===================================================================*/
        /*                               Methods                             */
        /*===================================================================*/
        //<summary> Constructor </summary>
        public Searchable() {}

        //<summary> getter </summary>
        public virtual State<T> getInitialState()
        {
            throw new NotImplementedException();
        }

        //<summary> getter </summary>
        public virtual State<T> getGoalState()
        {
            throw new NotImplementedException();
        }

        //<summary> getter of all the states that can follow a given state </summary>
        public List<State<T>> getAllPossibleStates(State<T> s)
        {
            throw new NotImplementedException();
        }
    }
}
