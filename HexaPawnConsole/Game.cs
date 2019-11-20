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
            Console.Write("A ");
            Enumerable.Range(1, 3).ToList().ForEach(x => Console.Write($"[{(int)_boardService.Pieces[$"A{x}"]}]"));           
            Console.WriteLine();
            Console.Write("B ");
            Enumerable.Range(1, 3).ToList().ForEach(x => Console.Write($"[{(int)_boardService.Pieces[$"B{x}"]}]"));
            Console.WriteLine();
            Console.Write("C ");
            Enumerable.Range(1, 3).ToList().ForEach(x => Console.Write($"[{(int)_boardService.Pieces[$"C{x}"]}]"));
            Console.WriteLine();
            Console.WriteLine("   1  2  3");

            var actions = _boardService.GetAllPlayerAvailableActions1(_boardService.CurrentPlayer.Color);

            int i = 0;
            actions.ForEach(x => Console.WriteLine($"{i++}:{x.Key} - {x.Action}"));
            return actions;
        }
    }

}
