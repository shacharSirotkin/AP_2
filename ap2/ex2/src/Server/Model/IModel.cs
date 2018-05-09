using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Server
{
    ///<summary>
    /// An interface for models. </summary>
    public interface IModel
    {
        Maze GenerateProblem(string name, int numberOfRows, int numberOfColumns);

        List<Maze> GetProblems();

        TcpClient FinishGame(TcpClient requester, string name);

        MultiPlayer StartGame(TcpClient requester, string name, int numberOfRows, int numberOfColumns);

        MultiPlayer getGame(TcpClient requester);

        SolutionAdapter SolveProblem(string name, int algorithm);

        Maze JoinGame(TcpClient requester, string name);
    }
}