using System;
using System.Diagnostics.CodeAnalysis;

namespace HexaPawnConsole
{
    public class AvailableAction : IEquatable<AvailableAction>, IAvailableActions
    {
        public int FromX, FromY, ToX, ToY;

        public AvailableAction()
        {
        }

        public AvailableAction(int fromY, int fromX, int toY, int toX, Actions action)
        {
            FromX = fromX;
            FromY = fromY;
            ToX = toX;
            Action = action;
            ToY = toY;
        }

        public Actions Action { get; }

        public bool Equals([AllowNull] AvailableAction other)
        {
            if (other == null) return false;
            if (other.FromX == FromX &&
                other.FromY == FromY &&
                other.ToX == ToX &&
                other.ToY == ToY)
            {
                return true;
            }
            return false;
        }
    }

    public interface IAvailableActions
    {
    }
}
