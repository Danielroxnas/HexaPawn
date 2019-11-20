using System;
using System.Diagnostics.CodeAnalysis;

namespace HexaPawnConsole
{
    public class AvailableAction  : IEquatable<AvailableAction>
    { 

        public AvailableAction()
        {
        }

        public AvailableAction(string fromKey,Color color, Actions actions)
        {
            Key = fromKey;
            Color = color;
            Action = actions;
        }

        public string Key { get; }
        public Color Color { get; }
        public Actions Action { get; }

        public bool Equals([AllowNull] AvailableAction other)
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
}
