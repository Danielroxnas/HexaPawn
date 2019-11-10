using System.Collections.Generic;

namespace HexaPawnConsole
{
    public interface IBoardService
    {
        int BoardSizeColumns { get; }
        int BoardSizeRows { get; }

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
        bool MakeAction(List<AvailableActions> actions);
        bool MakeAction(List<AvailableActions> actions, int index);
        void ResetBoard();
    }
}