﻿using System;
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
            var b = Boards.Where(z => z.Pieces1.Equals(board.Pieces1)).FirstOrDefault();

            return b ?? null;
        }

        public void CreatePreviuesBoard(BoardService board)
        {
            var b = Boards.Where(z => z.Pieces1.Equals(board.Pieces1)).FirstOrDefault();
            PreviusBoard = b;
        }
        public void Copy(BoardService board)
        {
            if(!Boards.Any(x => x.Pieces1.OrderBy(kvp => kvp.Key).SequenceEqual(board.Pieces1.OrderBy(kvp => kvp.Key))))
        //    if (!Boards.Any(z => z.Pieces1.Equals(board.Pieces1)))
            {
                Boards.Add(new BoardService(board, this, null));
            }
        }
    }
}
