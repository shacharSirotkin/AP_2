using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    ///<summary>
    /// class for Command Information. </summary>
    public class CommandInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="commandResult">the string whic the command returns.</param>
        /// <param name="isMultiPlayerMode">boolean - tells if the command is multiplayer.</param>
        /// <param name="recipient">the client to whic we send the result.</param>
        public CommandInfo(string commandResult, bool isMultiPlayerMode, TcpClient recipient)
        {
            Result = commandResult;
            MultiPlayerMode = isMultiPlayerMode;
            Recipient = recipient;
        }

        /*===================================================================*/
        /*                            Properties                             */
        /*===================================================================*/
        ///<summary>
        /// result of the command. </summary>
        public string Result
        {
            get;
        }

        ///<summary>
        /// boolean - is it a multiplayer command. </summary>
        public bool MultiPlayerMode
        {
            get;
        }

        ///<summary>
        /// client whic receives the command. </summary>
        public TcpClient Recipient
        {
            get;
        }
    }
}
