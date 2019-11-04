using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace HexaPawnConsole
{
    public class Point
    {
        public Point(Point point)
        {
            X = point.X;
            Y = point.Y;
        }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
