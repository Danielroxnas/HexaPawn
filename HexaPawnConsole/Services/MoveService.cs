namespace HexaPawnConsole
{
    public class MoveService : IMoveService
    {


        public int ForwardDirection(Color color)
        {
            return color == Color.White ? -1 : 1;
        }

        #region Can
        public bool CanMoveForward(int y, int x, Color color, Color[,] Pieces)
        {
            return Pieces[y + ForwardDirection(color), x] == Color.Empty;
        }
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
        #endregion

        #region Execute
        public bool MoveForward(int y, int x, Color color, Color[,] Pieces)
        {
            if (Pieces[y, x] == color)
            {
                if (CanMoveForward(y, x, color, Pieces))
                {
                    Pieces[y + ForwardDirection(color), x] = color;
                    Pieces[y, x] = Color.Empty;
                    return true;
                }
            }
            return false;
        }
        public bool AttackLeft(int y, int x, Color color, Color[,] Pieces)
        {
            if (Pieces[y, x] == color)
            {
                if (CanAttackLeft(y, x, color, Pieces))
                {
                    Pieces[y + ForwardDirection(color), x - 1] = color;
                    Pieces[y, x] = Color.Empty;
                    return true;
                }
            }
            return false;
        }

        public bool AttackRight(int y, int x, Color color, Color[,] Pieces)
        {
            if (Pieces[y, x] == color)
            {
                if (CanAttackRight(y, x, color, Pieces))
                {
                    Pieces[y + ForwardDirection(color), x + 1] = color;
                    Pieces[y, x] = Color.Empty;

                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}