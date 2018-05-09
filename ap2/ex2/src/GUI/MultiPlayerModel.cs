using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GUI
{
    public delegate void ReactionToOpponenet(string response);
    public delegate void ErrorReaction(string exceptionMessage);
    public delegate void CloseReaction();
    public class MultiPlayerModel : Model
    {
        public event ReactionToOpponenet ReactionsToOpponenetEvent;
        public event ErrorReaction ErrorReactionsEvent;
        public event CloseReaction CloseReactionsEvent;
        private TcpClient m_client;
        private IPEndPoint m_endP;

        string buttonContent;
        public string ButtonContent
        {
            get
            { return buttonContent; }
            set
            {
                buttonContent = value;
                NotifyPropertyChanged("ButtonContent");
            }
        }

        public MultiPlayerModel()
        {
            this.m_client = new TcpClient();
            int port = Convert.ToInt32(Properties.Settings.Default.Port);
            IPAddress ip = IPAddress.Parse(Properties.Settings.Default.IP);
            m_endP = new IPEndPoint(ip, port);
        }

        public string ConnectGameInServer(string command)
        {
            string mazeJson;
            m_client.Connect(m_endP);
            try
            {
                NetworkStream stream = m_client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);
                BinaryReader reader = new BinaryReader(stream);
                writer.Write(command);
                mazeJson = reader.ReadString();
            }
            finally { }
            return mazeJson;
        }

        public List<string> AskForGamesList()
        {
            TcpClient tempConnection = new TcpClient();
            tempConnection.Connect(m_endP);
            string message;
            using (NetworkStream stream = tempConnection.GetStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                // send message to the server.
                writer.Write("list");
                message = reader.ReadString();
            }

            JArray namesJson = JArray.Parse(message);
            string[] namesString = namesJson.ToObject<string[]>();
            return namesString.OfType<string>().ToList();
        }

        public void OpenPlayReaderConnection()
        {
            bool stopReading = false;
            Task receiver = new Task(() =>
            {
                string opponentDirection;
                try
                {
                    while (!stopReading)
                    {
                        NetworkStream stream = m_client.GetStream();
                        BinaryReader reader = new BinaryReader(stream);
                        opponentDirection = reader.ReadString();
                        if (opponentDirection.Equals("close"))
                        {
                            CloseReactionsEvent();
                            stopReading = true;
                        }
                        ReactionsToOpponenetEvent(opponentDirection);
                    }
                }
                catch (Exception e)
                {
                    ErrorReactionsEvent(e.Message);
                }
                finally { }
            }
            );
            receiver.Start();
        }

        public void SendDirectionMessage(string directionCmd)
        {
            Console.WriteLine("MULTIPLAYERMODEL - SendDirectionMessage");
            try
            {
                NetworkStream stream = m_client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);
                Console.WriteLine("MULTIPLAYERMODEL - SendDirectionMessage - BEFORE WRITE");
                writer.Write(directionCmd);
                Console.WriteLine("MULTIPLAYERMODEL - SendDirectionMessage - AFTER WRITE");
                if (directionCmd.Equals("close"))
                {
                    Console.WriteLine("MULTIPLAYERMODEL - SendDirectionMessage - IN THE CONDITION");
                    CloseReactionsEvent();
                }
            }catch(Exception e)
            {
                Console.WriteLine("MULTIPLAYERMODEL - SendDirectionMessage - CATCH EXEPTION : " + e.Message);
                ErrorReactionsEvent(e.Message);
            }
        }
    }
 }
