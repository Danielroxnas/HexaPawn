using System.Collections.Generic;
using HexaPawnServices;
using HexaPawnWeb.Models;

namespace HexaPawnWeb.Services
{
    public interface IGameService
    {
        BoardService MakeAction(BoardActionDTO boardAction);
        BoardService GetBoard();
        List<AvailableAction> GetActions(BoardService board);
    }
}