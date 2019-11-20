using HexaPawnConsole11;
using System.Collections.Generic;

namespace HexaPawnConsole1
{
    public interface IBoardService
    {
   //     Pieces Pieces { get; set; }

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