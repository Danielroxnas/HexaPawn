using HexaPawnConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexaPawnWeb.Services;
using HexaPawnWeb.Models;

namespace HexaPawnWeb.Services
{
    public class GameService : IGameService
    {
        private readonly IBoardService _boardService;

        public GameService(IBoardService boardService)
        {
            _boardService = boardService;
        }

        public List<AvailableAction> GetActions(BoardService board)
        {
            return board.GetAllPlayerAvailableActions(board);
        }

        public BoardService GetBoard()
        {

            _boardService.InitPieces();
            _boardService.InitPlayers(true, true);
            return _boardService as BoardService;
        }

        public BoardService MakeAction(BoardActionDTO boardAction)
        {
            boardAction.BoardService.MakeAction(boardAction.AvailableAction);
            return boardAction.BoardService;

        }




    }
    public class Test
    {
        public Test(Dictionary<string,int> testet)
        {
            Testet = testet;
        }
        public Dictionary<string,int> Testet { get; set; }
    }
}
