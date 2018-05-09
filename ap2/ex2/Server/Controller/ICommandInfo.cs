using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// An interface for CommandInfo
    /// </summary>
    public interface ICommandInfo
    {
        string Result
        {
            get;
        }

        bool MultiPlayerMode
        {
            get;
        }

        TcpClient Recipient
        {
            get;
        }
    }
}
