using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexaPawnConsole;
using HexaPawnWeb.Models;
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
        private readonly IGameService _gameService;
        
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;

        }
        [HttpGet]
        public string GameBoard()
        {
           var y = _gameService.GetBoard();
           return JsonConvert.SerializeObject(y);

        }
        [HttpPost]
        public object GetPieces([FromBody]BoardService boardService)
        {
           // var j = JsonConvert.DeserializeObject<BoardService>(boardService.ToString());
            var pieces = _gameService.GetActions(boardService);
            return JsonConvert.SerializeObject(pieces);
        }
        [HttpPost]
        public object GetActions([FromBody]string boardService)
        {
            var j = JsonConvert.DeserializeObject<BoardService>(boardService);
            var actions = _gameService.GetActions(j);
            return JsonConvert.SerializeObject(actions);
        }

        [HttpPost]
        public object MakeAction([FromBody]BoardActionDTO boardAction)
        {
             var board = _gameService.MakeAction(boardAction);
            return JsonConvert.SerializeObject(board);
        }
    }
}