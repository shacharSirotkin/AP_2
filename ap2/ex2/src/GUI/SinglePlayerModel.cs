using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace GUI
{
    class SinglePlayerModel:Model
    {
        private TcpClient m_client;
        public SinglePlayerModel()
        {
            m_client = new TcpClient();
        }
        public void Connect()
        {
            //create connection between the model and the server according to the app.config file
            int port = Convert.ToInt32(Properties.Settings.Default.Port);
            IPAddress ip = IPAddress.Parse(Properties.Settings.Default.IP);
            IPEndPoint endP = new IPEndPoint(ip, port);
            /////==========================/////
            /* */ m_client = new TcpClient(); // TODO maybe need to remove.
            /////==========================/////
            m_client.Connect(endP);
        }

        /// <summary>
        /// Sends the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public string SendAndReceive(string command)
        {
            string message;
            using (NetworkStream stream = m_client.GetStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                // send message to the server.
                writer.Write(command);
                Console.WriteLine("The command is: " + command);
                message = reader.ReadString();
                Console.WriteLine("Server response is: " + message);
            }
            return message;
        }
    }
}
