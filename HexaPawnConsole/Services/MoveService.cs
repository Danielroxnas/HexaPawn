using System;
using System.Collections.Generic;
using System.Linq;

namespace HexaPawnConsole
{
    public class MoveService : IMoveService
    {
        public bool AttackLeft(string key, Color color, Dictionary<string, Color> pieces)
        {
            if (!CanAttackLeft(key, color, pieces))
            {
                return false;
            }
            return ExecuteAction(key, color, pieces, -1);
        }

        public bool AttackRight(string key, Color color, Dictionary<string, Color> pieces)
        {
            if (!CanAttackRight(key, color, pieces))
            {
                return false;
            }
            return ExecuteAction(key, color, pieces, 1);

        }

        public bool CanAttackLeft(string key, Color color, Dictionary<string, Color> pieces)
        {
            var (y, x) = GenerateCoods(key, pieces);
            var left = x - 1;
            var expectedMove = ForwardDirection(color, y) + left;

            return pieces.Any(piece =>
            left >= 1 &&
            piece.Key == expectedMove &&
            piece.Value == opponent(color));
        }

        private Color opponent(Color color) => color == Color.White ? Color.Black : Color.White;

        public bool CanAttackRight(string key, Color color, Dictionary<string, Color> pieces)
        {
            var (y, x) = GenerateCoods(key, pieces);
            var right = x + 1;
            var expectedMove = ForwardDirection(color, y) + right;

            return pieces.Any(piece =>
            right <= 3 &&
            piece.Key == expectedMove &&
            piece.Value == opponent(color));
        }

        public bool CanMoveForward(string key, Color color, Dictionary<string, Color> pieces)
        {
            var (y, x) = GenerateCoods(key, pieces);
            var expectedMove = ForwardDirection(color, y) + x;
            return !pieces.Any(x => x.Key == expectedMove && x.Value != 0);
        }

        public static string ForwardDirection(Color x, string y) =>

        x switch
        {
            Color.White => y == "C" ? "B" : "A",
            Color.Black => y == "A" ? "B" : "C",
            _ => y
        };
        private (string y, int x) GenerateCoods(string key, Dictionary<string, Color> pieces)
        {
            var x = pieces.First(x => x.Key == key).Key[1].ToString();
            var y = pieces.First(x => x.Key == key).Key[0].ToString();
            return (y, int.Parse(x));
        }

        public bool MoveForward(string key, Color color, Dictionary<string, Color> pieces)
        {
            if (!CanMoveForward(key, color, pieces))
            {
                return false;
            }
            return ExecuteAction(key, color, pieces, 0);
        }

        private bool ExecuteAction(string key, Color color, Dictionary<string, Color> pieces, int side)
        {
            var (y, x) = GenerateCoods(key, pieces);
            var newDirection = ForwardDirection(color, y) + (x + side);
            pieces[key] = 0;
            pieces[newDirection] = color;
            return true;
        }
    }
}