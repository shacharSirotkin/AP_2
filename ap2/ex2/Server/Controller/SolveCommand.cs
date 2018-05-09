using MazeLib;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace Server
{
    ///<summary>
    /// a class for solve command. </summary>
    public class SolveCommand : Command
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">The model which executes the command.</param>
        public SolveCommand(IModel model) : base(model)
        {
        }

        ///<summary>
        /// the command execution. </summary>
        ///<param name ="requester"> the client which gave the command </param>
        ///<param name ="args"> arguments for solve -name of the maze, and the algorithm </param>
        public override CommandInfo Execute(TcpClient requester, string[] args)
        {
            string name = args[1];
            int algorithm = int.Parse(args[2]);

            SolutionAdapter solution = m_model.SolveProblem(name, algorithm);

            return new CommandInfo(solution.ToJSON(), false, requester);
        }
    }
}