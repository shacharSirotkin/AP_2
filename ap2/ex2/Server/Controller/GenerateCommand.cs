using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;

namespace Server
{
    ///<summary>
    /// class for generating maze command. </summary>
    class GenerateCommand : Command
    {
        /*===================================================================*/
        /*                               Methods                             */
        /*===================================================================*/
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">The model which executes the command.</param>
        public GenerateCommand(IModel model) : base(model) { }

        /// <summary>
        /// The command execution.
        /// </summary>
        /// <param name="requester">The client which gave the command.</param>
        /// <param name="args">The arguments - maze's name, numbers of rows and columns.</param>
        /// <returns></returns>
        public override CommandInfo Execute(TcpClient requester, string[] args)
        {
            string name = args[1];
            int numberOfRows = int.Parse(args[2]);
            int numberOfColumns = int.Parse(args[3]);

            Maze maze = m_model.GenerateProblem(name, numberOfRows, numberOfColumns);

            return new CommandInfo(maze.ToJSON(), false, requester);
        }
    }
}
