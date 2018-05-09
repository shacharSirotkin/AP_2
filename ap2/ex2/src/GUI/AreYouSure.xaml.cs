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
    /// Interaction logic for AreYouSure.xaml
    /// </summary>
    public partial class AreYouSure : Window
    {
        Window m_windowToClose;
        public AreYouSure(Window windowToClose, String title)
        {
            m_windowToClose = windowToClose;
            Title = title;
            InitializeComponent();
            ShowDialog();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (Title.Equals("Exit game"))
            {
                m_windowToClose.Close();
            }
            
            else if (Title.Equals("Restart game"))
            {
                SinglePlayerGameWindow singleplayerGameWindow = m_windowToClose as SinglePlayerGameWindow;
                singleplayerGameWindow.RestartGame();
            }

            else if (Title.Equals("Exit multiplayer game"))
            {
                m_windowToClose.Close();
            }
            Close();
        }
    }
}
