using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPawnConsole
{
    public enum OrderNumber { ONE, TWO}
    public static class AllPawns
    {
        public static List<Pawn> Pawns { get; set; } = new List<Pawn>();
    }
    public class Human : IPlayer
    {
        public int GamesWon { get; set; }
        public Human(OrderNumber players)
        {
            OrderNumber = players;
        }
        public OrderNumber OrderNumber { get; set; }

        public void AddPawn(int id, Point point)
        {
            new Pawn(id, point, true, OrderNumber );
        }
        public void Learn()
        {

        }
    }

    public interface IPlayer
    {
        public int GamesWon { get; set; }
        public OrderNumber OrderNumber { get; set; }
        public void Learn();
        public void AddPawn(int id, Point point);
    }
}
