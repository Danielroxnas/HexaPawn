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
            var b = Boards.Where(z => z.Pieces.Cast<int>().SequenceEqual(board.Pieces.Cast<int>())).FirstOrDefault();

            return b ?? null;
        }

        public void CreatePreviuesBoard(BoardService board)
        {
            var b = Boards.Where(z => z.Pieces.Cast<int>().SequenceEqual(board.Pieces.Cast<int>())).FirstOrDefault();
            PreviusBoard = b;
        }
        public void Copy(BoardService board)
        {
            if (!Boards.Any(z => z.Pieces.Cast<int>().SequenceEqual(board.Pieces.Cast<int>())))
            {
                Boards.Add(new BoardService(board, this, null));
            }
        }
    }
}
