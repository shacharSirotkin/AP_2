using System.Net.Sockets;
using System.Text;

namespace Server
{
    /// <summary>
    /// The close command.
    /// </summary>
    /// <seealso cref="Server.Command" />
    public class CloseCommand : Command
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">The model.</param>
        public CloseCommand(IModel model) : base(model) { }

        /// <summary>
        /// Executes the close command.
        /// </summary>
        /// <param name="requester">The requester.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public override CommandInfo Execute(TcpClient requester, string[] args)
        {
            string name = args[1];
            TcpClient opponent = m_model.FinishGame(requester, name);

            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("game '" + name + "' closed.");

            return new CommandInfo(jsonString.ToString(), true, opponent);
        }
    }
}