using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Server
{
    /// <summary> class for play command </summary>
    public class PlayCommand : Command
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">The model which executes the command.</param>
        public PlayCommand(IModel model) : base(model)
        {
        }
        /// <summary>
        /// The command execution.
        /// </summary>
        /// <param name="requester">The client which gave the command.</param>
        /// <param name="args">Arguments for solve -name of the maze, and the algorithm.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">there's no second player</exception>
        public override CommandInfo Execute(TcpClient requester, string[] args)
        {
            MultiPlayer game = m_model.getGame(requester);

            if (game.Opponent(requester).Id == null)
            {
                throw new Exception("there's no second player");
            }
            // Build the JSON representation to return.
            JObject solutionObj = new JObject();
            solutionObj["Name"] = game.Maze.Name;
            solutionObj["direction"] = args[1];

            return new CommandInfo(solutionObj.ToString(), true, game.Opponent(requester).Id);
        }
    }
}
