namespace HexaPawnConsole
{
    public interface IMoveService
    {
        bool AttackLeft(int y, int x, int color, Pieces Pieces);
        bool AttackRight(int y, int x, int color, Pieces Pieces);
        bool CanAttackLeft(int y, int x, int color, Pieces Pieces);
        bool CanAttackRight(int y, int x, int color, Pieces Pieces);
        bool CanMoveForward(int y, int x, int color, Pieces Pieces);
        int ForwardDirection(int color);
        bool MoveForward(int y, int x, int color, Pieces Pieces);
    }
}