using System;
using System.IO;
using System.Net;
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
    /// <summary>
    /// A class for the different commands 
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public abstract class Command : ICommand
    {
        /*===================================================================*/
        /*                            Data Members                           */
        /*===================================================================*/
        protected IModel m_model;

        /*===================================================================*/
        /*                               Methods                             */
        /*===================================================================*/

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">The model.</param>
        public Command(IModel model)
        {
            m_model = model;
        }

        /// <summary>
        /// Executes the specified requester.
        /// </summary>
        /// <param name="requester">The requester.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public abstract CommandInfo Execute(TcpClient requester, string[] args);
    }
}