using System.Collections.Generic;

namespace HexaPawnConsole
{
    public interface IMovService
    {
        bool AttackLeft(string key, int color, Dictionary<string, int> pieces);
        bool AttackRight(string key, int color, Dictionary<string, int> pieces);
        bool CanAttackLeft(string key, int color, Dictionary<string, int> pieces);
        bool CanAttackRight(string key, int color, Dictionary<string, int> pieces);
        bool CanMoveForward(string key, int color, Dictionary<string, int> pieces);
        bool MoveForward(string key, int color, Dictionary<string, int> pieces);
    }
}