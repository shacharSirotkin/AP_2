﻿using MazeLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace GUI
{
    class VM_MultiPlayerWindow : PropertyChangeNotifier
    {
        private MultiPlayerModel m_MultiPlayerModel;
        public VM_MultiPlayerWindow()
        {
            IsReady = false;
            m_MultiPlayerModel = new MultiPlayerModel();
            m_MultiPlayerModel.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                Console.WriteLine("Maze" + e.PropertyName);
                NotifyPropertyChanged("Maze" + e.PropertyName);
            };
            Games = m_MultiPlayerModel.AskForGamesList();
        }

        public MultiPlayerModel GetmMultiPlayerModel()
        {
            return m_MultiPlayerModel;
        }

        public int MazeRows
        {
            get { return m_MultiPlayerModel.Rows; }
            set { m_MultiPlayerModel.Rows = value; }
        }

        public int MazeCols
        {
            get { return m_MultiPlayerModel.Cols; }
            set { m_MultiPlayerModel.Cols = value; }
        }

        internal void UpdateComboBox()
        {
            Games = m_MultiPlayerModel.AskForGamesList();
        }

        public string MazeName
        {
            get { return m_MultiPlayerModel.Name; }
            set { m_MultiPlayerModel.Name = value; }
        }

        public string MazeButtonContent
        {
            get { return m_MultiPlayerModel.ButtonContent; }
            set { m_MultiPlayerModel.ButtonContent = value; }
        }
        public List<string> Games
        {
            get; set;
        }

        public string SelectedMaze
        {
            get; set;
        }
        public bool IsReady { get; private set; }

        public Maze StartNewGame()
        {
            string details = MazeName + " " + MazeRows + " " + MazeCols;
            string command = "start " + details;
            this.MazeButtonContent = "waiting...";
            Maze maze = Maze.FromJSON(m_MultiPlayerModel.ConnectGameInServer(command));
            this.MazeButtonContent = "now!";
            this.IsReady = true;
            return maze;
        }

        public Maze JoinGame()
        {
            string command = "join " + SelectedMaze;
            Maze maze = Maze.FromJSON(m_MultiPlayerModel.ConnectGameInServer(command));
            return maze;
        }
    }
}