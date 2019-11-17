using System;
using System.Collections.Generic;
using System.Linq;

namespace HexaPawnConsole
{



    public class MovService : IMovService
    {
        public bool AttackLeft(string key, int color, Dictionary<string, int> pieces)
        {
            if (!CanAttackLeft(key, color, pieces))
            {
                return false;
            }
            return ExecuteAction(key, color, pieces, -1);

        }

        public bool AttackRight(string key, int color, Dictionary<string, int> pieces)
        {
            if (!CanAttackRight(key, color, pieces))
            {
                return false;
            }
            return ExecuteAction(key, color, pieces, 1);

        }

        public bool CanAttackLeft(string key, int color, Dictionary<string, int> pieces)
        {
            var (y, x) = GenerateCoods(key, pieces);
            var left = x - 1;

            var expectedMove = ForwardDirection(color, y) + left;

            return pieces.Any(piece =>
            left >= 1 &&
            piece.Key == expectedMove &&
            piece.Value == opponent(color));
        }

        private int opponent(int color) => color == 1 ? 2 : 1;

        public bool CanAttackRight(string key, int color, Dictionary<string, int> pieces)
        {
            var (y, x) = GenerateCoods(key, pieces);
            var right = x + 1;
            var expectedMove = ForwardDirection(color, y) + right;

            return pieces.Any(piece =>
            right <= 3 &&
            piece.Key == expectedMove &&
            piece.Value == opponent(color));
        }

        public bool CanMoveForward(string key, int color, Dictionary<string, int> pieces)
        {
            var (y, x) = GenerateCoods(key, pieces);
            var expectedMove = ForwardDirection(color, y) + x;
            return !pieces.Any(x => x.Key == expectedMove && x.Value != 0);
        }

        public static string ForwardDirection(int color, string y) =>

        color switch
        {
            1 => y == "C" ? "B" : "A",
            2 => y == "A" ? "B" : "C",
            _ => y
        };
        private (string y, int x) GenerateCoods(string key, Dictionary<string, int> pieces)
        {
            var x = pieces.First(x => x.Key == key).Key[1].ToString();
            var y = pieces.First(x => x.Key == key).Key[0].ToString();
            return (y, int.Parse(x));
        }

        public bool MoveForward(string key, int color, Dictionary<string, int> pieces)
        {
            if (!CanMoveForward(key, color, pieces))
            {
                return false;
            }
            return ExecuteAction(key, color, pieces, 0);

        }

        private bool ExecuteAction(string key, int color, Dictionary<string, int> pieces, int side)
        {
            var (y, x) = GenerateCoods(key, pieces);
            var newDirection = ForwardDirection(color, y) + (x + side);
            pieces[key] = 0;
            pieces[newDirection] = color;
            return true;
        }
    }









    public class MoveService : IMoveService
    {
        public int ForwardDirection(int color)
        {
            return color == (int)Color.White ? -1 : 1;
        }

        #region Can
        public bool CanMoveForward(int y, int x, int color, Pieces Pieces)
        {
            return Pieces[y + ForwardDirection(color), x] == 0;
        }
        public bool CanAttackLeft(int y, int x, int color, Pieces Pieces)
        {
            if ((x >= 1 && x <= 2))
            {
                return Pieces[y + ForwardDirection(color), x - 1] != 0 &&
                Pieces[y + ForwardDirection(color), x - 1] != color;
            }
            return false;
        }

        public bool CanAttackRight(int y, int x, int color, Pieces Pieces)
        {
            if ((x >= 0 && x <= 1))
            {
                return Pieces[y + ForwardDirection(color), x + 1] != 0 &&
                Pieces[y + ForwardDirection(color), x + 1] != color;
            }
            return false;
        }
        #endregion

        #region Execute
        public bool MoveForward(int y, int x, int color, Pieces Pieces)
        {
            if (Pieces[y, x] == color)
            {
                if (CanMoveForward(y, x, color, Pieces))
                {
                    Pieces[y + ForwardDirection(color), x] = color;
                    Pieces[y, x] = 0;
                    return true;
                }
            }
            return false;
        }
        public bool AttackLeft(int y, int x, int color, Pieces Pieces)
        {
            if (Pieces[y, x] == color)
            {
                if (CanAttackLeft(y, x, color, Pieces))
                {
                    Pieces[y + ForwardDirection(color), x - 1] = color;
                    Pieces[y, x] = 0;
                    return true;
                }
            }
            return false;
        }

        public bool AttackRight(int y, int x, int color, Pieces Pieces)
        {
            if (Pieces[y, x] == color)
            {
                if (CanAttackRight(y, x, color, Pieces))
                {
                    Pieces[y + ForwardDirection(color), x + 1] = color;
                    Pieces[y, x] = 0;

                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}