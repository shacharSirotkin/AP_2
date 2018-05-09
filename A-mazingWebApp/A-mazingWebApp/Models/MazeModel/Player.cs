using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MazeModel
{
    ///<summary> class for players </summary>
    public class Player
    {
        ///<summary>
        /// Constructor. </summary>
        ///<param name ="id"> the client which plays </param>
        ///<param name ="initialPosition"> start position </param>
        public Player(string id, Position initialPosition)
        {
            ClientId = id;
            PositionInMaze = initialPosition;
        }

        /*===================================================================*/
        /*                            Properties                             */
        /*===================================================================*/
        ///<summary> the client's connection </summary>
        public string ClientId
        {
            get;
        }

        ///<summary> position in maze </summary>
        public Position PositionInMaze
        {
            get; private set;
        }
    }
}
