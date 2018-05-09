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

namespace GUI.Controls
{
    /// <summary>
    /// Interaction logic for MazeDetails.xaml
    /// </summary>
    public partial class MazeDetailsUserControl : UserControl
    {
        public MazeDetailsUserControl()
        {
            InitializeComponent();
        }

        //Rows DependencyProperty for accessing the MazeRows TextBox
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeDetailsUserControl));

        //Cols DependencyProperty for accessing the MazeCols TextBox
        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cols.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeDetailsUserControl));

        //Name DependencyProperty for accessing the MazeName TextBox
        public string MazeNameProp
        {
            get { return (string)GetValue(MazeNamePropProperty); }
            set { SetValue(MazeNamePropProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeNamePropProperty =
            DependencyProperty.Register("MazeNameProp", typeof(string), typeof(MazeDetailsUserControl));
    }
}