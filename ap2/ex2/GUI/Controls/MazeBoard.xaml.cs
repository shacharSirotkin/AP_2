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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    public delegate void PlayerMoved(String direction);

    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class MazeBoard : UserControl
    {
        public MazeBoard(Maze maze)
        {
            Rows = maze.Rows;
            Cols = maze.Cols;
            Maze = maze;
            InitialPos = maze.InitialPos;
            GoalPos = maze.GoalPos;
            FinishGame = false;
            OpponentWon = false;
            InitializeComponent();
            Play();
        }

       // public event PlayerMoved PlayerMovedEvent;

        /*===================================================================*/
        /*                        Dependency properties                      */
        /*===================================================================*/

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeBoard), new PropertyMetadata(0));

        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cols.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeBoard), new PropertyMetadata(0));


        public Maze Maze
        {
            get { return (Maze)GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(Maze), typeof(MazeBoard));


        public Position InitialPos
        {
            get { return (Position)GetValue(InitialPosProperty); }
            set { SetValue(InitialPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitialPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitialPosProperty =
            DependencyProperty.Register("InitialPos", typeof(Position), typeof(MazeBoard));

        public Position GoalPos
        {
            get { return (Position)GetValue(GoalPosProperty); }
            set { SetValue(GoalPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoalPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoalPosProperty =
            DependencyProperty.Register("GoalPos", typeof(Position), typeof(MazeBoard));

        public int DefaultSolver
        {
            get { return (int)GetValue(DefaultSolverProperty); }
            set { SetValue(DefaultSolverProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DefaultSolver.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultSolverProperty =
            DependencyProperty.Register("DefaultSolver", typeof(int), typeof(MazeBoard), new PropertyMetadata(0));


        public bool SolveFinish
        {
            get { return (bool)GetValue(SolveFinishProperty); }
            set { SetValue(SolveFinishProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SolveFinish.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SolveFinishProperty =
            DependencyProperty.Register("SolveFinish", typeof(bool), typeof(MazeBoard));


        public bool FinishGame
        {
            get { return (bool)GetValue(FinishGameProperty); }
            set { SetValue(FinishGameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FinishGame.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FinishGameProperty =
            DependencyProperty.Register("FinishGame", typeof(bool), typeof(MazeBoard));


        public bool OpponentWon
        {
            get { return (bool)GetValue(OpponentWonProperty); }
            set { SetValue(OpponentWonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OpponentWon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpponentWonProperty =
            DependencyProperty.Register("OpponentWon", typeof(bool), typeof(MazeBoard));



        /*===================================================================*/
        /*                            Logic                                  */
        /*===================================================================*/
        private double m_cellWidth;
        private double m_cellHeight;
        private Position m_playerPosition;
        private Image m_playerImage;

        public void Play()
        {
            // Set the cell size.
            m_cellWidth = MazeCanvas.Width / Cols;
            m_cellHeight = MazeCanvas.Height / Rows;
            
            // Set the Maze on the screen.
            int index = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++, index++)
                {
                    Path cell = new Path
                    {
                        Data = new RectangleGeometry(new Rect(j * m_cellWidth, i * m_cellHeight, m_cellWidth, m_cellHeight)),
                        Fill = Brushes.White,
                        Stroke = Brushes.White,
                        StrokeThickness = 0.3
                    };

                    if (Maze[i, j] == CellType.Wall) 
                    {
                        cell.Fill = Brushes.CornflowerBlue;
                    }

                    MazeCanvas.Children.Add(cell);
                }
            }

            // Set the goal image.
            Image GoalImage = new Image
            {
                Width = m_cellWidth,
                Height = m_cellHeight,
                Source = new BitmapImage(new Uri("../Images/Goal.jpg", UriKind.Relative))
            };

            Canvas.SetLeft(GoalImage, GoalPos.Col * GoalImage.Width);
            Canvas.SetTop(GoalImage, GoalPos.Row * GoalImage.Height);
            MazeCanvas.Children.Add(GoalImage);

            m_playerPosition = InitialPos;

            // Set the player image.
            m_playerImage = new Image
            {
                Width = m_cellWidth,
                Height = m_cellHeight,
                Source = new BitmapImage(new Uri("../Images/Player.jpg", UriKind.Relative))
            };

            SetPlayerPosition();
            MazeCanvas.Children.Add(m_playerImage);
        }

        private void SetPlayerPosition()
        {
            Canvas.SetLeft(m_playerImage, m_playerPosition.Col * m_playerImage.Width);
            Canvas.SetTop(m_playerImage, m_playerPosition.Row * m_playerImage.Height);
        }

        public void Restart()
        {
            FinishGame = false;
            m_playerPosition = InitialPos;
            SetPlayerPosition();
        }

        public void MovePlayer(object sender, KeyEventArgs information)
        {
            Console.WriteLine("PlayerMoved");
            if (FinishGame)
            {
                return;
            }
            
            Position test = new Position(m_playerPosition.Row, m_playerPosition.Col);

            if (information.Key == Key.Up)
            {
                test.Row -= 1;
                if ((test.Row >= 0) && (Maze[test.Row, test.Col] != CellType.Wall))
                {
                    m_playerPosition.Row -= 1;
                    SetPlayerPosition();
                    Console.WriteLine("PlayerMoved - up");
                    //PlayerMovedEvent("up");
                }
            }

            if (information.Key == Key.Down)
            {
                test.Row += 1;
                if ((test.Row < Rows) && (Maze[test.Row, test.Col] != CellType.Wall))
                {
                    m_playerPosition.Row += 1;
                    SetPlayerPosition();
                    Console.WriteLine("PlayerMoved - down");
                    //PlayerMovedEvent("down");
                }
            }

            if (information.Key == Key.Right)
            {
                test.Col += 1;
                if ((test.Col < Cols) && (Maze[test.Row, test.Col] != CellType.Wall))
                {
                    m_playerPosition.Col += 1;
                    SetPlayerPosition();
                    Console.WriteLine("PlayerMoved - right");
                    //PlayerMovedEvent("right");
                }
            }

            if (information.Key == Key.Left)
            {
                test.Col -= 1;
                if ((test.Col >= 0) && (Maze[test.Row, test.Col] != CellType.Wall))
                {
                    m_playerPosition.Col -= 1;
                    SetPlayerPosition();
                    Console.WriteLine("PlayerMoved - left");
                    //PlayerMovedEvent("left");
                }
            }
            
            if (m_playerPosition.Equals(GoalPos))
            {
                FinishGame = true;
                WinWindow winWindow = new WinWindow();
            } 
        }

        public void MoveOpponentPlayer(String direction)
        {
            if (FinishGame)
            {
                return;
            }

            if (direction == "up")
            { 
                m_playerPosition.Row -= 1;
                SetPlayerPosition();
            }

            if (direction == "down")
            {
                m_playerPosition.Row += 1;
                SetPlayerPosition();
            }

            if (direction == "right")
            {
                m_playerPosition.Col += 1;
                SetPlayerPosition();
            }

            if (direction == "left")
            {
                m_playerPosition.Col -= 1;
                SetPlayerPosition();
            }

            if (m_playerPosition.Equals(GoalPos))
            {
                FinishGame = true;
                OpponentWon = true;
                WinWindow winWindow = new WinWindow();
            }
        }

        public void Solve(String solution)
        {
            Position position = InitialPos;
            Task task = Task.Run(() =>
            {
                Animation(position);
                foreach (char c in solution)
                {
                    switch (c)
                    {
                        case '0':
                            {
                                position.Col -= 1;
                                Animation(position);
                                break;
                            }
                        case '1':
                            {
                                position.Col += 1;
                                Animation(position);
                                break;
                            }
                        case '2':
                            {
                                position.Row -= 1;
                                Animation(position);
                                break;
                            }
                        case '3':
                            {
                                position.Row += 1;
                                Animation(position);
                                break;
                            }
                        default:
                            break;
                    }
                }
            });
        }

        public void Animation(Position position)
        {
            Thread.Sleep(100);
            Dispatcher.Invoke(() =>
            {
                m_playerPosition = position;
                Canvas.SetLeft(m_playerImage , m_playerPosition.Col * m_playerImage.Width);
                Canvas.SetTop(m_playerImage, m_playerPosition.Row * m_playerImage.Height);
                if (m_playerPosition.Equals(GoalPos))
                {
                    SolveFinish = true;
                }
            });

        }
    }
}