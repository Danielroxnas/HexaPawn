using System;
using System.Collections.Generic;
using System.Linq;

namespace HexaPawnConsole
{
    public interface IPlayer
    {
        Piece Piece { get; set; }
        int TimesWon { get; set; }
    }
    public class AI : IPlayer
    {
        public Piece Piece { get; set; }
        public int TimesWon { get; set; } = 0;
    }

    public class Human : IPlayer
    {
        public Piece Piece { get; set; }
        public int TimesWon { get; set; } = 0;
    }
    public class CopyBoards
    {

        public List<Board> BoardsCopy;

        public CopyBoards()
        {
            BoardsCopy = new List<Board>();
        }
        public void Copy(Board board)
        {
            if (!BoardsCopy.Any(z => z.Pieces.Cast<int>().SequenceEqual(board.Pieces.Cast<int>())))
            {

                BoardsCopy.Add(new Board(board));
            }
        }
    }

    public class Board
    {
        public Piece[,] Pieces;
        public List<IPlayer> players = new List<IPlayer>();
        public IPlayer P1;
        public IPlayer P2;
        public Piece currentPiece;
        public int BoardSizeRows { get; }
        public int BoardSizeColumns { get; }
        private CopyBoards CopyBoards;

        public Board(Board board)
        {
            BoardSizeRows = 3;
            BoardSizeColumns = 3;
            Pieces = new Piece[BoardSizeColumns, BoardSizeRows];
            for (int y = 0; y <= 2; y++)
            {
                for (int x = 0; x <= 2; x++)
                {
                    Pieces[y, x] = board.Pieces[y, x];
                }
            }
            CopyBoards = new CopyBoards();
        }
        public void CopyBoard()
        {
            CopyBoards = new CopyBoards();

        }
        public Board()
        {
            BoardSizeRows = 3;
            BoardSizeColumns = 3;
            Pieces = new Piece[BoardSizeColumns, BoardSizeRows];
            CopyBoards = new CopyBoards();
        }
        public void ResetBoard()
        {
            Pieces = new Piece[BoardSizeColumns, BoardSizeRows];
            currentPiece = Piece.White;
            for (int i = 0; i <= 2; i++)
            {
                Pieces[0, i] = Piece.White;
                Pieces[2, i] = Piece.Black;
            }
        }
        public bool CheckIfCurrentIsAI()
        {
            if (currentPiece == Piece.White && P1.GetType() == typeof(AI) ||
                currentPiece == Piece.Black && P2.GetType() == typeof(AI))
            {
                return true;
            }
            else if (
                currentPiece == Piece.White && P1.GetType() == typeof(Human) ||
                currentPiece == Piece.Black && P2.GetType() == typeof(Human))
            {
                return false;
            }
            return false;
        }

        public void InitPlayers(IPlayer player1, IPlayer player2)
        {
            P1 = player1;
            P2 = player2;
            currentPiece = Piece.White;
            for (int i = 0; i <= 2; i++)
            {
                Pieces[0, i] = Piece.White;
                Pieces[2, i] = Piece.Black;
            }
        }

        public bool ExecuteAction(AvailableActions action) =>
            action.Action switch
            {
                Actions.Forward => MoveForward(action.FromY, action.FromX, currentPiece),
                Actions.AttackLeft => AttackLeft(action.FromY, action.FromX, currentPiece),
                Actions.AttackRight => AttackRight(action.FromY, action.FromX, currentPiece),
                _ => false
            };

        public bool MakeAction(List<AvailableActions> actions, int index)
        {
            var action = actions.ElementAt(index);
            ExecuteAction(action);
            CopyBoards.Copy(this);
            if (CheckMovedWinner(currentPiece))
            {
                if (currentPiece == Piece.White)
                {
                    P1.TimesWon++;
                }
                else
                {
                    P2.TimesWon++;
                }
                return true;
            }
            ChangeCurrentPlayer();
            return false;
        }

        private bool CanMoveForward(int y, int x, Piece player)
        {
            return Pieces[y + ForwardDirection(player), x] == Piece.Empty;
        }
        public int ForwardDirection(Piece player)
        {
            return player == Piece.White ? 1 : -1;
        }
        public bool MoveForward(int y, int x, Piece player)
        {
            if (Pieces[y, x] == player)
            {
                if (CanMoveForward(y, x, player))
                {
                    Pieces[y + ForwardDirection(player), x] = player;
                    Pieces[y, x] = Piece.Empty;
                    return true;
                }
            }
            return false;
        }

        public void ChangeCurrentPlayer()
        {
            currentPiece = currentPiece == Piece.White ? Piece.Black : Piece.White;
        }

        private bool CanAttackLeft(int y, int x, Piece player)
        {
            if ((x >= 1 && x <= BoardSizeColumns - 1))
            {
                return Pieces[y + ForwardDirection(player), x - 1] != Piece.Empty &&
                Pieces[y + ForwardDirection(player), x - 1] != player;
            }
            return false;
        }

        private bool CanAttackRight(int y, int x, Piece player)
        {
            if ((x >= 0 && x <= BoardSizeColumns - 2))
            {
                return Pieces[y + ForwardDirection(player), x + 1] != Piece.Empty &&
                Pieces[y + ForwardDirection(player), x + 1] != player;
            }
            return false;
        }

        public bool AttackLeft(int y, int x, Piece player)
        {
            if (Pieces[y, x] == player)
            {
                if (CanAttackLeft(y, x, player))
                {
                    Pieces[y + ForwardDirection(player), x - 1] = player;
                    Pieces[y, x] = Piece.Empty;
                    return true;
                }
            }
            return false;
        }

        public bool AttackRight(int y, int x, Piece player)
        {
            if (Pieces[y, x] == player)
            {
                if (CanAttackRight(y, x, player))
                {
                    Pieces[y + ForwardDirection(player), x + 1] = player;
                    Pieces[y, x] = Piece.Empty;
                    return true;
                }
            }
            return false;
        }

        public bool CheckAvailableActions(List<AvailableActions> actions)
        {
            if (!actions.Any())
            {
                ChangeCurrentPlayer();
                if (currentPiece == Piece.White)
                {
                    P1.TimesWon++;
                }
                else
                {
                    P2.TimesWon++;
                }
                return false;
            }
            return true;
        }

        public bool CheckMovedWinner(Piece piece)
        {
            if (piece == Piece.White)
            {
                if (Pieces[2, 0] == piece) return true;
                if (Pieces[2, 1] == piece) return true;
                if (Pieces[2, 2] == piece) return true;
            }
            else
            {
                if (Pieces[0, 0] == piece) return true;
                if (Pieces[0, 1] == piece) return true;
                if (Pieces[0, 2] == piece) return true;
            }
            return false;
        }

        public List<AvailableActions> GetAllPlayerAvailableActions(Piece player)
        {
            var moveDirections = new List<AvailableActions>();
            for (int y = 0; y <= BoardSizeRows - 1; y++)
            {
                for (int x = 0; x <= BoardSizeColumns - 1; x++)
                {
                    if (Pieces[y, x] == player)
                    {
                        if (CanMoveForward(y, x, player)) moveDirections.Add(new AvailableActions(y, x, y + ForwardDirection(player), x, Actions.Forward));
                        if (CanAttackLeft(y, x, player)) moveDirections.Add(new AvailableActions(y, x, y + ForwardDirection(player), x - 1, Actions.AttackLeft));
                        if (CanAttackRight(y, x, player)) moveDirections.Add(new AvailableActions(y, x, y + ForwardDirection(player), x + 1, Actions.AttackRight));
                    }
                }
            }
            return moveDirections;
        }

        public void AiLearn()
        {

        }

        public bool MakeAction(List<AvailableActions> actions)
        {
            var rnd = new Random();
            if (actions.Any())
            {
                var index = rnd.Next(0, actions.Count() - 1);
                return MakeAction(actions, index);

            }
            return false;
        }
    }

    public enum Actions { Forward, AttackLeft, AttackRight }
    public class AvailableActions
    {
        public int FromX, FromY, ToX, ToY;
        public AvailableActions(int fromY, int fromX, int toY, int toX, Actions action)
        {
            FromX = fromX;
            FromY = fromY;
            ToX = toX;
            Action = action;
            ToY = toY;
        }

        public Actions Action { get; }
    }

    public class Game
    {
        public List<AvailableActions> GenerateBoard(Board board)
        {
            for (int y = 0; y <= 2; y++)
            {
                for (int x = 0; x <= 2; x++)
                {
                    if (board.Pieces[y, x] == Piece.Black) Console.Write("[2]");
                    if (board.Pieces[y, x] == Piece.White) Console.Write("[1]");
                    if (board.Pieces[y, x] == Piece.Empty) Console.Write("[ ]");
                }
                Console.WriteLine("");
            }
            var actions = board.GetAllPlayerAvailableActions(board.currentPiece);

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
