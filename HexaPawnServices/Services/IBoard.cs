using System.Collections.Generic;

namespace HexaPawnService
{
    public interface IBoard
    {
        Dictionary<string, int> Pieces { get; set; }
        Player CurrentPlayer { get; set; }
        Player P1 { get; set; } 
        Player P2 { get; set; }
    }
}