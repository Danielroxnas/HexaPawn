using HexaPawnConsole11;
using System.Collections.Generic;

namespace HexaPawnConsole1
{
    public interface IBoardState
    {
        IBoard PreviusBoard { get; set; }
        List<IBoard> Boards { get; set; }
        void Copy(IBoard board);
        void CreatePreviuesBoard(IBoard board);
        IBoard GetBoardState(IBoard board);

    }
}