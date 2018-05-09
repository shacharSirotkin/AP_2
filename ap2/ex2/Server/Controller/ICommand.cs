using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace Server
{
    ///<summary>
    /// An interface for commands. </summary>
    public interface ICommand
    {
        CommandInfo Execute(TcpClient requester, string[] args);
    }
}