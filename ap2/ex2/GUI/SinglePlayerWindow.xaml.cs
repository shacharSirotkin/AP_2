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
    /// Interaction logic for SinglePlayerWindow.xaml
    /// </summary>
    public partial class SinglePlayerWindow : Window
    {
        private VM_SinglePlayerWindow m_singlePlayerVM;
        public SinglePlayerWindow()
        {
            m_singlePlayerVM = new VM_SinglePlayerWindow();
            this.DataContext = m_singlePlayerVM;
            InitializeComponent();
        }

        private void MazeDetails_Loaded(object sender, RoutedEventArgs e)
        {
            m_singlePlayerVM.MazeRows = int.Parse(Properties.Settings.Default.DefaultRows);
            m_singlePlayerVM.MazeCols = int.Parse(Properties.Settings.Default.DefaultColumns);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            string mazeDetailsString = m_singlePlayerVM.MazeName + " " + m_singlePlayerVM.MazeRows + " " + m_singlePlayerVM.MazeCols;
            Maze maze = m_singlePlayerVM.GenerateMaze(mazeDetailsString);
            Hide();
            SinglePlayerGameWindow SingleGame = new SinglePlayerGameWindow(maze);
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.Show();
        }
    }
}