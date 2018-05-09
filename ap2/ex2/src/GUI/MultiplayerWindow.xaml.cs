using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    public delegate void comboBoxFunc();

    public partial class MultiplayerWindow : Window
    {
        private VM_MultiPlayerWindow m_multiPlayerVM;
        private WaitingWindow waitingWindow;
        public event comboBoxFunc ComboBoxFuncEvent;
        public MultiplayerWindow() 
        {
            waitingWindow = new WaitingWindow();
            m_multiPlayerVM = new VM_MultiPlayerWindow();
            this.DataContext = m_multiPlayerVM;
            m_multiPlayerVM.MazeButtonContent = "start";
            m_multiPlayerVM.SelectedMaze = "Select Maze";
            InitializeComponent();
            JoinButton.IsEnabled = false;
            ComboBoxFuncEvent += m_multiPlayerVM.UpdateComboBox;
        }

        private void MazeDetailsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            m_multiPlayerVM.MazeRows = int.Parse(Properties.Settings.Default.DefaultRows);
            m_multiPlayerVM.MazeCols = int.Parse(Properties.Settings.Default.DefaultColumns);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Maze maze = null;
            waitingWindow.Show();
            waitingWindow.Activate();
            Task startOperation = new Task(() =>
            {
                maze = m_multiPlayerVM.StartNewGame();
            });
            startOperation.Start();
            while (!m_multiPlayerVM.IsReady)
            {
                Thread.Sleep(100);
            }
            waitingWindow.Close();
            this.Hide();
            MultiPlayerGameWindow multiGameWindow = new MultiPlayerGameWindow(maze, m_multiPlayerVM.GetmMultiPlayerModel());
            multiGameWindow.Owner = this;
            multiGameWindow.ShowDialog();
        }

        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            Maze maze = m_multiPlayerVM.JoinGame();
            this.Hide();
            MultiPlayerGameWindow multiGameWindow = new MultiPlayerGameWindow(maze, m_multiPlayerVM.GetmMultiPlayerModel());
            multiGameWindow.Owner = this;
            multiGameWindow.ShowDialog();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!m_multiPlayerVM.SelectedMaze.Equals("Select Maze"))
            {
                JoinButton.IsEnabled = true;
            }
            else
            {
                JoinButton.IsEnabled = false;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.Show();
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            ComboBoxFuncEvent();
            comboBox.ItemsSource = m_multiPlayerVM.Games;
            comboBox.Items.Refresh();
        }
    }
}
