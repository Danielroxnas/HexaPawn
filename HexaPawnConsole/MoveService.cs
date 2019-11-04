using System;
using System.Collections.Generic;
using System.Linq;

namespace HexaPawnConsole
{
    public class MoveService
    {
        public Point MoveTo(Pawn pawn, Point point)
        {
            pawn.Standing.X = point.X;
            pawn.Standing.Y = point.Y;
            Utils.History.Add((pawn.PawnId, point));
            var pawnToRemove = AllPawns.Pawns
                .Where(x => !x.Removed && x.PawnId != pawn.PawnId)
                .FirstOrDefault(x => x.Occupied(pawn.Standing));
            if (pawnToRemove != null) Utils.RemovePawn(pawnToRemove);
            
            return pawn.Standing;
        }

        public Point MoveViaDirection(Pawn pawn, DirectionType direction)
        {
            var point = direction switch
            {
                DirectionType.Forward => GetMoveForwardPoint(pawn),
                DirectionType.Left => GetMoveLeftPoint(pawn),
                DirectionType.Right => GetMoveRightPoint(pawn),
                _ => null
            };
            if (point == null) return null;
            return MoveTo(pawn, point);
        }

        public Point GetMoveForwardPoint(Pawn pawn)
        {
            var expectedPoint = new Point(pawn.Standing.X, pawn.Standing.Y + Forward(pawn));
            if (AllPawns.Pawns
                .Where(x => !x.Removed)
                .Any(x => x.Occupied(expectedPoint)) || pawn.Removed ) return null;

            return expectedPoint;
        }
        private int Forward(Pawn pawn) => pawn.Player == OrderNumber.ONE ? -1 : 1;

        public Point GetMoveRightPoint(Pawn pawn)
        {
            var expectedPoint = new Point(pawn.Standing.X + 1, pawn.Standing.Y + Forward(pawn));
            if (expectedPoint.X > 3) return null;
            return ValidateMoveSidePoint(pawn, expectedPoint);

        }

        public Point GetMoveLeftPoint(Pawn pawn)
        {
            var expectedPoint = new Point(pawn.Standing.X + -1, pawn.Standing.Y + Forward(pawn));
            if (expectedPoint.X < 1) return null;
            return ValidateMoveSidePoint(pawn, expectedPoint);
        }

        private Point ValidateMoveSidePoint(Pawn pawn, Point expectedPoint)
        {
            if (AllPawns.Pawns
                .Where(x => !x.Removed && x.Player == pawn.Player)
                .Any(x => x.Occupied(expectedPoint)) || pawn.Removed) return null;

            if (AllPawns.Pawns
                .Where(x => !x.Removed && x.Player != pawn.Player)
                .Any(x => x.Occupied(expectedPoint)) || pawn.Removed) return expectedPoint;
            return null;
        }
        public Dictionary<DirectionType, Point> GetAvailablePoints(Pawn pawn)
        {
            var forward = GetMoveForwardPoint(pawn);
            var left = GetMoveLeftPoint(pawn);
            var right = GetMoveRightPoint(pawn);
            var points = new Dictionary<DirectionType, Point>();
            if (forward != null && NotRemoved(pawn, forward)) points.Add(DirectionType.Forward, forward);
            if (left != null && NotRemoved(pawn, forward)) points.Add(DirectionType.Left, left);
            if (right != null && NotRemoved(pawn, forward)) points.Add(DirectionType.Right, right);

            return points;
        }

        private static bool NotRemoved(Pawn pawn, Point forward)
        {
            return !pawn.RemovedMoves.Any(x => x.X == forward.X && x.Y == forward.Y);
        }

        public Dictionary<Pawn, Dictionary<DirectionType, Point>> GetAllAvailablePoints(OrderNumber player)
        {
            var pawns = AllPawns.Pawns.Where(x => x.Player == player).ToList();
            var allPawnsPoints = new Dictionary<Pawn, Dictionary<DirectionType, Point>>();
            pawns.ForEach(x =>
            {
                var points = GetAvailablePoints(x);
                if (points.Any())
                {
                    allPawnsPoints.Add(x, points);
                }
            });

            return allPawnsPoints;
        }

        public Point MoveRandom(OrderNumber player)
        {
            var points = GetAllAvailablePoints(player);
            if (!points.Keys.Any())
            {
                return null;
            }
            var rnd = new Random();
            var rndPawn = rnd.Next(0, points.Count());
            var pawn = points.Keys.ElementAt(rndPawn);
            var value = points.Values.ElementAt(rndPawn);
            var point = value.Values.ElementAt(rnd.Next(0, value.Count()));
            MoveTo(pawn, point);
            return point;
        }
        public DirectionType Direction(int input)
        {
            return (DirectionType)input;
        }
    }
}
