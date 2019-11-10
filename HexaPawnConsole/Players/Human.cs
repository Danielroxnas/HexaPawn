﻿using System.Linq;

namespace HexaPawnConsole
{
    public class Human : IPlayer
    {
        public Human(Color piece, IBoardState boardState)
        {
            Color = piece;
            BoardState = boardState;
        }
        public AvailableActions LastAvailableActions { get; set; } = new AvailableActions();

        public Color Color { get; set; }
        public int TimesWon { get; set; } = 0;
        public IBoardState BoardState { get; }
        public BoardService LastBord { get; set; } 
        public void LastState(BoardService board)
        {
            var b = BoardState.Boards.Where(z => z.Pieces.Cast<int>().SequenceEqual(board.Pieces.Cast<int>())).FirstOrDefault();
            LastBord = b;
        }
    }
}