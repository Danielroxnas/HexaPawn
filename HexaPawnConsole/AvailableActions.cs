using System;
using System.Diagnostics.CodeAnalysis;

namespace HexaPawnConsole
{
    public class AvailableActions : IEquatable<AvailableActions>, IAvailableActions
    {
        public AvailableActions()
        {

        }
        public int FromX, FromY, ToX, ToY;
        public AvailableActions(int fromY, int fromX, int toY, int toX, Actions action)
        {
            FromX = fromX;
            FromY = fromY;
            ToX = toX;
            Action = action;
            ToY = toY;
        }

        public Actions Action { get; }

        public bool Equals([AllowNull] AvailableActions other)
        {
            if (other == null) return false;
            if(other.FromX == FromX &&
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
