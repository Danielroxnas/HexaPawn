using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexaPawnConsole
{
    public class Computer
    {
        public void hej()
        {
            var move = new MoveService();
            Utils.InitPlayers();

            var pawn1 = Utils.SelectPawn(1);
            var player = Utils.SelectStartPlayer();
            move.MoveViaDirection(pawn1, DirectionType.Forward);

            IPlayer playerWon = null;
            do
            {
                player = Utils.SelectNextPlayer(player.OrderNumber);
                if (player != Utils.PlayerTwo)
                {
                    var possibleToMove = move.GetAllAvailablePoints(player.OrderNumber);
                    if(possibleToMove is null)
                    {
                        player = Utils.SelectNextPlayer(player.OrderNumber);
                        player.GamesWon += 1;
                        playerWon = player;
                    }
                    else
                    {
                        Pawn point;
                        do
                        {
                          point =   move.MoveViaDirection(pawn1, DirectionType.Forward);
                        } while (point == null);
                    }
                }
                else
                {
                    var possibleToMove = move.GetAllAvailablePoints(player.OrderNumber);

                    if (possibleToMove is null)
                    {
                        player = Utils.SelectNextPlayer(player.OrderNumber);
                        player.GamesWon += 1;
                        playerWon = player;
                    }
                    move.MoveRandom(player.OrderNumber);
                }
            } while (playerWon is null);
        }
    }
}
