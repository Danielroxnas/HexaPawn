using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexaPawnConsole
{
    public class AI : IPlayer
    {
        public int GamesWon { get; set; }
        public OrderNumber OrderNumber { get; set; }
        public AI(OrderNumber players)
        {
            OrderNumber = players;
        }

        public void AddPawn(int id, Point point)
        {
            new Pawn(id, point, false, OrderNumber);
        }


        public void Learn()
        {
            var pawnMove = Utils.History[Utils.History.Count - 2];
            var pawn = Utils.SelectPawn(pawnMove.Item1);
            pawn.RemovedMoves.Add(pawnMove.Item2);
        }
    }
}
