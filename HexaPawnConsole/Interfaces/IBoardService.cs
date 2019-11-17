using System.Collections.Generic;

namespace HexaPawnConsole
{
    public interface IBoardService
    {
   //     Pieces Pieces { get; set; }
        Dictionary<string, int> Pieces1 { get; set; }
        Player CurrentPlayer { get; set; }
        Player P1 { get; set; }
        Player P2 { get; set; }
        void AiLearn();
        void ChangeCurrentPlayer();
        bool CheckIfCurrentPlayerIsAI();
        bool ExecuteAction(AvailableAction1 action);
        List<AvailableAction1> GetAllPlayerAvailableActions1(Color color);
        void InitPlayers(bool playerOne, bool playerTwo);
        void InitPieces();
        bool MakeRandomAction(List<AvailableAction1> actions);
        bool MakeAction(List<AvailableAction1> actions, int index);
        void ResetBoard();
    }
}