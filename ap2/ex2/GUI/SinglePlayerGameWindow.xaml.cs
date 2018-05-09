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
using MazeLib;

namespace GUI
{
    /// <summary>
    /// Interaction logic for SinglePlayerGameWindow.xaml
    /// </summary>
    public partial class SinglePlayerGameWindow : Window
    {
        MazeBoard m_MazeBoard;
        VM_SinglePlayerGameWindow m_VM_singlePlayerGameWindow;

        public SinglePlayerGameWindow(Maze maze)
        {
            Title = maze.Name;
            InitializeComponent();
            m_VM_singlePlayerGameWindow = new VM_SinglePlayerGameWindow(maze);
            m_MazeBoard = new MazeBoard(maze);
            stackPanel.Children.Add(m_MazeBoard);
            this.KeyDown += m_MazeBoard.MovePlayer;
            ShowDialog();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            AreYouSure areYouSure = new AreYouSure(this, "Exit game");
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            AreYouSure areYouSure = new AreYouSure(this, "Restart game");
        }

        internal void RestartGame()
        {
            m_MazeBoard.Restart();
        }

        private void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            String solution = m_VM_singlePlayerGameWindow.SolveMaze();
            m_MazeBoard.Solve(solution);
        }
    } 
}