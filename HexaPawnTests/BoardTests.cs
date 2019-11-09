using HexaPawnConsole;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using static HexaPawnConsole.Board;

namespace HexaPawnTests
{
    public class BoardTests
    {
        private Board _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Board();
            _sut.InitPlayers(new Human1(), new Human1());
        }

        [Test]
        public void It_should_generate_a_3x3_board()
        {
            var sut = new Board();
            Assert.That(sut.Pieces[0, 0], Is.EqualTo(Piece.Empty));
            Assert.That(sut.Pieces[2, 2], Is.EqualTo(Piece.Empty));
        }

        [Test]
        public void It_should_initialize_white_and_black_pieces()
        {

            Assert.That(_sut.Pieces[0, 0], Is.EqualTo(Piece.White));
            Assert.That(_sut.Pieces[0, 1], Is.EqualTo(Piece.White));
            Assert.That(_sut.Pieces[0, 2], Is.EqualTo(Piece.White));
            Assert.That(_sut.Pieces[2, 0], Is.EqualTo(Piece.Black));
            Assert.That(_sut.Pieces[2, 1], Is.EqualTo(Piece.Black));
            Assert.That(_sut.Pieces[2, 2], Is.EqualTo(Piece.Black));
        }

        [Test]
        public void It_should_init_board_with_set_pieces()
        {
            var result = new Board(_sut);
            Assert.That(result.Pieces[2, 0], Is.EqualTo(Piece.Black));
            Assert.That(result.Pieces[0, 1], Is.EqualTo(Piece.White));
        }

        [Test]
        public void It_should_move_piece_forward_direction_based_if_white()
        {

            var result = _sut.MoveForward(0, 0, Piece.White);
            Assert.That(result, Is.EqualTo(true));
            Assert.That(_sut.Pieces[0, 0], Is.EqualTo(Piece.Empty));
            Assert.That(_sut.Pieces[1, 0], Is.EqualTo(Piece.White));
        }

        [Test]
        public void It_should_move_piece_forward_direction_based_if_black()
        {
            var result = _sut.MoveForward(2, 0, Piece.Black);
            Assert.That(result, Is.EqualTo(true));
            Assert.That(_sut.Pieces[2, 0], Is.EqualTo(Piece.Empty));
            Assert.That(_sut.Pieces[1, 0], Is.EqualTo(Piece.Black));
        }

        private static IEnumerable<TestCaseData> PieceTestData
        {
            get
            {
                yield return new TestCaseData(Piece.Black);
                yield return new TestCaseData(Piece.White);
            }
        }

        [TestCaseSource(nameof(PieceTestData))]
        public void It_should_not_move_piece_forward_if_blocked(Piece piece)
        {
            _sut.Pieces[1, 0] = piece;
            var result = _sut.MoveForward(0, 0, Piece.White);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_sut.Pieces[0, 0], Is.EqualTo(Piece.White));
            Assert.That(_sut.Pieces[1, 0], Is.EqualTo(piece));
        }

        [Test]
        public void It_should_not_move_piece_forward_if_blocked()
        {
            _sut.Pieces[1, 0] = Piece.Black;
            var result = _sut.MoveForward(0, 0, Piece.White);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_sut.Pieces[0, 0], Is.EqualTo(Piece.White));
            Assert.That(_sut.Pieces[1, 0], Is.EqualTo(Piece.Black));
        }

        [Test]
        public void It_should_not_move_piece_forward_if_not_players_piece()
        {
            _sut.Pieces[1, 0] = Piece.Black;
            var result = _sut.MoveForward(0, 0, Piece.Black);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_sut.Pieces[0, 0], Is.EqualTo(Piece.White));
            Assert.That(_sut.Pieces[1, 0], Is.EqualTo(Piece.Black));
        }

        [Test]
        public void It_should_not_attack_left_if_left_is_empty()
        {
            _sut.Pieces[1, 1] = Piece.Empty;
            var result = _sut.AttackLeft(0, 2, Piece.White);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_sut.Pieces[0, 2], Is.EqualTo(Piece.White));
            Assert.That(_sut.Pieces[1, 1], Is.EqualTo(Piece.Empty));
        }

        [Test]
        public void It_should_not_attack_left_if_left_is_same_player()
        {
            _sut.Pieces[1, 1] = Piece.White;
            var result = _sut.AttackLeft(0, 2, Piece.White);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_sut.Pieces[0, 2], Is.EqualTo(Piece.White));
            Assert.That(_sut.Pieces[1, 1], Is.EqualTo(Piece.White));
        }

        [Test]
        public void It_should_not_attack_left_if_out_of_board()
        {
            var result = _sut.AttackLeft(0, 0, Piece.White);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_sut.Pieces[0, 0], Is.EqualTo(Piece.White));
        }

        [Test]
        public void It_should_attack_left_if_left_is_opposite()
        {
            _sut.Pieces[1, 1] = Piece.Black;
            var result = _sut.AttackLeft(0, 2, Piece.White);
            Assert.That(result, Is.EqualTo(true));
            Assert.That(_sut.Pieces[0, 2], Is.EqualTo(Piece.Empty));
            Assert.That(_sut.Pieces[1, 1], Is.EqualTo(Piece.White));
        }

        [Test]
        public void It_should_not_attack_right_if_right_is_empty()
        {
            _sut.Pieces[1, 1] = Piece.Empty;
            var result = _sut.AttackRight(0, 0, Piece.White);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_sut.Pieces[0, 2], Is.EqualTo(Piece.White));
            Assert.That(_sut.Pieces[1, 1], Is.EqualTo(Piece.Empty));
        }

        [Test]
        public void It_should_not_attack_right_if_right_is_same_player()
        {
            _sut.Pieces[1, 1] = Piece.White;
            var result = _sut.AttackRight(0, 0, Piece.White);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_sut.Pieces[0, 2], Is.EqualTo(Piece.White));
            Assert.That(_sut.Pieces[1, 1], Is.EqualTo(Piece.White));
        }

        [Test]
        public void It_should_not_attack_right_if_out_of_board()
        {
            var result = _sut.AttackRight(0, 2, Piece.White);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_sut.Pieces[0, 2], Is.EqualTo(Piece.White));
        }

        [TestCase(1,0)]
        [TestCase(2,1)]
        public void It_should_attack_right_if_right_is_opposite(int blockX, int attackX)
        {
            _sut.Pieces[1, blockX] = Piece.Black;
            var result = _sut.AttackRight(0, attackX, Piece.White);
            Assert.That(result, Is.EqualTo(true));
            Assert.That(_sut.Pieces[0, attackX], Is.EqualTo(Piece.Empty));
            Assert.That(_sut.Pieces[1, blockX], Is.EqualTo(Piece.White));
        }

        [Test]
        public void It_should_return_list_of_AvailableActions()
        {
            var result = _sut.GetAllPlayerAvailableActions(Piece.White);
            Assert.That(result, Is.TypeOf<List<AvailableActions>>());
        }

        [Test]
        public void It_should_return_list_of_AvailableActions_with_values()
        {
            var result = _sut.GetAllPlayerAvailableActions(Piece.White);
            Assert.That(result.First().FromX, Is.EqualTo(0));
            Assert.That(result.First().FromY, Is.EqualTo(0));
            Assert.That(result.First().ToX, Is.EqualTo(0));
            Assert.That(result.First().ToY, Is.EqualTo(1));
            Assert.That(result.First().Action, Is.EqualTo(Actions.Forward));
        }

        [Test]
        public void It()
        {
            _sut.MoveForward(2, 1, Piece.Black);
            new Game().GenerateBoard(_sut);
        }
    }
}
