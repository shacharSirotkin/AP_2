using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MazeModel;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace A_mazingWebApp.Controllers
{
    public class MultiGameHub : Hub
    {
        private static Model mazeModel = new Model();

        public void StartGame(string name, int rows, int cols)
        {
            mazeModel.StartGame(Context.ConnectionId, name, rows, cols);
            GameList();
        }

        public void JoinGame(string name)
        {
            Maze maze = mazeModel.JoinGame(Context.ConnectionId,name);
            JObject jMaze = JObject.Parse(maze.ToJSON());
            Clients.Client(Context.ConnectionId).drawMazes(jMaze);
            Clients.Client(mazeModel.GetOpponenetId(Context.ConnectionId)).drawMazes(jMaze);
        }

        public void GameList()
        {
            JObject obj = new JObject();
            obj["gameList"]= JToken.FromObject(mazeModel.GetProblems());
            Clients.All.fillGameSelection(obj);
        }

        public void SendPlayMessage(string playMessage)
        {
            string opponentId = mazeModel.GetOpponenetId(Context.ConnectionId);
            if (opponentId == null)
                return;
            Clients.Client(opponentId).gotPlayMessage(playMessage);
        }
    }
}