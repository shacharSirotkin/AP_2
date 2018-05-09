using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// The main program of the client.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client(IPAddress.Parse("127.0.0.1"), 8888);
            client.Start();
            client.Stop();
        }
    }
}
