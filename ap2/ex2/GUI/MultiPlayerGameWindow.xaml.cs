using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI
{


    /// <summary>
    /// Interaction logic for MultiPlayerGameWindow.xaml
    /// </summary>
    public partial class MultiPlayerGameWindow : Window
    {
        MazeBoard m_myMazeBoard;
        MazeBoard m_hisMazeBoard;
        VM_MultiPlayerGameWindow m_VM_multiPlayerGameWindow;
        MultiPlayerModel m_multiPlayerModel;

        public MultiPlayerGameWindow(Maze maze, MultiPlayerModel multiPlayerModel)
        {
            m_multiPlayerModel = new MultiPlayerModel();
            Title = maze.Name;
            InitializeComponent();
            m_VM_multiPlayerGameWindow = new VM_MultiPlayerGameWindow(maze);
            m_myMazeBoard = new MazeBoard(maze);
            m_hisMazeBoard = new MazeBoard(maze);

            this.KeyDown += m_myMazeBoard.MovePlayer;
            //m_myMazeBoard.PlayerMovedEvent += PlayerMovedNotifyToServer;
            m_VM_multiPlayerGameWindow.OpponentMoveEvent += OpponentMoveOnBoard;

            Grid.SetColumn(m_myMazeBoard, 0);
            Grid.SetRow(m_myMazeBoard, 1);
            Grid.SetColumn(m_hisMazeBoard, 1);
            Grid.SetRow(m_hisMazeBoard, 1);
            Grid.Children.Add(m_myMazeBoard);
            Grid.Children.Add(m_hisMazeBoard);
        }

        private void PlayerMovedNotifyToServer(String direction)
        {
            Console.WriteLine("EVENT FUNCTION - PlayerMovedNotifyToServer");
            m_multiPlayerModel.SendDirectionMessage(direction);
            Console.WriteLine("EVENT FUNCTION - PlayerMovedNotifyToServer END!!");
        }

        private void OpponentMoveOnBoard(String direction)
        {
            m_hisMazeBoard.MoveOpponentPlayer(direction);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AreYouSure areYouSure = new AreYouSure(this, "Exit multiplayer game");
        }


    }
}
