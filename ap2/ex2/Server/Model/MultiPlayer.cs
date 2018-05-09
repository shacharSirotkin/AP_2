using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using SolveMaze;

namespace Server
{
    /// <summary>
    /// The multiPlayer class.
    /// </summary>
    public class MultiPlayer
    {
        private Player m_hostPlayer;
        private Player m_guestPlayer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <param name="hostPlayer">The host player.</param>
        public MultiPlayer(Maze maze, Player hostPlayer)
        {
            Maze = maze;
            m_hostPlayer = hostPlayer;
        }

        /// <summary>
        /// Adds the opponent.
        /// </summary>
        /// <param name="opponent">The opponent.</param>
        public void addOpponent(TcpClient opponent)
        {
            m_guestPlayer = new Player(opponent, Maze.InitialPos);
        }

        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public Maze Maze { get; internal set; }

        /// <summary>
        /// Returns the opponent of the requester.
        /// </summary>
        /// <param name="requester">The requester.</param>
        /// <returns></returns>
        internal Player Opponent(TcpClient requester)
        {
            if (requester.Equals(m_hostPlayer.Id))
            {
                return m_guestPlayer;
            }
            return m_hostPlayer;
        }
    }
}