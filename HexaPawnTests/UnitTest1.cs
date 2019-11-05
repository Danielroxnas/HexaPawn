using HexaPawnConsole;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace HexaPawnTests
{
    public class Tests
    {
        private MoveService _sut;
        private Pawn _pawn;

        [OneTimeSetUp]
        public void Setup()
        {
            Utils.InitPlayers();
            _sut = new MoveService();

        }
        [TearDown]
        public void TearDown()
        {
            Utils.ResetPlayers();
            _pawn.RemovedMoves = new List<Point>();
        }

        [Test]
        public void It_should_not_be_possible_to_move_if_blocked_forward()
        {
            _pawn = Utils.SelectPawn(4);
            var pawn1 = Utils.SelectPawn(1);
            _sut.MoveViaDirection(pawn1,DirectionType.Forward);
            _sut.MoveViaDirection(_pawn, DirectionType.Forward);

            Assert.That(_pawn.Standing.X, Is.EqualTo(1));
            Assert.That(_pawn.Standing.Y, Is.EqualTo(1));
        }
        [Test]
        public void It_should_be_possible_to_move_if_removed_pawn()
        {
            _pawn = Utils.SelectPawn(4);
            var pawn1 = Utils.SelectPawn(1);
            _sut.MoveViaDirection(pawn1, DirectionType.Forward);
            Utils.RemovePawn(pawn1);
            _sut.MoveViaDirection(_pawn, DirectionType.Forward);

            Assert.That(_pawn.Standing.X, Is.EqualTo(1));
            Assert.That(_pawn.Standing.Y, Is.EqualTo(2));
        }
        [Test]
        public void It_should_be_possible_to_move_forward_if_no_blaock()
        {
            _pawn = Utils.SelectPawn(4);
            var pawn1 = Utils.SelectPawn(1);
            _sut.MoveViaDirection(pawn1, DirectionType.Forward);
            _sut.MoveViaDirection(_pawn, DirectionType.Forward);

            Assert.That(_pawn.Standing.X, Is.EqualTo(1));
            Assert.That(_pawn.Standing.Y, Is.EqualTo(1));
        }

        [Test]
        public void It_should_not_be_possible_to_move_right_if_same_player_is_blocking()
        {
            _pawn = Utils.SelectPawn(4);
            var pawn1 = Utils.SelectPawn(5);
            _sut.MoveViaDirection(pawn1, DirectionType.Forward);
            _sut.MoveViaDirection(_pawn, DirectionType.Right);

            Assert.That(_pawn.Standing.X, Is.EqualTo(1));
            Assert.That(_pawn.Standing.Y, Is.EqualTo(1));
        }
        [Test]
        public void It_should_not_be_possible_to_move_right_if_removed_player()
        {
            _pawn = Utils.SelectPawn(4);
            var pawn1 = Utils.SelectPawn(2);
            _sut.MoveViaDirection(pawn1, DirectionType.Forward);
            Utils.RemovePawn(pawn1);
            _sut.MoveViaDirection(_pawn, DirectionType.Right);

            Assert.That(_pawn.Standing.X, Is.EqualTo(1));
            Assert.That(_pawn.Standing.Y, Is.EqualTo(1));
        }

        [Test]
        public void It_should_be_possible_to_move_right_if_opposite_player_occupied_block()
        {
            _pawn = Utils.SelectPawn(4);
            var pawn1 = Utils.SelectPawn(2);
            _sut.MoveViaDirection(pawn1, DirectionType.Forward);
            _sut.MoveViaDirection(_pawn, DirectionType.Right);

            Assert.That(_pawn.Standing.X, Is.EqualTo(2));
            Assert.That(_pawn.Standing.Y, Is.EqualTo(2));
        }
        [Test]
        public void It_should_not_be_possible_to_move_right_if_wall()
        {
            _pawn = Utils.SelectPawn(6);
            _sut.MoveViaDirection(_pawn, DirectionType.Right);

            Assert.That(_pawn.Standing.X, Is.EqualTo(3));
            Assert.That(_pawn.Standing.Y, Is.EqualTo(1));
        }
        [Test]
        public void It_should_not_be_possible_to_move_left_if_same_player_is_blocking()
        {
            _pawn = Utils.SelectPawn(6);
            var pawn1 = Utils.SelectPawn(5);
            _sut.MoveViaDirection(pawn1, DirectionType.Forward);
            _sut.MoveViaDirection(_pawn, DirectionType.Left);

            Assert.That(_pawn.Standing.X, Is.EqualTo(3));
            Assert.That(_pawn.Standing.Y, Is.EqualTo(1));
        }
        [Test]
        public void It_should_be_possible_to_move_left_if_opposite_player_occupied_block()
        {
            _pawn = Utils.SelectPawn(6);
            var pawn1 = Utils.SelectPawn(2);
            _sut.MoveViaDirection(pawn1, DirectionType.Forward);
            _sut.MoveViaDirection(_pawn, DirectionType.Left);

            Assert.That(_pawn.Standing.X, Is.EqualTo(2));
            Assert.That(_pawn.Standing.Y, Is.EqualTo(2));
        }

        [Test]
        public void It_should_not_be_possible_to_move_left_if_removed_player()
        {
            _pawn = Utils.SelectPawn(6);
            var pawn1 = Utils.SelectPawn(2);
            _sut.MoveViaDirection(pawn1, DirectionType.Forward);
            Utils.RemovePawn(pawn1);
            _sut.MoveViaDirection(_pawn, DirectionType.Left);

            Assert.That(_pawn.Standing.X, Is.EqualTo(3));
            Assert.That(_pawn.Standing.Y, Is.EqualTo(1));
        }

        [Test]
        public void It_should_not_be_possible_to_move_left_if_wall()
        {
            _pawn = Utils.SelectPawn(4);
            _sut.MoveViaDirection(_pawn, DirectionType.Left);

            Assert.That(_pawn.Standing.X, Is.EqualTo(1));
            Assert.That(_pawn.Standing.Y, Is.EqualTo(1));
        }

        [Test]
        public void It_should_get_available_point()
        {
            _pawn = Utils.SelectPawn(4);
            var result = _sut.GetAvailablePoints(_pawn);

            Assert.That(result.Keys.First(), Is.EqualTo(DirectionType.Forward));
        }
        [Test]
        public void It_should_not_get_available_point_if_removed()
        {
            _pawn = Utils.SelectPawn(4);
            _pawn.RemovedMoves.Add(new Point(1, 2));
            var result = _sut.GetAvailablePoints(_pawn);

            Assert.That(result.Count(), Is.EqualTo(0));
        }
        [Test]
        public void It_should_get_all_available_point()
        {
            var pawn1 = Utils.SelectPawn(1);
            var pawn2 = Utils.SelectPawn(2);
            var pawn3 = Utils.SelectPawn(3);
            var result = _sut.GetAllAvailablePoints(OrderNumber.ONE);

            Assert.That(result.Keys.First(x => x.PawnId == 1), Is.EqualTo(pawn1));
            Assert.That(result.Keys.First(x => x.PawnId == 2), Is.EqualTo(pawn2));
            Assert.That(result.Keys.First(x => x.PawnId == 3), Is.EqualTo(pawn3));
        }
    }
}