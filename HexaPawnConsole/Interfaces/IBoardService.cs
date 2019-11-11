using System.Collections.Generic;

namespace HexaPawnConsole
{
    public interface IBoardService
    {
        Color[,] Pieces { get; set; }
        IPlayer CurrentPlayer { get; set; }
        IPlayer P1 { get; set; }
        IPlayer P2 { get; set; }
        void AiLearn();
        void ChangeCurrentPlayer();
        bool CheckIfCurrentPlayerIsAI();
        bool ExecuteAction(AvailableAction action);
        List<AvailableAction> GetAllPlayerAvailableActions(Color color);
        void InitPlayers(bool playerOne, bool playerTwo);
        void InitPieces();
        bool MakeRandomAction(List<AvailableAction> actions);
        bool MakeAction(List<AvailableAction> actions, int index);
        void ResetBoard();
    }
}