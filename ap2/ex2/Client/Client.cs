using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Client
{
    /// <summary>
    /// The client class.
    /// </summary>
    class Client
    {
        TcpClient m_client;
        private IPAddress m_ip;
        private int m_port;
        private IPEndPoint m_endPoint;
        private const string CLOSE_COMMAND = "";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        public Client(IPAddress ip, int port)
        {
            m_client = new TcpClient();
            m_ip = ip;
            m_port = port;
            m_endPoint = new IPEndPoint(m_ip, m_port);
        }

        /// <summary>
        /// Starts the communication.
        /// </summary>
        public void Start()
        {
            // Connect to the server.
            while (!m_client.Connected)
            {
                try
                {
                    m_client.Connect(m_endPoint);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Connection failed. Try again...");
                }
            }

            // Set the connection info.
            Console.WriteLine("You are connected");
            using (NetworkStream stream = m_client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                string message = "initial message";

                // Write to the server.
                Task write = new Task(() =>
                {
                    while (message != CLOSE_COMMAND)
                    {
                        string s = Console.ReadLine();
                        writer.Write(s);
                    }
                });
                write.Start();

                // Close command - close connection
                while (message != CLOSE_COMMAND)
                {
                    message = reader.ReadString();
                    Console.WriteLine(message);
                }
                write.Wait();
                m_client.Close();
            }
        }

        /// <summary>
        /// Stops the communication.
        /// </summary>
        public void Stop()
        {
            m_client.Close();
        }
    }
}