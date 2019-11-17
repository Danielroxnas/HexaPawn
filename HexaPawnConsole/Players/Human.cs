﻿using System.Linq;

namespace HexaPawnConsole
{
    public class Human
    {
        public Human(Color piece, IBoardState boardState)
        {
            Color = piece;
            BoardState = boardState;
        }
        public AvailableAction1 LastAvailableActions { get; set; } = new AvailableAction1();

        public Color Color { get; set; }
        public int TimesWon { get; set; } = 0;
        public IBoardState BoardState { get; }
        public BoardService LastBord { get; set; }
        public void LastState(BoardService board)
        {
            var b = BoardState.Boards.Where(x => x.Pieces1.OrderBy(kvp => kvp.Key).SequenceEqual(board.Pieces1.OrderBy(kvp => kvp.Key))).FirstOrDefault();
            
            LastBord = b ?? board;
        }
    }
}
