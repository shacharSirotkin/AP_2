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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MazeGeneratorLib;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            SinglePlayerWindow singlePlayerWindow = new SinglePlayerWindow();
            singlePlayerWindow.Owner = this;
            singlePlayerWindow.ShowDialog();
        }

        private void MultiPlayer_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MultiplayerWindow multiPlayerWindow = new MultiplayerWindow();
            multiPlayerWindow.Owner = this;
            multiPlayerWindow.ShowDialog();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            SettingsWindow settings = new SettingsWindow();
            settings.Owner = this;
            settings.ShowDialog();
        }
    }
}
