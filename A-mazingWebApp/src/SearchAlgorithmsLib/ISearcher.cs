using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    //<summary> interface for search algorithms </summary>
    public interface ISearcher<T>
    {
        //<summary> The search method </summary>
        Solution<T> Search(ISearchable<T> searchable);

        //<summary> Get how many nodes were evaluated by the algorithm </summary>
        int getNumberOfNodesEvaluated();
    }
}
