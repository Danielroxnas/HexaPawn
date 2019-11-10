using System.Collections.Generic;

namespace HexaPawnConsole
{
    public interface IBoardService
    {
        Color[,] Pieces { get; set; }
        IPlayer CurrentPlayer { get; set; }
        void AiLearn();
        void ChangeCurrentPlayer();
        bool CheckAvailableActions(List<AvailableActions> actions);
        bool CheckIfCurrentIsAI();
        bool CheckMovedWinner(Color color);
        bool ExecuteAction(AvailableActions action);
        List<AvailableActions> GetAllPlayerAvailableActions(Color color);
        void InitPlayers(bool playerOne, bool playerTwo);
        bool MakeRandomAction(List<AvailableActions> actions);
        bool MakeAction(List<AvailableActions> actions, int index);
        void ResetBoard();
        void RegisterMove(AvailableActions availableActions, IPlayer player);

    }
}