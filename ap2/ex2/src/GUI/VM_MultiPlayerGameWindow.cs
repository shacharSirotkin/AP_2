using MazeLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI
{
    public delegate void OpponentMove(String direction);

    class VM_MultiPlayerGameWindow
    {
        public event OpponentMove OpponentMoveEvent;

        MultiPlayerModel m_multiPlayerModel;
        Maze m_maze;

        public VM_MultiPlayerGameWindow(Maze maze)
        {
            m_multiPlayerModel = new MultiPlayerModel();
            m_multiPlayerModel.OpenPlayReaderConnection();
            m_maze = maze;
            m_multiPlayerModel.ReactionsToOpponenetEvent += OpponentMove;
        }

        internal void NotifyMove(object sender, KeyEventArgs information)
        {
            Console.WriteLine("NotifyMove");
        }

        public void OpponentMove(String JSONDirection)
        {
            dynamic direction = JsonConvert.DeserializeObject<dynamic>(JSONDirection);
            var directionString = direction.direction;

            Console.WriteLine("directionString: " + directionString); //TODO REMOVE!!

            OpponentMoveEvent(directionString);
        }
    }
}
