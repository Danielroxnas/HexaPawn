using System;
using System.Diagnostics.CodeAnalysis;

namespace HexaPawnServices
{
    public class AvailableAction : IEquatable<AvailableAction>, IAvailableActions
    {
        //public int FromX, FromY, ToX, ToY {get;set;}
        public int FromY { get; set; }
        public int FromX { get; set; }
        public int ToY { get; set; }
        public int ToX { get; set; }

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
