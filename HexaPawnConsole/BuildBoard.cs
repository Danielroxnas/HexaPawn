using System;
using System.Linq;

namespace HexaPawnConsole
{
    public class BuildBoard
    {
        public void CreateBoard()
        {

            var pawns = Utils.SelectAllPawns();
            var x1y1 = pawns.Where(x => x.Standing.X == 1 && x.Standing.Y == 1 && !x.Removed)?.FirstOrDefault()?.PawnId.ToString() ?? "";
            var x2y1 = pawns.Where(x => x.Standing.X == 2 && x.Standing.Y == 1 && !x.Removed)?.FirstOrDefault()?.PawnId.ToString() ?? "";
            var x3y1 = pawns.Where(x => x.Standing.X == 3 && x.Standing.Y == 1 && !x.Removed)?.FirstOrDefault()?.PawnId.ToString() ?? "";

            var x1y2 = pawns.Where(x => x.Standing.X == 1 && x.Standing.Y == 2 && !x.Removed)?.FirstOrDefault()?.PawnId.ToString() ?? "";
            var x2y2 = pawns.Where(x => x.Standing.X == 2 && x.Standing.Y == 2 && !x.Removed)?.FirstOrDefault()?.PawnId.ToString() ?? "";
            var x3y2 = pawns.Where(x => x.Standing.X == 3 && x.Standing.Y == 2 && !x.Removed)?.FirstOrDefault()?.PawnId.ToString() ?? "";

            var x1y3 = pawns.Where(x => x.Standing.X == 1 && x.Standing.Y == 3 && !x.Removed)?.FirstOrDefault()?.PawnId.ToString() ?? "";
            var x2y3 = pawns.Where(x => x.Standing.X == 2 && x.Standing.Y == 3 && !x.Removed)?.FirstOrDefault()?.PawnId.ToString() ?? "";
            var x3y3 = pawns.Where(x => x.Standing.X == 3 && x.Standing.Y == 3 && !x.Removed)?.FirstOrDefault()?.PawnId.ToString() ?? "";

            Console.WriteLine($"{x1y1} | {x2y1} | {x3y1}");
            Console.WriteLine($"----------");
            Console.WriteLine($"{x1y2} | {x2y2} | {x3y2}");
            Console.WriteLine($"----------");
            Console.WriteLine($"{x1y3} | {x2y3} | {x3y3}");


        }
    }
}
