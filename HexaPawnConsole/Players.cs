using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPawnConsole
{
    public class Human : IPlayer
    {
        public int GamesWon { get; set; }
        public Human(Piece players)
        {
            OrderNumber = players;
        }
        public Piece OrderNumber { get; set; }
        

        //public IPlayer OppositePlayer()
        //{
        //    return Utils.GetNextPlayer(OrderNumber);
        //}
    }
    public interface IPlayer
    {
        //List<Pawn> Pawns { get; set; }
        int GamesWon { get; set; }
        Piece OrderNumber { get; set; }
        //void Learn();
        //void AddPawn(int id, Point point);
        //IPlayer OppositePlayer();

    }
}
