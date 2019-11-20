using HexaPawnConsole1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexaPawnServices.Repository
{
    public class BoardRepository
    {
        public List<IBoard> Boards { get; set; }

        public BoardRepository()
        {
            Boards = new List<IBoard>();
        }
        public IBoard GetBoardState(IBoard board)
        {
            var b = Boards.Where(x => x.Pieces1.OrderBy(kvp => kvp.Key).SequenceEqual(board.Pieces1.OrderBy(kvp => kvp.Key))).FirstOrDefault();
            return b ?? null;
        }
        public void Copy(IBoard board)
        {
            if (!Boards.Any(x => x.Pieces1.OrderBy(kvp => kvp.Key).SequenceEqual(board.Pieces1.OrderBy(kvp => kvp.Key))))
            //if (!Boards.Any(z => z.Pieces1.Equals(board.Pieces1)))
            {
                Boards.Add(new Board
                {
                    CurrentPlayer = board.CurrentPlayer,
                    P1 = board.P1,
                    P2 = board.P2,
                    Pieces1 = board.Pieces1,
                    RemovedActions = board.RemovedActions
                });
            }
        }
    }
}
