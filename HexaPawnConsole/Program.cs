using System;

namespace HexaPawnConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();
            board.InitPlayers(new Human(), new AI());
            var bb = new Game();
            while (true)
            {
                var winner = false;
               
                while (winner == false)
                {
                    var actions = bb.GenerateBoard(board);
                    if(!board.CheckAvailableActions(actions))
                    {
                        winner = true;
                        continue;
                    }
                    if(board.CheckIfCurrentIsAI())
                    {
                        winner = board.MakeAction(actions);
                    }
                    else
                    {
                        var index = Console.ReadLine();
                        winner = board.MakeAction(actions,int.Parse(index));
                    }
                }
                Console.WriteLine($"{board.currentPiece} WINNER");
                Console.ReadKey();
                board.ResetBoard();
            }
        }
    }
}
