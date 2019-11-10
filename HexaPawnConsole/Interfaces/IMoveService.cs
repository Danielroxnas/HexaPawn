namespace HexaPawnConsole
{
    public interface IMoveService
    {
        bool AttackLeft(int y, int x, Color color, Color[,] Pieces);
        bool AttackRight(int y, int x, Color color, Color[,] Pieces);
        bool CanAttackLeft(int y, int x, Color color, Color[,] Pieces);
        bool CanAttackRight(int y, int x, Color color, Color[,] Pieces);
        bool CanMoveForward(int y, int x, Color color, Color[,] Pieces);
        int ForwardDirection(Color color);
        bool MoveForward(int y, int x, Color color, Color[,] Pieces);
    }
}