using System;
using System.Collections.Generic;

namespace HexaPawnService
{
    public class Board : IBoard
    {
        public Guid Id { get; set; }
        public Dictionary<string, int> Pieces { get; set; }
        public Player CurrentPlayer { get; set; }
        public Player P1 { get; set; } = new Player(Color.White, null, true);
        public Player P2 { get; set; } = new Player(Color.Black, null, true);
    }
}
