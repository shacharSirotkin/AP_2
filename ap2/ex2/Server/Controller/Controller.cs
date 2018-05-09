using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// The controller
    /// </summary>
    /// <seealso cref="Server.IController" />
    public class Controller : IController
    {
        public static char SEPERATOR = ' ';

        /*===================================================================*/
        /*                            Data Members                           */
        /*===================================================================*/
        private Dictionary<string, ICommand> m_commandsDictionary;

        /*===================================================================*/
        /*                               Methods                             */
        /*===================================================================*/

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">The model.</param>
        public Controller(IModel model)
        {
            m_commandsDictionary = new Dictionary<string, ICommand>();

            // Add the supported commands to the dictionary.
            m_commandsDictionary.Add("generate", new GenerateCommand(model));
            m_commandsDictionary.Add("solve", new SolveCommand(model));
            m_commandsDictionary.Add("start", new StartCommand(model));
            m_commandsDictionary.Add("list", new ListCommand(model));
            m_commandsDictionary.Add("join", new JoinCommand(model));
            m_commandsDictionary.Add("play", new PlayCommand(model));
            m_commandsDictionary.Add("close", new CloseCommand(model));
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="requester">The requester.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Command: " + commandArgs[0] + " is not part of the maze command's set.</exception>
        public CommandInfo ExecuteCommand(TcpClient requester, string command)
        {
            // Split the command into arguments
            string[] commandArgs = command.Split(SEPERATOR);

            string theCommand = commandArgs[0];

            // Execute the command.
            if (m_commandsDictionary.ContainsKey(theCommand))
            {
                return m_commandsDictionary[theCommand].Execute(requester, commandArgs);
            }

            throw new Exception("Command: " + theCommand + " is not part of the maze command's set.");
        }
    }
}