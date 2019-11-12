using System;
using System.Collections.Generic;
using System.Linq;

namespace HexaPawnConsole
{
    public class Game
    {
        private readonly IBoardService _boardService;

        public Game(IBoardService boardService)
        {
            _boardService = boardService;
        }
        public List<AvailableAction> GenerateBoard()
        {
            _boardService.InitPlayers(true, false);
            _boardService.InitPieces();
            while (true)
            {
                var winner = false;

                while (winner == false)
                {
                    try
                    {
                        var actions = AvailableActions();
                        if (_boardService.CheckIfCurrentPlayerIsAI())
                        {
                            winner = _boardService.MakeRandomAction(actions);
                        }
                        else
                        {
                            var index = Console.ReadLine();
                            winner = _boardService.MakeAction(actions, int.Parse(index));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error {ex.Message} try again!");
                    }
                }
                Console.WriteLine($"{_boardService.CurrentPlayer.Color} WINNER");
                Console.WriteLine($"{_boardService.CurrentPlayer.TimesWon} wins");
                Console.ReadKey();
                _boardService.ResetBoard();
            }

        }
        public List<AvailableAction> AvailableActions()
        {

            for (int y = 0; y <= 2; y++)
            {
                Console.Write($"{y} |");
                for (int x = 0; x <= 2; x++)
                {
                    if (_boardService.Pieces[y, x] == Color.Black) Console.Write("[2]");
                    if (_boardService.Pieces[y, x] == Color.Empty) Console.Write("[ ]");
                    if (_boardService.Pieces[y, x] == Color.White) Console.Write("[1]");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("    0  1  2");
            var actions = _boardService.GetAllPlayerAvailableActions(_boardService.CurrentPlayer.Color);

            for (int i = 0; i < actions.Count(); i++)
            {

                Console.WriteLine($"{i}: {actions[i].Action} " +
                    $"{actions[i].FromY}.{actions[i].FromX} " +
                    $"- {actions[i].ToY}.{actions[i].ToX}");

            }
            return actions;
        }
    }

}
