using System.Collections.Generic;

namespace HexaPawnServices
{
    public interface IBoardState
    {
        BoardService PreviusBoard { get; set; }
        List<BoardService> Boards { get; set; }
        void Copy(BoardService board);
        void CreatePreviuesBoard(BoardService board);
        BoardService GetBoardState(BoardService board);
    }
}