using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HexaPawnConsole
{
    public class BoardService : IBoardService
    {
        //  public Pieces Pieces { get; set; }
        [JsonProperty]
        public Dictionary<string, Color> Pieces { get; set; }

        [JsonProperty]
        public Player CurrentPlayer { get; set; }

        private readonly IBoardState _boardState;

        [JsonProperty]
        public Player P1 { get; set; }

        [JsonProperty]
        public Player P2 { get; set; }
        private readonly IMoveService _moveService;
        private readonly List<AvailableAction> removedActions = new List<AvailableAction>();

        public BoardService()
        {

        }
        public BoardService(BoardService board, IBoardState boardState, IMoveService moveService)
            : this(boardState, moveService)
        {

            Pieces = board.Pieces.ToDictionary(x => x.Key, x => x.Value);

        }



        public void GenerateBoard()
        {

        }
        public BoardService(IBoardState boardState, IMoveService moveService)
        {
            Pieces = new Dictionary<string, Color>();

            _boardState = boardState;
            _moveService = moveService;
        }

        public void ResetBoard()
        {
            CurrentPlayer = P1;
            Pieces = new Dictionary<string, Color>();
            Enumerable.Range(1, 3).ToList().ForEach(x => Pieces.Add($"A{x}", Color.Black));
            Enumerable.Range(1, 3).ToList().ForEach(x => Pieces.Add($"B{x}", Color.Empty));
            Enumerable.Range(1, 3).ToList().ForEach(x => Pieces.Add($"C{x}", Color.White));
        }
        public void InitPieces()
        {
            Enumerable.Range(1, 3).ToList().ForEach(x => Pieces.Add($"A{x}", Color.Black));
            Enumerable.Range(1, 3).ToList().ForEach(x => Pieces.Add($"B{x}", Color.Empty));
            Enumerable.Range(1, 3).ToList().ForEach(x => Pieces.Add($"C{x}", Color.White));
        }
        public void InitPlayers(bool playerOne, bool playerTwo)
        {
            P1 = new Player(Color.White, _boardState, playerOne);
            P2 = new Player(Color.Black, _boardState, playerTwo);
            CurrentPlayer = P1;
        }

        public bool CheckIfCurrentPlayerIsAI()
        {
            if (CurrentPlayer.Human == false)
            {
                return true;
            }
            return false;
        }

        public bool ExecuteAction(AvailableAction action) =>
            action.Action switch
            {

                Actions.Forward => _moveService.MoveForward(action.Key, action.Color, Pieces),
                Actions.AttackLeft => _moveService.AttackLeft(action.Key, action.Color, Pieces),
                Actions.AttackRight => _moveService.AttackRight(action.Key, action.Color, Pieces),
                _ => false
            };

        private void RegisterAction(AvailableAction availableActions) => CurrentPlayer.LastAvailableActions = availableActions;

        public bool MakeAction(List<AvailableAction> actions, int index)
        {

            var action = actions.ElementAt(index);
            return ActionExecute(action);
        }

        private bool ActionExecute(AvailableAction action)
        {
            CurrentPlayer.LastState(this);
            if (!ExecuteAction(action))
            {
                return false;
            }
            RegisterAction(action);
            _boardState.Copy(this);
            if (CheckMovedWinner(CurrentPlayer))
            {
                if (!CheckIfCurrentPlayerIsAI())
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

        private bool CheckMovedWinner(IPlayer player)
        {
            if (player.Color == Color.White)
            {
                if (Pieces.Any(x => Regex.IsMatch(x.Key, @"A") && x.Value == Color.White)) return true;

                if (!GetAllPlayerAvailableActions1(Color.Black).Any()) return true;
            }
            else
            {
                if (Pieces.Any(x => Regex.IsMatch(x.Key, @"C") && x.Value == Color.Black)) return true;

                if (!GetAllPlayerAvailableActions1(Color.White).Any()) return true;
            }
            return false;
        }
        public List<AvailableAction> GetAllPlayerAvailableActions(BoardService board)
        {
            return GetAllPlayerAvailableActions1(board.CurrentPlayer.Color);
        }
        public List<AvailableAction> GetAllPlayerAvailableActions1(Color color)
        {

            var p = Pieces.Where(x => x.Value == color).ToList();

            var availableActions = new List<AvailableAction>();

            p.ForEach(x =>
            {

                if (_moveService.CanMoveForward(x.Key, x.Value, Pieces)) availableActions.Add(new AvailableAction(x.Key, x.Value, Actions.Forward));
                if (_moveService.CanAttackLeft(x.Key, x.Value, Pieces)) availableActions.Add(new AvailableAction(x.Key, x.Value, Actions.AttackLeft));
                if (_moveService.CanAttackRight(x.Key, x.Value, Pieces)) availableActions.Add(new AvailableAction(x.Key, x.Value, Actions.AttackRight));

            });

            return RemoveAiActions(availableActions);
        }

        private List<AvailableAction> RemoveAiActions(List<AvailableAction> availableActions)
        {
            if (!CheckIfCurrentPlayerIsAI())
            { return availableActions; }
            var boardState = _boardState.GetBoardState(this);
            if (boardState != null)
            {
                boardState.removedActions.ForEach(x =>
                {
                    var itemToRemove = availableActions.Where(y => x.Equals(y)).FirstOrDefault();
                    availableActions.Remove(itemToRemove);
                });
            }

            return availableActions;
        }


        public void AiLearn()
        {
            ChangeCurrentPlayer();
            var boardState = CurrentPlayer.LastBord;
            boardState.removedActions.Add(CurrentPlayer.LastAvailableActions);

            ChangeCurrentPlayer();
        }
        public bool MakeAction(AvailableAction availableAction)
        {
            return ActionExecute(availableAction);
        }
        public bool MakeRandomAction(List<AvailableAction> actions)
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
