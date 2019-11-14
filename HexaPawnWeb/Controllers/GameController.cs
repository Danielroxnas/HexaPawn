using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexaPawnServices;
using HexaPawnWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HexaPawnWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IBoardService _boardService;
        
        public GamesController(IBoardService boardService)
        {
            _boardService = boardService;
            _boardService.InitPieces();
            _boardService.InitPlayers(true,true);
        }
        [HttpGet]
        public List<AvailableAction> Game()
        {
            var x = new GameService(_boardService);
            return x.AvailableActions();

        }
        [HttpGet]
        public object GameBoard()
        {
            var x = new GameService(_boardService);

            var y = x.GetPieces();
            return JsonConvert.SerializeObject(y);

        }
        [HttpGet]
        public List<AvailableAction> GetActions()
        {
            return _boardService.GetAllPlayerAvailableActions(_boardService.CurrentPlayer.Color);
        }

        [HttpPost]
        public bool MakeAction(AvailableAction availableAction)
        {
            return false;
        }

        [HttpPost]
        public bool MakeAction(object availableAction)
        {
            return false;
        }
    }
}