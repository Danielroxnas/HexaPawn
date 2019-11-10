using System;
using System.Collections.Generic;
using System.Linq;

namespace HexaPawnConsole
{
    public class BoardService : IBoardService
    {
        public Color[,] Pieces { get; set; }
        public IPlayer CurrentPlayer { get; set; }
        private IPlayer P1;
        private IPlayer P2;
        private readonly int BoardSizeRows;
        private readonly int BoardSizeColumns;
        private readonly IBoardState _boardState;
        private readonly IMoveService _moveService;
        private readonly List<AvailableActions> removedActions = new List<AvailableActions>();

        public BoardService(BoardService board, IBoardState boardState, IMoveService moveService)
            :this(boardState, moveService)
        {
            for (int y = 0; y <= 2; y++)
            {
                for (int x = 0; x <= 2; x++)
                {
                    Pieces[y, x] = board.Pieces[y, x];
                }
            }
        }

        public BoardService(IBoardState boardState, IMoveService moveService)
        {
            _boardState = boardState;
            _moveService = moveService;
            Pieces = new Color[3, 3];
        }

        public void ResetBoard()
        {
            Pieces = new Color[BoardSizeColumns, BoardSizeRows];
            CurrentPlayer = P1;
            for (int i = 0; i <= 2; i++)
            {
                Pieces[0, i] = Color.Black;
                Pieces[2, i] = Color.White;
            }
        }
        public void InitPlayers(bool playerOne, bool playerTwo)
        {
            P1 = playerOne == true ? new Human(Color.White, _boardState) : new AI(Color.White, _boardState) as IPlayer;
            P2 = playerTwo == true ? new Human(Color.Black, _boardState) : new AI(Color.Black, _boardState) as IPlayer;
            CurrentPlayer = P1;
            for (int i = 0; i <= 2; i++)
            {
                Pieces[0, i] = Color.Black;
                Pieces[2, i] = Color.White;
            }
        }
        public bool CheckIfCurrentIsAI()
        {
            if (CurrentPlayer == P1 && P1.GetType() == typeof(AI) ||
                CurrentPlayer == P2 && P2.GetType() == typeof(AI))
            {
                return true;
            }
            else if (
                CurrentPlayer == P1 && P1.GetType() == typeof(Human) ||
                CurrentPlayer == P2 && P2.GetType() == typeof(Human))
            {
                return false;
            }
            return false;
        }

        public bool ExecuteAction(AvailableActions action) =>
            action.Action switch
            {
                Actions.Forward => _moveService.MoveForward(action.FromY, action.FromX, CurrentPlayer.Color, Pieces),
                Actions.AttackLeft => _moveService.AttackLeft(action.FromY, action.FromX, CurrentPlayer.Color, Pieces),
                Actions.AttackRight => _moveService.AttackRight(action.FromY, action.FromX, CurrentPlayer.Color, Pieces),
                _ => false
            };

        public void RegisterMove(AvailableActions availableActions, IPlayer player)
        {
            player.LastAvailableActions = availableActions;
        }

        public bool MakeAction(List<AvailableActions> actions, int index)
        {
            var action = actions.ElementAt(index);
            CurrentPlayer.LastState(this);
            ExecuteAction(action);
            RegisterMove(action, CurrentPlayer);
            _boardState.Copy(this);
            if (CheckMovedWinner(CurrentPlayer.Color))
            {
                if (!CheckIfCurrentIsAI())
                {
                    AiLearn();
                }
                CurrentPlayer.TimesWon++;
                return true;
            }
            ChangeCurrentPlayer();
            return false;
        }


        public void ChangeCurrentPlayer()
        {
            CurrentPlayer = CurrentPlayer == P1 ? P2 : P1;
        }


        public bool CheckAvailableActions(List<AvailableActions> actions)
        {
            if (!actions.Any())
            {
                ChangeCurrentPlayer();
                CurrentPlayer.TimesWon++;
                return false;
            }
            return true;
        }

        public bool CheckMovedWinner(Color color)
        {

            if (color == Color.White)
            {
                if (!GetAllPlayerAvailableActions(Color.Black).Any()) return true;
                if (Pieces[0, 0] == color) return true;
                if (Pieces[0, 1] == color) return true;
                if (Pieces[0, 2] == color) return true;
            }
            else
            {
                if (!GetAllPlayerAvailableActions(Color.White).Any()) return true;

                if (Pieces[2, 0] == color) return true;
                if (Pieces[2, 1] == color) return true;
                if (Pieces[2, 2] == color) return true;
            }
            return false;
        }

        public List<AvailableActions> GetAllPlayerAvailableActions(Color color)
        {
            var moveDirections = new List<AvailableActions>();
            for (int y = 0; y <= 3 - 1; y++)
            {
                for (int x = 0; x <= 3 - 1; x++)
                {
                    if (Pieces[y, x] == color)
                    {
                        if (_moveService.CanMoveForward(y, x, color, Pieces))
                        {
                            var availableActions = new AvailableActions(y, x, y + _moveService.ForwardDirection(color), x, Actions.Forward);
                            moveDirections.Add(availableActions);
                        }
                        if (_moveService.CanAttackLeft(y, x, color, Pieces))
                        {
                            var availableActions = new AvailableActions(y, x, y + _moveService.ForwardDirection(color), x - 1, Actions.AttackLeft);
                            moveDirections.Add(availableActions);

                        }
                        if (_moveService.CanAttackRight(y, x, color, Pieces))
                        {
                            var availableActions = new AvailableActions(y, x, y + _moveService.ForwardDirection(color), x + 1, Actions.AttackRight);

                            moveDirections.Add(availableActions);

                        }
                    }
                }
            }
            return CheckRemovedActions(moveDirections);
        }

        private List<AvailableActions> CheckRemovedActions(List<AvailableActions> moveDirections)
        {
            var fakeBoard = _boardState.GetBoardState(this);
            if (fakeBoard != null)
            {
                if (CheckIfCurrentIsAI())
                {
                    fakeBoard.removedActions.ForEach(x =>
                    {
                        var itemToRemove = moveDirections.Where(y => x.Equals(y)).FirstOrDefault();
                        moveDirections.Remove(itemToRemove);
                    });
                };
            }
            return moveDirections;
        }


        public void AiLearn()
        {

            ChangeCurrentPlayer();
            var fakeBoard = CurrentPlayer.LastBord;
            fakeBoard.removedActions.Add(CurrentPlayer.LastAvailableActions);

            ChangeCurrentPlayer();
        }

        public bool MakeRandomAction(List<AvailableActions> actions)
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
}
