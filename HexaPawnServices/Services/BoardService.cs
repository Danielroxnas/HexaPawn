﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HexaPawnService
{
    public class Pieces
    {

        int[,] data;

        public Pieces()
        {
            data = new int[3, 3];
        }

        public int this[int y,int x] {
            get { return data[y,x]; }
            set { data[y, x] = value; } }
    }
    public class ConcreteConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType) => true;

        public override object ReadJson(JsonReader reader,
         Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<T>(reader);
        }

        public override void WriteJson(JsonWriter writer,
            object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
    public class BoardService : IBoardService
    {
        [JsonProperty]
        public Pieces Pieces { get; set; }

        //   [JsonProperty(ItemConverterType = typeof(ConcreteConverter<Player>))]
        [JsonProperty]
        public Player CurrentPlayer { get; set; }
        private readonly IBoardState _boardState;

        //[JsonProperty(ItemConverterType = typeof(ConcreteConverter<Player>))]
        public Player P1 { get; set; } = new Player(Color.White, null, true);
        //[JsonProperty(ItemConverterType = typeof(ConcreteConverter<Player>))]
        public Player P2 { get; set; } = new Player(Color.Black, null, true);
        
        private readonly IMoveService _moveService;
        private readonly List<AvailableAction> removedActions = new List<AvailableAction>();

        public BoardService()
        {

        }
        public BoardService(BoardService board, IBoardState boardState, IMoveService moveService)
            : this(boardState, moveService)
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
            Pieces = new Pieces();
           // Pieces = new Color[3, 3];
        }

        public void ResetBoard()
        {
            Pieces = new Pieces();

            //Pieces = new Color[3, 3];
            CurrentPlayer = P1;
            for (int i = 0; i <= 2; i++)
            {
                Pieces[0, i] = 2;
                Pieces[2, i] = 1;
            }
        }
        public void InitPieces()
        {
            for (int i = 0; i <= 2; i++)
            {
                Pieces[0, i] = 2;
                Pieces[2, i] = 1;
            }
        }
        public void InitPlayers(bool playerOne, bool playerTwo)
        {
            P1 = new Player(Color.White, _boardState, playerOne);
            P2 = new Player(Color.Black, _boardState, playerTwo);
            CurrentPlayer = P1;
        }

        public bool CheckIfCurrentPlayerIsAI()
        {
            if (CurrentPlayer == P1 && P1.GetType() == typeof(AI) ||
                CurrentPlayer == P2 && P2.GetType() == typeof(AI))
            {
                return true;
            }
            return false;
        }

        public bool ExecuteAction(AvailableAction action) =>
            action.Action switch
            {
                Actions.Forward => _moveService.MoveForward(action.FromY, action.FromX, (int)CurrentPlayer.Color, Pieces),
                Actions.AttackLeft => _moveService.AttackLeft(action.FromY, action.FromX, (int)CurrentPlayer.Color, Pieces),
                Actions.AttackRight => _moveService.AttackRight(action.FromY, action.FromX, (int)CurrentPlayer.Color, Pieces),
                _ => false
            };

        private void RegisterAction(AvailableAction availableActions) => CurrentPlayer.LastAvailableActions = availableActions;

        public bool MakeAction(AvailableAction action)
        {
            return Execute(action);
        }
        public bool MakeAction(List<AvailableAction> actions, int index)
        {

            var action = actions.ElementAt(index);
            return Execute(action);
        }

        private bool Execute(AvailableAction action)
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
                if (Pieces[0, 0] == 1) return true;
                if (Pieces[0, 1] == 1) return true;
                if (Pieces[0, 2] == 1) return true;
                if (!GetAllPlayerAvailableActions(Color.Black).Any()) return true;
            }
            else
            {
                if (Pieces[2, 0] == 2) return true;
                if (Pieces[2, 1] == 2) return true;
                if (Pieces[2, 2] == 2) return true;
                if (!GetAllPlayerAvailableActions(Color.White).Any()) return true;
            }
            return false;
        }

        public List<AvailableAction> GetAllPlayerAvailableActions(BoardService boardService)
        {
            return GetAllPlayerAvailableActions(boardService.CurrentPlayer.Color);
        }
        public List<AvailableAction> GetAllPlayerAvailableActions(Color color)
        {
            var availableActions = new List<AvailableAction>();
            for (int y = 0; y <= 3 - 1; y++)
            {
                for (int x = 0; x <= 3 - 1; x++)
                {
                    if (Pieces[y, x] == (int)color)
                    {
                        if (_moveService.CanMoveForward(y, x, (int)color, Pieces))
                        {
                            availableActions.Add(new AvailableAction(y, x, y + _moveService.ForwardDirection((int)color), x, Actions.Forward));
                        }
                        if (_moveService.CanAttackLeft(y, x, (int)color, Pieces))
                        {
                            availableActions.Add(new AvailableAction(y, x, y + _moveService.ForwardDirection((int)color), x - 1, Actions.AttackLeft));

                        }
                        if (_moveService.CanAttackRight(y, x, (int)color, Pieces))
                        {
                            availableActions.Add(new AvailableAction(y, x, y + _moveService.ForwardDirection((int)color), x + 1, Actions.AttackRight));

                        }
                    }
                }
            }
            return RemoveAiActions(availableActions);
        }

        private List<AvailableAction> RemoveAiActions(List<AvailableAction> availableActions)
        {
            if (CheckIfCurrentPlayerIsAI())
            {
                var boardState = _boardState.GetBoardState(this);
                if (boardState != null)
                {
                    boardState.removedActions.ForEach(x =>
                    {
                        var itemToRemove = availableActions.Where(y => x.Equals(y)).FirstOrDefault();
                        availableActions.Remove(itemToRemove);
                    });
                }
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
