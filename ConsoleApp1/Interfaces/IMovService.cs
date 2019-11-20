using System.Collections.Generic;

namespace HexaPawnConsole1
{
    public interface IMovService
    {
        bool AttackLeft(string key, Color color, Dictionary<string, Color> pieces);
        bool AttackRight(string key, Color color, Dictionary<string, Color> pieces);
        bool CanAttackLeft(string key, Color color, Dictionary<string, Color> pieces);
        bool CanAttackRight(string key, Color color, Dictionary<string, Color> pieces);
        bool CanMoveForward(string key, Color color, Dictionary<string, Color> pieces);
        bool MoveForward(string key, Color color, Dictionary<string, Color> pieces);
    }
}