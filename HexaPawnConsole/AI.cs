using System.Collections.Generic;
using System.Linq;

namespace HexaPawnConsole
{
    public class AI : IPlayer
    {
        public int GamesWon { get; set; }
        public Piece OrderNumber { get; set; }
        public List<Pawn> Pawns { get; set; }

        public AI(Piece players)
        {
            OrderNumber = players;
            Pawns = new List<Pawn>();

        }


        public void AddPawn(int id, Point point)
        {
            Pawns.Add(new Pawn(id, point, OrderNumber));
        }

        public void Learn()
        {
        }

        //public IPlayer OppositePlayer()
        //{
        //    return Utils.GetNextPlayer(OrderNumber);
        //}
    }
}
