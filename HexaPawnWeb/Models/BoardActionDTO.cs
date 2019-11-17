using HexaPawnConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HexaPawnWeb.Models
{
    public class BoardActionDTO
    {
        public BoardService BoardService{ get; set; }
        public AvailableAction1 AvailableAction { get; set; }

    }
}
