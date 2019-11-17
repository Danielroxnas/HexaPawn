using System.Collections.Generic;
using HexaPawnConsole;
using HexaPawnWeb.Models;

namespace HexaPawnWeb.Services
{
    public interface IGameService
    {
        BoardService MakeAction(BoardActionDTO boardAction);
        BoardService GetBoard();
        List<AvailableAction1> GetActions(BoardService board);
    }
}