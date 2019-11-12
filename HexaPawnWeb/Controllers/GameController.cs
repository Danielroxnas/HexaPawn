using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexaPawnServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexaPawnWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [HttpGet]
        public List<AvailableAction> Game()
        {
            return new List<AvailableAction>();
        }
    }
}