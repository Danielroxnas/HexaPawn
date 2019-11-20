using HexaPawnConsole11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HexaPawnConsole1
{
    public class Board2State2
    {
        public Board2State2()
        {
            Board2s = new List<Board2>();
        }
        public List<Board2> Board2s { get; set; }
    }


    public class Board2
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public Dictionary<double, int> Squares { get; set; }
        public List<Action2> RemovedAction { get; set; }
    }

    public class Player2
    {
        public Player2(bool human, int color)
        {
            Human = human;
            Color = color;
        }

        public bool Human { get; set; }
        public int Color { get; set; }
        public Guid Id { get; set; }
        public Action2 LastAction { get; set; }
    }

    public class Action2
    {
        public Board2 Board { get; set; }
        public int Action { get; set; }
        public double FromSquare { get; set; }
        public double ToSquare { get; set; }
        public Player2 Player { get; set; }
    }


    public class Game2
    {
        public Player2 P1 { get; set; }
        public Player2 P2 { get; set; }
        public Player2 CurrentPlayer { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
    }

    public class MoveActions
    {
        public int Direction(Player2 player)
        {
            return player.Color == 1 ? -1 : 1;
        }
        public bool CanMoveForward(double fromSquare, Player2 player, Board2 board2)
        {
            return board2.Squares[fromSquare + Direction(player)] == 0;
        }
        public bool MoveForward(double fromSquare, Player2 player, Board2 board2)
        {

            throw new NotImplementedException();
        }
    }



    public class GameService
    {
        private readonly MoveActions _moveActions;
        private Game2 _game2;
        private Board2State2 _boards;
        public GameService(MoveActions moveActions)
        {
            _moveActions = moveActions;
        }

        public void Initplayer(bool p1, bool p2)
        {
            _game2.P1 = new Player2(p1, 1);
            _game2.P2 = new Player2(p2, 2);
            _game2.CurrentPlayer = _game2.P1;
        }
        public void InitBoard()
        {
            _boards = new Board2State2();
            var b = new Board2
            {
                Squares = new Dictionary<double, int> {
                    { 1.1, 2 },{ 1.2, 2 },{ 1.3, 2 },
                    { 2.1, 0 },{ 2.2, 0 },{ 2.3, 0 },
                    { 3.1, 1 },{ 3.2, 1 },{ 3.3, 1 } }
            };
            _boards.Board2s.Add(b);
        }
        public bool CheckIfCurrentPlayerIsAI()
        {
            if (_game2.CurrentPlayer.Human == false)
            {
                return true;
            }
            return false;
        }
        private void RegisterAction(Action2 action) => _game2.CurrentPlayer.LastAction = action;
        public bool ExecuteAction(Action2 action) =>
   
            action.Action switch
            {
                (int)Actions.Forward => _moveActions.MoveForward(action.FromSquare, action.Player, _boards.Board2s.Last()),
                //(int)Actions.AttackLeft => _moveService.AttackLeft(action.Key, action.Color, _board.Pieces1),
                //(int)Actions.AttackRight => _moveService.AttackRight(action.Key, action.Color, _board.Pieces1),
                _ => false
            };
        //private bool ActionExecute(Action2 action)
        //{
        //    _game2.CurrentPlayer.LastAction = action;

        //}
    }


















    public class Board : IBoard
    {
        public Guid Id { get; set; }
        public Dictionary<string, Color> Pieces1 { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<AvailableAction1> RemovedActions { get; set; }

        public Player P1 { get; set; }
        public Player P2 { get; set; }
    }

    public class BoardService : IBoardService
    {
        private readonly IBoardState _boardState;
        private readonly IMovService _moveService;
        private readonly IBoard _board;

        public BoardService()
        {

        }
        public BoardService(IBoard board, IBoardState boardState, IMovService moveService)
        {
            _board = board;
            _boardState = boardState;
            _moveService = moveService;
        }
        public void GenerateBoard()
        {

        }
        public void ResetBoard()
        {
            _board.CurrentPlayer = _board.P1;
            Enumerable.Range(1, 3).ToList().ForEach(x => _board.Pieces1.Add($"A{x}", Color.Black));
            Enumerable.Range(1, 3).ToList().ForEach(x => _board.Pieces1.Add($"B{x}", Color.Empty));
            Enumerable.Range(1, 3).ToList().ForEach(x => _board.Pieces1.Add($"C{x}", Color.White));
        }
        public void InitPieces()
        {
            Enumerable.Range(1, 3).ToList().ForEach(x => _board.Pieces1.Add($"A{x}", Color.Black));
            Enumerable.Range(1, 3).ToList().ForEach(x => _board.Pieces1.Add($"B{x}", Color.Empty));
            Enumerable.Range(1, 3).ToList().ForEach(x => _board.Pieces1.Add($"C{x}", Color.White));
        }
        public void InitPlayers(bool playerOne, bool playerTwo)
        {
            _board.P1 = new Player(Color.White, _boardState, playerOne);
            _board.P2 = new Player(Color.Black, _boardState, playerTwo);
            _board.CurrentPlayer = _board.P1;
        }

        public bool CheckIfCurrentPlayerIsAI()
        {
            if (_board.CurrentPlayer.Human == false)
            {
                return true;
            }
            return false;
        }

        public bool ExecuteAction(AvailableAction1 action) =>
            action.Action switch
            {
                Actions.Forward => _moveService.MoveForward(action.Key, action.Color, _board.Pieces1),
                Actions.AttackLeft => _moveService.AttackLeft(action.Key, action.Color, _board.Pieces1),
                Actions.AttackRight => _moveService.AttackRight(action.Key, action.Color, _board.Pieces1),
                _ => false
            };

        private void RegisterAction(AvailableAction1 availableActions) => _board.CurrentPlayer.LastAvailableActions = availableActions;

        public bool MakeAction(List<AvailableAction1> actions, int index)
        {

            var action = actions.ElementAt(index);
            return ActionExecute(action);
        }

        private bool ActionExecute(AvailableAction1 action)
        {
            _board.CurrentPlayer.LastState(_board); //game.currentPlayer
            if (!ExecuteAction(action))
            {
                return false;
            }
            RegisterAction(action);
            _boardState.Copy(_board);
            if (CheckMovedWinner(_board.CurrentPlayer))
            {
                if (!CheckIfCurrentPlayerIsAI())
                {
                    AiLearn();
                }
                _board.CurrentPlayer.TimesWon++;
                return true;
            }
            ChangeCurrentPlayer();
            return false;
        }

        public void ChangeCurrentPlayer()
        {
            _board.CurrentPlayer = _board.CurrentPlayer == _board.P1 ? _board.P2 : _board.P1;
        }

        private bool CheckMovedWinner(IPlayer player)
        {
            if (player.Color == Color.White)
            {
                if (_board.Pieces1.Any(x => Regex.IsMatch(x.Key, @"A") && x.Value == Color.White)) return true;
                if (!GetAllPlayerAvailableActions1(Color.Black).Any()) return true;
            }
            else
            {
                if (_board.Pieces1.Any(x => Regex.IsMatch(x.Key, @"C") && x.Value == Color.Black)) return true;
                if (!GetAllPlayerAvailableActions1(Color.White).Any()) return true;
            }
            return false;
        }
        public List<AvailableAction1> GetAllPlayerAvailableActions()
        {
            return GetAllPlayerAvailableActions1(_board.CurrentPlayer.Color);
        }
        public List<AvailableAction1> GetAllPlayerAvailableActions1(Color color)
        {

            var p = _board.Pieces1.Where(x => x.Value == color).ToList();

            var availableActions = new List<AvailableAction1>();

            p.ForEach(x =>
            {
                if (_moveService.CanMoveForward(x.Key, x.Value, _board.Pieces1)) availableActions.Add(new AvailableAction1(x.Key, x.Value, Actions.Forward));
                if (_moveService.CanAttackLeft(x.Key, x.Value, _board.Pieces1)) availableActions.Add(new AvailableAction1(x.Key, x.Value, Actions.AttackLeft));
                if (_moveService.CanAttackRight(x.Key, x.Value, _board.Pieces1)) availableActions.Add(new AvailableAction1(x.Key, x.Value, Actions.AttackRight));

            });

            return RemoveAiActions(availableActions);
        }

        private List<AvailableAction1> RemoveAiActions(List<AvailableAction1> availableActions)
        {
            if (!CheckIfCurrentPlayerIsAI())
            { return availableActions; }
            var boardState = _boardState.GetBoardState(_board);
            if (boardState != null)
            {
                boardState.RemovedActions.ForEach(x =>
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
            var boardState = _board.CurrentPlayer.LastBord;
            boardState.RemovedActions.Add(_board.CurrentPlayer.LastAvailableActions);

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
