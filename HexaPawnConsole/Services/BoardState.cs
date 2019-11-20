using System;
using System.Collections.Generic;
using System.Linq;

namespace HexaPawnConsole
{
    public class BoardState : IBoardState
    {
        public BoardService PreviusBoard { get; set; }
        public List<BoardService> Boards { get; set; }

        public BoardState()
        {
            Boards = new List<BoardService>();
        }
        public BoardService GetBoardState(BoardService board)
        {
            var b = Boards.Where(x => x.Pieces.OrderBy(kvp => kvp.Key).SequenceEqual(board.Pieces.OrderBy(kvp => kvp.Key))).FirstOrDefault();

            return b ?? null;
        }
        public void Copy(BoardService board)
        {
            if(!Boards.Any(x => x.Pieces.OrderBy(kvp => kvp.Key).SequenceEqual(board.Pieces.OrderBy(kvp => kvp.Key))))
            {
                Boards.Add(new BoardService(board, this, null));
            }
        }
    }
}
