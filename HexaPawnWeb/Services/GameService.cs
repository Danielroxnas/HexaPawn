using HexaPawnServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HexaPawnWeb.Services
{
    public class GameService
    {
        private readonly IBoardService _boardService;

        public GameService(IBoardService boardService)
        {

            _boardService = boardService;
            _boardService.InitPlayers(true, false);
            _boardService.InitPieces();
        }
        public List<AvailableAction> AvailableActions()
        {
            return _boardService.GetAllPlayerAvailableActions(_boardService.CurrentPlayer.Color);

        }
        public object GetPieces()
        {
            return _boardService.Pieces;

        }
    }
}
