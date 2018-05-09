using MazeLib;
using MazeModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace A_mazingWebApp.Controllers.SingleGame
{
    public class SinglePlayerController : ApiController
    {
        private static Model mazeModel = new Model();
        
        // GET api/SinglePlayer/maze/5/5
        public JObject GetGenerateMaze(string name, int rows, int cols)
        {
            Maze maze = mazeModel.GenerateProblem(name, rows, cols);
            JObject obj = JObject.Parse(maze.ToJSON());
            return obj;
        }

        public SolutionAdapter GetSolveMaze(string name, int algorithm)
        {
            SolutionAdapter mazeSol = mazeModel.SolveProblem(name, algorithm);
            return mazeSol;
        }

        // POST: api/SinglePlayer
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SinglePlayer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SinglePlayer/5
        public void Delete(int id)
        {
        }
    }
}
