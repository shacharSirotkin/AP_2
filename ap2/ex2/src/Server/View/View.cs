using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server
{
    /// <summary>
    /// The view class.
    /// </summary>
    /// <seealso cref="Server.IView" />
    public class View : IView
    {
        /*===================================================================*/
        /*                            Data Members                           */
        /*===================================================================*/
        private const string CLOSE_COMMAND = "";
        private int m_port;
        private TcpListener m_listener;
        private IController m_controller;
        private List<TcpClient> m_clientsList;
        private IPEndPoint m_endPoint;

        /*===================================================================*/
        /*                               Methods                             */
        /*===================================================================*/

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="controller">The controller.</param>
        public View(int port, IController controller)
        {
            m_port = port;
            m_controller = controller;
            m_clientsList = new List<TcpClient>();
            m_endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), m_port);
        }

        /// <summary>
        /// Starts the communication.
        /// </summary>
        public void Start()
        {
            // Listen to the end point.
            m_listener = new TcpListener(m_endPoint);

            // Start listening.
            m_listener.Start();
            Console.WriteLine("Wait to clients.");

            // Accept new clients and add them to the clients list.
            while (true)
            {
                try
                {
                    // Accept new clients
                    TcpClient client = m_listener.AcceptTcpClient();
                    Console.WriteLine("Client accepted");

                    if (client != null)
                    {
                        m_clientsList.Add(client);
                        Console.WriteLine("Got new connection");

                        // In a new task - receive from the new client.
                        Task receive = new Task(() =>
                        {
                            using (NetworkStream stream = client.GetStream())
                            using (BinaryReader reader = new BinaryReader(stream))
                            using (BinaryWriter writer = new BinaryWriter(stream))
                            {
                                TcpClient me = client;
                                while (true)
                                {
                                    CommandInfo commandResult = m_controller.ExecuteCommand(me, reader.ReadString());

                                    if (commandResult.Recipient != me)
                                    {
                                        BinaryWriter otherWriter = new BinaryWriter(commandResult.Recipient.GetStream());
                                        otherWriter.Write(commandResult.Result);
                                        continue;
                                    }

                                    writer.Write(commandResult.Result);
                                }
                            }
                        });
                        receive.Start();
                    }
                }       
                catch (SocketException)
                {
                    break;
                }               
            }
        }

        /// <summary>
        /// Stops the communication.
        /// </summary>
        public void Stop()
        {
            // Stop listening
            m_listener.Stop();

            // Close all the clients
            foreach (TcpClient client in m_clientsList)
            {
                client.Close();
            }
            Console.WriteLine("Clean exit.");
        }
    }
}