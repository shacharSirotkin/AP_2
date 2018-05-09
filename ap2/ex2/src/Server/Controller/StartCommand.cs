using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Server
{
    ///<summary>
    /// A class for the "start" command. </summary>
    public class StartCommand : Command
    {
        /// <summary>
        /// The name of the game to be started.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
        public string MazeName { get; set; }

        ///<summary>
        /// the StartCommand Constructor. </summary>
        ///<param name ="model"> a model which executes the command </param>
        public StartCommand(IModel model) : base(model)
        {
            MazeName = "";
        }

        ///<summary>
        /// the command execution. </summary>
        ///<param name ="requester"> the client which gave the command </param>
        ///<param name ="args"> the arguments for start command (name, rows and cols) </param>
        public override CommandInfo Execute(TcpClient requester, string[] args)
        {
            string name = args[1];
            int numberOfRows = int.Parse(args[2]);
            int numberOfColumns = int.Parse(args[3]);

            MultiPlayer result = m_model.StartGame(requester, name, numberOfRows, numberOfColumns);

            MazeName = args[1];
            
            while (m_model.getGame(requester).Opponent(requester) == null)
            {
                Thread.Sleep(10);
            }

            return new CommandInfo(result.Maze.ToJSON(), true, requester);
        }
    }
}
