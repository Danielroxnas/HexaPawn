using HexaPawnConsole;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace HexaPawnTests
{
    public class MoveServiceTests
    {
        private MovService _sut;
        private IBoardService _boardService;

        [SetUp]
        public void Setup()
        {
            _sut = new MovService();

            _boardService = new BoardService(new BoardState(), _sut);
            _boardService.InitPieces();
            _boardService.InitPlayers(true, false);
        }

        [Test]
        public void It_should_move_piece_forward_direction_based_if_white()
        {

            var result = _sut.MoveForward("C1", 1, _boardService.Pieces1);
            Assert.That(result, Is.EqualTo(true));
            Assert.That(_boardService.Pieces1.First(x => x.Key == "B1").Value, Is.EqualTo((int)Color.White));
            Assert.That(_boardService.Pieces1.First(x => x.Key == "C1").Value, Is.EqualTo((int)Color.Empty));
        }

        [Test]
        public void It_should_move_piece_forward_direction_based_if_black()
        {
            var result = _sut.MoveForward("A1", 2, _boardService.Pieces1);

            Assert.That(result, Is.EqualTo(true));
            Assert.That(_boardService.Pieces1.First(x => x.Key == "B1").Value, Is.EqualTo((int)Color.Black));
            Assert.That(_boardService.Pieces1.First(x => x.Key == "A1").Value, Is.EqualTo((int)Color.Empty));
        }

        private static IEnumerable<TestCaseData> PieceTestData
        {
            get
            {
                yield return new TestCaseData((int)Color.Black);
                yield return new TestCaseData((int)Color.White);
            }
        }

        [TestCaseSource(nameof(PieceTestData))]
        public void It_should_not_move_piece_forward_if_blocked(int piece)
        {
            _boardService.Pieces1["B1"] = piece;
            var result = _sut.MoveForward("C1", 1, _boardService.Pieces1);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_boardService.Pieces1["C1"], Is.EqualTo((int)Color.White));
            Assert.That(_boardService.Pieces1["B1"], Is.EqualTo(piece));
        }

        //[Test]
        //public void It_should_not_move_piece_forward_if_not_players_piece()
        //{
        //    _boardService.Pieces[1, 0] = (int)Color.Black;
        //    var result = _sut.MoveForward(2, 0, (int)Color.Black, _boardService.Pieces);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_boardService.Pieces[2, 0], Is.EqualTo((int)Color.White));
        //    Assert.That(_boardService.Pieces[1, 0], Is.EqualTo((int)Color.Black));
        //}

        //[Test]
        //public void It_should_not_attack_left_if_left_is_empty()
        //{
        //    _boardService.Pieces[1, 1] = (int)Color.Empty;
        //    var result = _sut.AttackLeft(2, 2, (int)Color.White, _boardService.Pieces);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_boardService.Pieces[2, 2], Is.EqualTo((int)Color.White));
        //    Assert.That(_boardService.Pieces[1, 1], Is.EqualTo((int)Color.Empty));
        //}

        //[Test]
        //public void It_should_not_attack_left_if_left_is_same_player()
        //{
        //    _boardService.Pieces[1, 1] = (int)Color.White;
        //    var result = _sut.AttackLeft(2, 2, (int)Color.White, _boardService.Pieces);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_boardService.Pieces[2, 2], Is.EqualTo((int)Color.White));
        //    Assert.That(_boardService.Pieces[1, 1], Is.EqualTo((int)Color.White));
        //}

        //[Test]
        //public void It_should_not_attack_left_if_out_of_board()
        //{
        //    var result = _sut.AttackLeft(2, 0, (int)Color.White, _boardService.Pieces);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_boardService.Pieces[2, 0], Is.EqualTo((int)Color.White));
        //}

        //[Test]
        //public void It_should_attack_left_if_left_is_opposite()
        //{
        //    _boardService.Pieces[1, 1] = (int)Color.Black;
        //    var result = _sut.AttackLeft(2, 2, (int)Color.White, _boardService.Pieces);
        //    Assert.That(result, Is.EqualTo(true));
        //    Assert.That(_boardService.Pieces[2, 2], Is.EqualTo((int)Color.Empty));
        //    Assert.That(_boardService.Pieces[1, 1], Is.EqualTo((int)Color.White));
        //}

        //[Test]
        //public void It_should_not_attack_right_if_right_is_empty()
        //{
        //    _boardService.Pieces[1, 1] = (int)Color.Empty;
        //    var result = _sut.AttackRight(2, 0, (int)Color.White, _boardService.Pieces);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_boardService.Pieces[2, 2], Is.EqualTo((int)Color.White));
        //    Assert.That(_boardService.Pieces[1, 1], Is.EqualTo((int)Color.Empty));
        //}

        //[Test]
        //public void It_should_not_attack_right_if_right_is_same_player()
        //{
        //    _boardService.Pieces[1, 1] = (int)Color.White;
        //    var result = _sut.AttackRight(2, 0, (int)Color.White, _boardService.Pieces);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_boardService.Pieces[2, 2], Is.EqualTo((int)Color.White));
        //    Assert.That(_boardService.Pieces[1, 1], Is.EqualTo((int)Color.White));
        //}

        //[Test]
        //public void It_should_not_attack_right_if_out_of_board()
        //{
        //    var result = _sut.AttackRight(2, 2, (int)Color.White, _boardService.Pieces);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_boardService.Pieces[2, 2], Is.EqualTo((int)Color.White));
        //}

        //[TestCase(1, 0)]
        //[TestCase(2, 1)]
        //public void It_should_attack_right_if_right_is_opposite(int blockX, int attackX)
        //{
        //    _boardService.Pieces[1, blockX] = (int)Color.Black;
        //    var result = _sut.AttackRight(2, attackX, (int)Color.White, _boardService.Pieces);
        //    Assert.That(result, Is.EqualTo(true));
        //    Assert.That(_boardService.Pieces[2, attackX], Is.EqualTo((int)Color.Empty));
        //    Assert.That(_boardService.Pieces[1, blockX], Is.EqualTo((int)Color.White));
        //}

    }
}
