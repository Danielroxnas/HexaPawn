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
        public List<AvailableAction1> GenerateBoard()
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
        public List<AvailableAction1> AvailableActions()
        {

            //for (int y = 0; y <= 2; y++)
            //{
            //    Console.Write($"{y} |");
            //    for (int x = 0; x <= 2; x++)
            //    {
            //        if (_boardService.Pieces[y, x] == 2) Console.Write("[2]");
            //        if (_boardService.Pieces[y, x] == 0) Console.Write("[ ]");
            //        if (_boardService.Pieces[y, x] == 1) Console.Write("[1]");
            //    }
            //    Console.WriteLine("");
            //}
            //Console.WriteLine("    0  1  2");

            Enumerable.Range(1, 3).ToList().ForEach(x => Console.Write($"[{_boardService.Pieces1[$"A{x}"]}]"));
            Console.WriteLine();
            Enumerable.Range(1, 3).ToList().ForEach(x => Console.Write($"[{_boardService.Pieces1[$"B{x}"]}]"));

            Console.WriteLine();
            Enumerable.Range(1, 3).ToList().ForEach(x => Console.Write($"[{_boardService.Pieces1[$"C{x}"]}]"));

            Console.WriteLine();

            var actions = _boardService.GetAllPlayerAvailableActions1(_boardService.CurrentPlayer.Color);

            int i = 0;
            actions.ForEach(x => Console.WriteLine($"{i++}:{x.Key} - {x.Color} - {x.Action}"));

            //for (int i = 0; i < actions.Count(); i++)
            //{

            //    //Console.WriteLine($"{i}: {actions[i].Action} " +
            //    //    $"{actions[i].FromY}.{actions[i].FromX} " +
            //    //    $"- {actions[i].ToY}.{actions[i].ToX}");

            //}
            return actions;
        }
    }

}
