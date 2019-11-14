using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexaPawnServices;
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
        public BoardService GameBoard()
        {
           return  _gameService.GetBoard();
            //return JsonConvert.SerializeObject(y);

        }

        [HttpPost]
        public object GetActions([FromBody]object boardService)
        {
            var j = JsonConvert.DeserializeObject<BoardService>(boardService.ToString());
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