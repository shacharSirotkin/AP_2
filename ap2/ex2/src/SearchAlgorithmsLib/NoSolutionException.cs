using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    //<summary> exception for no solution case </summary>
    public class NoSolutionException : Exception
    {
        public NoSolutionException(): base("There is no solution to this searchable problem") {}
    }
}
