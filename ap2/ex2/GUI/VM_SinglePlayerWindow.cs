using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.ComponentModel;

namespace GUI
{
    class VM_SinglePlayerWindow : PropertyChangeNotifier
    {
        SinglePlayerModel m_singlePlayerModel;
        public VM_SinglePlayerWindow()
        {
            m_singlePlayerModel = new SinglePlayerModel();
            m_singlePlayerModel.PropertyChanged +=
           delegate (Object sender, PropertyChangedEventArgs e)
           {
               NotifyPropertyChanged("Maze" + e.PropertyName);
           };
        }

        public int MazeRows
        {
            get { return m_singlePlayerModel.Rows; }
            set { m_singlePlayerModel.Rows = value; }
        }

        public int MazeCols
        {
            get { return m_singlePlayerModel.Cols; }
            set { m_singlePlayerModel.Cols = value; }
        }

        public string MazeName
        {
            get { return m_singlePlayerModel.Name; }
            set { m_singlePlayerModel.Name = value; }
        }

        public Maze GenerateMaze(string mazeDetails)
        {
            //connect the server for handling the command 
            m_singlePlayerModel = new SinglePlayerModel();

            m_singlePlayerModel.Connect();

            //create generation command from the details
            string generationCommand = "generate " + mazeDetails;
            //send the generationCommand to the server and get the maze JSON string from the server
            string mazeJSON = m_singlePlayerModel.SendAndReceive(generationCommand);
            //return the wanted maze
            return Maze.FromJSON(mazeJSON);            
        }
    }
}
