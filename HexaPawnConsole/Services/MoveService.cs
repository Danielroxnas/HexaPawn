namespace HexaPawnConsole
{
    public class MoveService : IMoveService
    {

        public bool CanAttackLeft(int y, int x, Color color, Color[,] Pieces)
        {
            if ((x >= 1 && x <= 2))
            {
                return Pieces[y + ForwardDirection(color), x - 1] != Color.Empty &&
                Pieces[y + ForwardDirection(color), x - 1] != color;
            }
            return false;
        }

        public bool CanAttackRight(int y, int x, Color color, Color[,] Pieces)
        {
            if ((x >= 0 && x <= 1))
            {
                return Pieces[y + ForwardDirection(color), x + 1] != Color.Empty &&
                Pieces[y + ForwardDirection(color), x + 1] != color;
            }
            return false;
        }
        public void RegisterMove(AvailableActions availableActions, IPlayer player)
        {
            player.LastAvailableActions = availableActions;
        }

        public bool AttackLeft(int y, int x, Color color, Color[,] Pieces, IPlayer player)
        {
            if (Pieces[y, x] == color)
            {
                if (CanAttackLeft(y, x, color, Pieces))
                {
                    Pieces[y + ForwardDirection(color), x - 1] = color;
                    Pieces[y, x] = Color.Empty;
                    RegisterMove(new AvailableActions(y, x, y + ForwardDirection(color), x - 1, Actions.AttackLeft), player);
                    return true;
                }
            }
            return false;
        }

        public bool AttackRight(int y, int x, Color color, Color[,] Pieces, IPlayer player)
        {
            if (Pieces[y, x] == color)
            {
                if (CanAttackRight(y, x, color, Pieces))
                {
                    Pieces[y + ForwardDirection(color), x + 1] = color;
                    Pieces[y, x] = Color.Empty;
                    RegisterMove(new AvailableActions(y, x, y + ForwardDirection(color), x + 1, Actions.AttackRight), player);

                    return true;
                }
            }
            return false;
        }

        public bool CanMoveForward(int y, int x, Color color, Color[,] Pieces)
        {
            return Pieces[y + ForwardDirection(color), x] == Color.Empty;
        }
        public int ForwardDirection(Color color)
        {
            return color == Color.White ? -1 : 1;
        }
        public bool MoveForward(int y, int x, Color color, Color[,] Pieces, IPlayer player)
        {
            if (Pieces[y, x] == color)
            {
                if (CanMoveForward(y, x, color, Pieces))
                {
                    Pieces[y + ForwardDirection(color), x] = color;
                    Pieces[y, x] = Color.Empty;
                    RegisterMove(new AvailableActions(y, x, y + ForwardDirection(color), x, Actions.Forward), player);

                    return true;
                }
            }
            return false;
        }
    }
}