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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            // Take the default settings from the current settings.
            IP.Text = Properties.Settings.Default.IP;
            Port.Text = Properties.Settings.Default.Port;
            DefaultRows.Text = Properties.Settings.Default.DefaultRows;
            DefaultColumns.Text = Properties.Settings.Default.DefaultColumns;
            DefaultAlgorithm.Text = Properties.Settings.Default.DefaultAlgorithm;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            // Save the new settings.
            Properties.Settings.Default.IP = IP.Text.ToString();
            Properties.Settings.Default.Port = Port.Text.ToString();
            Properties.Settings.Default.DefaultRows = DefaultRows.Text.ToString();
            Properties.Settings.Default.DefaultColumns = DefaultColumns.Text.ToString();
            Properties.Settings.Default.DefaultAlgorithm = DefaultAlgorithm.Text.ToString();
            Properties.Settings.Default.Save();

            // Close the window.
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Close the window.
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.Show();
        }
    }
}