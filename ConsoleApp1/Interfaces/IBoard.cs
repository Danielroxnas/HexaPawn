using HexaPawnConsole11;
using System.Collections.Generic;

namespace HexaPawnConsole1
{
    public interface IBoard
    {
        Player CurrentPlayer { get; set; }
        Player P1 { get; set; }
        Player P2 { get; set; }
        List<AvailableAction1> RemovedActions { get; set; }
        Dictionary<string, Color> Pieces1 { get; set; }
    }
}