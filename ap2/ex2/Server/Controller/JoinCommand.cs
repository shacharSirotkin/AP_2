using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    ///<summary>
    /// A class for join command. </summary>
    public class JoinCommand : Command
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">The model which executes the command.</param>
        public JoinCommand(IModel model) : base(model)
        {
        }
        /// <summary>
        /// The command execution.
        /// </summary>
        /// <param name="requester">The client which gave the command.</param>
        /// <param name="args">The arguments for join command - the name of the maze to join.</param>
        /// <returns></returns>
        public override CommandInfo Execute(TcpClient requester, string[] args)
        {
            string name = args[1];

            Maze game = m_model.JoinGame(requester, name);
            
            return new CommandInfo(game.ToJSON(), true, requester);
        }
    }
}
