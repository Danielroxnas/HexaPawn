using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPawnConsole
{
    public enum OrderNumber { ONE, TWO}
    public class Human : IPlayer
    {
        public int GamesWon { get; set; }
        public Human(OrderNumber players)
        {
            OrderNumber = players;
            Pawns = new List<Pawn>();
        }
        public OrderNumber OrderNumber { get; set; }
        public void AddPawn(int id, Point point)
        {
            Pawns.Add(new Pawn(id, point, OrderNumber ));
        }
        public List<Pawn> Pawns { get; set; }
        public void Learn()
        {

        }

        public IPlayer OppositePlayer()
        {
            return Utils.GetNextPlayer(OrderNumber);
        }
    }
    public interface IPlayer
    {
        List<Pawn> Pawns { get; set; }
        int GamesWon { get; set; }
        OrderNumber OrderNumber { get; set; }
        void Learn();
        void AddPawn(int id, Point point);
        IPlayer OppositePlayer();

    }
}
