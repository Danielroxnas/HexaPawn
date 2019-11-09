using System.Collections.Generic;
using System.Linq;

namespace HexaPawnConsole
{
    public static class Utils
    {
        public static IPlayer PlayerOne { get; set; }
        public static IPlayer PlayerTwo { get; set; }
        public static Pawn SelectPawn(int id)
        {
            return Pawns.FirstOrDefault(x => x.PawnId == id);
        }
        public static List<Pawn> Pawns { get; set; } = new List<Pawn>();
        //public static void InitPlayers()
        //{
        //    PlayerOne = new Human(Piece.ONE);
        //    PlayerTwo = new AI(Piece.TWO);
        //    PlayerOne.AddPawn(1, new Point(1, 3));
        //    PlayerOne.AddPawn(2, new Point(2, 3));
        //    PlayerOne.AddPawn(3, new Point(3, 3));
        //    PlayerTwo.AddPawn(4, new Point(1, 1));
        //    PlayerTwo.AddPawn(5, new Point(2, 1));
        //    PlayerTwo.AddPawn(6, new Point(3, 1));
        //}
        public static IPlayer SelectStartPlayer()
        {
            return PlayerOne;
        }
        //public static IPlayer GetNextPlayer(Piece player)
        //{
            //return Piece.ONE == player ? PlayerTwo : PlayerOne;
        //}
        public static List<Pawn> SelectPawnsByPlayer(Piece player)
        {
            return Pawns.Where(x => x.Player == player).ToList();
        }
        public static List<Pawn> SelectAllPawns()
        {
            return Pawns.ToList();
        }
        public static void ResetPlayers()
        {
           Pawns.ForEach(x =>
            {
                x.Standing = new Point(x.StartPoint);
                x.Removed = false;
            });
        }

        //public static bool PlayerWon(IPlayer player)
        //{
            //var pawns = player.Pawns;
        //    var startPosition = pawns.First().StartPoint.Y;

        //    if(startPosition is 1)
        //    {
        //       return pawns.Where(x => x.Standing.Y == 3).Any();
        //    }
        //    else
        //    {
        //        return pawns.Where(x => x.Standing.Y == 1).Any();
        //    }
        //}
        public static void RemovePawn(Pawn pawn)
        {
            pawn.Removed = true;
        }
    }
}
