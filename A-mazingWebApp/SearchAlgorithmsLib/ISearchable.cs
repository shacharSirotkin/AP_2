using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    //<summary> an interface for searchable problems </summary>
    public interface ISearchable<T>
    {
        //<summary> Getter </summary>
        State<T> getInitialState();

        //<summary> Getter </summary>
        State<T> getGoalState();

        //<summary> Getter </summary>
        List<State<T>> getAllPossibleStates(State<T> state);
    }
}
