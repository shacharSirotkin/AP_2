using MazeLib;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    /// <summary>
    /// a class for list command. </summary>
    internal class ListCommand : Command
    {
        /// <summary>
        /// The model which executes the command
        /// </summary>
        /// <param name="model">The model.</param>
        public ListCommand(IModel model) : base(model)
        {
        }

        /// <summary>
        /// Executes the specified requester.
        /// </summary>
        /// <param name="requester">The requester.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public override CommandInfo Execute(TcpClient requester, string[] args)
        {
            List<string> gameNames = new List<string>();
            StringBuilder jsonString = new StringBuilder();
            List<Maze> games = m_model.GetProblems();

            // Build the list to return
            jsonString.Append("[\n");

            for (int i = 0; i <  games.Count - 1; i++)
            {
                jsonString.Append(games[i].Name + ",\n");
            }
            if (games.Count > 0)
            {
                jsonString.Append("'" + games[games.Count - 1].Name + "'\n");
            }
            jsonString.Append("]");

            return new CommandInfo(jsonString.ToString(), false, requester);
        }
    }
}