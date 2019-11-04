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
            var pawn1 = Utils.SelectPawn(1);
            var player = Utils.SelectStartPlayer();
            move.MoveViaDirection(pawn1, DirectionType.Forward);
            bool playerWon = false ;
            do
            {
                player = Utils.SelectNextPlayer(player.OrderNumber);
                if (player != Utils.PlayerTwo)
                {
                    var possibleToMove = move.GetAllAvailablePoints(player.OrderNumber);
                    if (possibleToMove.Count() == 0)
                    {
                        player = Utils.SelectNextPlayer(player.OrderNumber);
                        player.GamesWon += 1;
                        playerWon = true;
                    }
                    else
                    {
                        Point point;
                        do
                        {
                            var pawns = Utils.SelectPawnsByPlayer(player.OrderNumber).Where(x => !x.Removed).ToList();

                            pawns.ForEach(x =>
                            {
                                Console.Write($"{x.PawnId} :");
                                Console.WriteLine($"{x.Standing.X}x{x.Standing.Y}");
                            });
                            Console.WriteLine("CHOOSE PAWN");

                            possibleToMove = move.GetAllAvailablePoints(player.OrderNumber);
                            pawn1 = Utils.SelectPawn(int.Parse(Console.ReadLine()));
                            Console.WriteLine("CHOOSE DIRECTION: 0:Forward, 1:Left, 2:Right");

                            point = move.MoveViaDirection(pawn1, move.Direction(int.Parse(Console.ReadLine())));
                            playerWon = Utils.PlayerWon(player.OrderNumber);
                        } while (point == null || playerWon );
                    }
                }
                else
                {
                    var possibleToMove = move.GetAllAvailablePoints(player.OrderNumber);

                    if (possibleToMove.Count() == 0)
                    {
                        player.Learn();
                        player = Utils.SelectNextPlayer(player.OrderNumber);
                        player.GamesWon += 1;
                        playerWon = false;
                    }
                    var point = move.MoveRandom(player.OrderNumber);
                    var pawns = Utils.SelectPawnsByPlayer(player.OrderNumber).Where(x => !x.Removed).ToList();
                    pawns.ForEach(x =>
                    {
                        Console.Write($"{x.PawnId} :");
                        Console.WriteLine($"{x.Standing.X}x{x.Standing.Y}");
                    });
                    Console.WriteLine($"------------------------");

                    playerWon = Utils.PlayerWon(player.OrderNumber);

                }
            } while (playerWon == false);
        }
    }
}
