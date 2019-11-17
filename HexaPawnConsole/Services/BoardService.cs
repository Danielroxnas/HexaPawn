using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HexaPawnConsole
{
    public class Pieces
    {

        int[,] data = new int[3, 3];

        public int this[int y, int x]
        {
            get { return data[y, x]; }
            set { data[y, x] = value; }
        }
    }
    public class BoardService : IBoardService
    {
        //  public Pieces Pieces { get; set; }
        [JsonProperty]
        public Dictionary<string, int> Pieces1 { get; set; }

        [JsonProperty]
        public Player CurrentPlayer { get; set; }

        private readonly IBoardState _boardState;

        [JsonProperty]
        public Player P1 { get; set; }

        [JsonProperty]
        public Player P2 { get; set; }
        private readonly IMovService _moveService;
        private readonly List<AvailableAction1> removedActions = new List<AvailableAction1>();

        public BoardService()
        {

        }
        public BoardService(BoardService board, IBoardState boardState, IMovService moveService)
            : this(boardState, moveService)
        {

            Pieces1 = board.Pieces1.ToDictionary(x => x.Key, x => x.Value);
            //for (int y = 0; y <= 2; y++)
            //{
            //    for (int x = 0; x <= 2; x++)
            //    {
            //        Pieces[y, x] = board.Pieces[y, x];
            //    }
            //}

        }



        public void GenerateBoard()
        {

        }
        public BoardService(IBoardState boardState, IMovService moveService)
        {
            Pieces1 = new Dictionary<string, int>();

            _boardState = boardState;
            _moveService = moveService;
            //   Pieces = new Pieces();
            // Pieces = new Color[3, 3];
        }

        public void ResetBoard()
        {
            //            Pieces = new Pieces();

            //Pieces = new Color[3, 3];
            CurrentPlayer = P1;
            //for (int i = 0; i <= 2; i++)
            //{
            //    Pieces[0, i] = 2;
            //    Pieces[2, i] = 1;
            //}
            Pieces1 = new Dictionary<string, int>();
            Enumerable.Range(1, 3).ToList().ForEach(x => Pieces1.Add($"A{x}", 2));
            Enumerable.Range(1, 3).ToList().ForEach(x => Pieces1.Add($"B{x}", 0));
            Enumerable.Range(1, 3).ToList().ForEach(x => Pieces1.Add($"C{x}", 1));
        }
        public void InitPieces()
        {
            Enumerable.Range(1, 3).ToList().ForEach(x => Pieces1.Add($"A{x}", 2));
            Enumerable.Range(1, 3).ToList().ForEach(x => Pieces1.Add($"B{x}", 0));
            Enumerable.Range(1, 3).ToList().ForEach(x => Pieces1.Add($"C{x}", 1));
            //for (int i = 0; i <= 2; i++)
            //{
            //    Pieces[0, i] = 2;
            //    Pieces[2, i] = 1;
            //}
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

        public bool ExecuteAction(AvailableAction1 action) =>
            action.Action switch
            {

                Actions.Forward => _moveService.MoveForward(action.Key, action.Color, Pieces1),
                Actions.AttackLeft => _moveService.AttackLeft(action.Key, action.Color, Pieces1),
                Actions.AttackRight => _moveService.AttackRight(action.Key, action.Color, Pieces1),

                //Actions.Forward => _moveService.MoveForward(action.FromY, action.FromX, (int)CurrentPlayer.Color, Pieces),
                //Actions.AttackLeft => _moveService.AttackLeft(action.FromY, action.FromX, (int)CurrentPlayer.Color, Pieces),
                //Actions.AttackRight => _moveService.AttackRight(action.FromY, action.FromX, (int)CurrentPlayer.Color, Pieces),
                _ => false
            };

        private void RegisterAction(AvailableAction1 availableActions) => CurrentPlayer.LastAvailableActions = availableActions;

        public bool MakeAction(List<AvailableAction1> actions, int index)
        {

            var action = actions.ElementAt(index);
            return ActionExecute(action);
        }

        private bool ActionExecute(AvailableAction1 action)
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
                if (Pieces1.Any(x => Regex.IsMatch(x.Key, @"A") && x.Value == 1)) return true;
                //if (Pieces[0, 0] == 1) return true;
                //if (Pieces[0, 1] == 1) return true;
                //if (Pieces[0, 2] == 1) return true;
                if (!GetAllPlayerAvailableActions1(Color.Black).Any()) return true;
            }
            else
            {
                if (Pieces1.Any(x => Regex.IsMatch(x.Key, @"C") && x.Value == 2)) return true;

                //if (Pieces[2, 0] == 2) return true;
                //if (Pieces[2, 1] == 2) return true;
                //if (Pieces[2, 2] == 2) return true;
                if (!GetAllPlayerAvailableActions1(Color.White).Any()) return true;
            }
            return false;
        }
        public List<AvailableAction1> GetAllPlayerAvailableActions(BoardService board)
        {
            return GetAllPlayerAvailableActions1(board.CurrentPlayer.Color);
        }
        public List<AvailableAction1> GetAllPlayerAvailableActions1(Color color)
        {

            var p = Pieces1.Where(x => x.Value == (int)color).ToList();

            var availableActions = new List<AvailableAction1>();

            p.ForEach(x =>
            {

                if (_moveService.CanMoveForward(x.Key, x.Value, Pieces1)) availableActions.Add(new AvailableAction1(x.Key, x.Value, Actions.Forward));
                if (_moveService.CanAttackLeft(x.Key, x.Value, Pieces1)) availableActions.Add(new AvailableAction1(x.Key, x.Value, Actions.AttackLeft));
                if (_moveService.CanAttackRight(x.Key, x.Value, Pieces1)) availableActions.Add(new AvailableAction1(x.Key, x.Value, Actions.AttackRight));

            });

            return RemoveAiActions(availableActions);



            //for (int y = 0; y <= 3 - 1; y++)
            //{
            //    for (int x = 0; x <= 3 - 1; x++)
            //    {
            //        if (Pieces[y, x] == (int)color)
            //        {
            //            if (_moveService.CanMoveForward(y, x, (int)color, Pieces))
            //            {
            //                availableActions.Add(new AvailableAction1(y, x, y + _moveService.ForwardDirection((int)color), x, Actions.Forward));
            //            }
            //            if (_moveService.CanAttackLeft(y, x, (int)color, Pieces))
            //            {
            //                availableActions.Add(new AvailableAction(y, x, y + _moveService.ForwardDirection((int)color), x - 1, Actions.AttackLeft));

            //            }
            //            if (_moveService.CanAttackRight(y, x, (int)color, Pieces))
            //            {
            //                availableActions.Add(new AvailableAction(y, x, y + _moveService.ForwardDirection((int)color), x + 1, Actions.AttackRight));

            //            }
            //        }
            //    }
            //}
            //return RemoveAiActions(availableActions);
        }

        private List<AvailableAction1> RemoveAiActions(List<AvailableAction1> availableActions)
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
        public bool MakeAction(AvailableAction1 availableAction)
        {
            return ActionExecute(availableAction);
        }
        public bool MakeRandomAction(List<AvailableAction1> actions)
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
