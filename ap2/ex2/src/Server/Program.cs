using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Server
{
    class Program
    {
        /// <summary>
        /// Create the server.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            IModel model = new Model();
            IController controller = new Controller(model);
            IView view = new View(int.Parse(ConfigurationSettings.AppSettings["Port"]), controller);
            view.Start();
        }
    }
}
