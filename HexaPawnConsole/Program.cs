using System;
using System.Collections.Generic;
using System.Linq;

namespace HexaPawnConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var move = new MoveService();
            Utils.InitPlayers();
            Console.WriteLine("");
            var player = Utils.SelectStartPlayer();
            bool playerWon = false;
            while (playerWon == false)
            {
                var possibleToMove = move.GetAllAvailablePoints(player.OrderNumber);
                if (possibleToMove.Count() == 0)
                {
                    player.Learn();
                    player = Utils.GetNextPlayer(player.OrderNumber);
                    player.GamesWon += 1;
                    playerWon = true;
                    return;
                }
                if (player != Utils.PlayerTwo)
                {
                    new BuildBoard().CreateBoard();

                    Pawn point;
                    do
                    {
                        var pawns = Utils.SelectPawnsByPlayer(player.OrderNumber).Where(x => !x.Removed).ToList();

                        pawns.ForEach(x =>
                        {
                            Console.WriteLine($"{x.PawnId} : {x.Standing.X}x{x.Standing.Y}");
                        });
                        Console.WriteLine("CHOOSE PAWN");

                        var pawn = Utils.SelectPawn(int.Parse(Console.ReadLine()));
                        Console.WriteLine("CHOOSE DIRECTION: 0:Forward, 1:Left, 2:Right");

                        point = move.MoveViaDirection(pawn, move.Direction(int.Parse(Console.ReadLine())), player);
                    } while (point == null || playerWon);
                }
                else
                {

                    var pawn = move.MoveRandom(player);
                    var pawns = Utils.SelectPawnsByPlayer(player.OrderNumber).Where(x => !x.Removed).ToList();

                    Console.WriteLine($"{player} - {pawn.PawnId} moved to x{pawn.Standing.X} y{pawn.Standing.Y}");

                }
                playerWon = Utils.PlayerWon(player);
                if (playerWon == false)
                {
                    player = Utils.GetNextPlayer(player.OrderNumber);
                }
                else
                {
                    Console.WriteLine($"{player} Won the game");
                    Console.ReadLine();
                }
            }
        }
    }
}
