using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace MazeModel
{
    ///<summary>
    /// An interface for models. </summary>
    public interface IModel
    {
        Maze GenerateProblem(string name, int numberOfRows, int numberOfColumns);

        List<string> GetProblems();

        string FinishGame(string requester, string name);

        void StartGame(string requester, string name, int numberOfRows, int numberOfColumns);

        MultiPlayer getGame(string requester);

        SolutionAdapter SolveProblem(string name, int algorithm);

        Maze JoinGame(string requester, string name);
    }
}