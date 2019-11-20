using System.Collections.Generic;

namespace HexaPawnConsole
{
    public interface IBoardService
    {
        Dictionary<string, Color> Pieces { get; set; }
        Player CurrentPlayer { get; set; }
        Player P1 { get; set; }
        Player P2 { get; set; }
        void AiLearn();
        void ChangeCurrentPlayer();
        bool CheckIfCurrentPlayerIsAI();
        bool ExecuteAction(AvailableAction action);
        List<AvailableAction> GetAllPlayerAvailableActions1(Color color);
        void InitPlayers(bool playerOne, bool playerTwo);
        void InitPieces();
        bool MakeRandomAction(List<AvailableAction> actions);
        bool MakeAction(List<AvailableAction> actions, int index);
        void ResetBoard();
    }
}