﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HexaPawnConsole
{
    public class Pawn
    {

        public bool Occupied([AllowNull] Point other)
        {
            if (other == null) return false;

            if (Standing.X == other.X && Standing.Y == other.Y)
                return true;

            return false;
        }

        public bool Removed { get; set; } = false;
        public Pawn(int pawnId, Point startPoint, bool isHuman, OrderNumber player)
        {
            StartPoint = startPoint;
            Standing = new Point(startPoint);
            IsHuman = isHuman;
            Player = player;
            PawnId = pawnId;
            Removed = false;
            AllPawns.Pawns.Add(this);
        }

        public Point Standing { get; set; }
        public Point StartPoint { get; set; }
        public bool IsHuman { get; }
        public OrderNumber Player { get; }
        public int PawnId { get; set; }
        public List<Point> RemovedMoves { get; set; } = new List<Point>();
    }
    public enum DirectionType
    {
        Forward, Left, Right
    }
}
