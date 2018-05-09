using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    ///<summary>
    /// An interface for controllers </summary>
    public interface IController
    {
        /// <summary>
        /// Execute the given command if the command is part of the known commands set.
        /// </summary>
        /// <param name="requester">The requester.</param>
        /// <param name="command">The command.</param>
        /// <returns>The command result in JSON format.</returns>
        CommandInfo ExecuteCommand(TcpClient requester, string command);
    }
}
