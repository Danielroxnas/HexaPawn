using HexaPawnConsole1;
using System;
using System.Diagnostics.CodeAnalysis;

namespace HexaPawnConsole11
{
    public class AvailableAction1  : IEquatable<AvailableAction1>
    { 

        public AvailableAction1()
        {
        }

        public AvailableAction1(string fromKey,Color color, Actions actions)
        {
            Key = fromKey;
            Color = color;
            Action = actions;
            Id = Guid.NewGuid();
        }

        public string Key { get; }
        public Color Color { get; }
        public Actions Action { get; }
        public Guid Id { get; set; }

        public bool Equals([AllowNull] AvailableAction1 other)
        {
            if (other == null) return false;
            if (other.Key == Key &&
                other.Color == Color)
            {
                return true;
            }
            return false;
        }
    }
    public class AvailableAction : IEquatable<AvailableAction>, IAvailableActions
    {
        public int FromX, FromY, ToX, ToY;

        public AvailableAction()
        {
        }
        public AvailableAction(int fromY, int fromX, Actions action)
        {
            FromX = fromX;
            FromY = fromY;
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
