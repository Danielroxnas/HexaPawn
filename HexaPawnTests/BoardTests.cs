using HexaPawnConsole;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace HexaPawnTests
{
    public class MoveServiceTests
    {
        private MoveService _sut;
        private IBoardService _boardService;
        private Color[,] _pieces;
        private Human _p1;
        private Human _p2;

        [SetUp]
        public void Setup()
        {
            _sut = new MoveService();
            _boardService = Mock.Of<IBoardService>();
            _boardService.Pieces = 
            _pieces = new Color[0,0];
            _p1 = new Human(Color.White, Mock.Of<IBoardState>());
            _p2 = new Human(Color.Black, Mock.Of<IBoardState>());
        }

        //[Test]
        //public void It_should_generate_a_3x3_board()
        //{
        //    var sut = new BoardService(null, null);
        //    Assert.That(sut.Pieces[0, 0], Is.EqualTo(Color.Empty));
        //    Assert.That(sut.Pieces[2, 2], Is.EqualTo(Color.Empty));
        //}

        //[Test]
        //public void It_should_initialize_white_and_black_pieces()
        //{

        //    Assert.That(_sut.Pieces[0, 0], Is.EqualTo(Color.White));
        //    Assert.That(_sut.Pieces[0, 1], Is.EqualTo(Color.White));
        //    Assert.That(_sut.Pieces[0, 2], Is.EqualTo(Color.White));
        //    Assert.That(_sut.Pieces[2, 0], Is.EqualTo(Color.Black));
        //    Assert.That(_sut.Pieces[2, 1], Is.EqualTo(Color.Black));
        //    Assert.That(_sut.Pieces[2, 2], Is.EqualTo(Color.Black));
        //}

        //[Test]
        //public void It_should_init_board_with_set_pieces()
        //{
        //    var result = new BoardService(_sut, Mock.Of<IBoardState>(), Mock.Of<IMoveService>());
        //    Assert.That(result.Pieces[2, 0], Is.EqualTo(Color.Black));
        //    Assert.That(result.Pieces[0, 1], Is.EqualTo(Color.White));
        //}

        [Test]
        public void It_should_move_piece_forward_direction_based_if_white()
        {

            var result = _sut.MoveForward(0, 0, Color.White, _pieces);
            Assert.That(result, Is.EqualTo(true));
        }

        //[Test]
        //public void It_should_move_piece_forward_direction_based_if_black()
        //{
        //    var result = _sut.MoveForward(2, 0, Color.Black);
        //    Assert.That(result, Is.EqualTo(true));
        //    Assert.That(_sut.Pieces[2, 0], Is.EqualTo(Color.Empty));
        //    Assert.That(_sut.Pieces[1, 0], Is.EqualTo(Color.Black));
        //}

        //private static IEnumerable<TestCaseData> PieceTestData
        //{
        //    get
        //    {
        //        yield return new TestCaseData(Color.Black);
        //        yield return new TestCaseData(Color.White);
        //    }
        //}

        //[TestCaseSource(nameof(PieceTestData))]
        //public void It_should_not_move_piece_forward_if_blocked(Color piece)
        //{
        //    _sut.Pieces[1, 0] = piece;
        //    var result = _sut.MoveForward(0, 0, Color.White);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_sut.Pieces[0, 0], Is.EqualTo(Color.White));
        //    Assert.That(_sut.Pieces[1, 0], Is.EqualTo(piece));
        //}

        //[Test]
        //public void It_should_not_move_piece_forward_if_blocked()
        //{
        //    _sut.Pieces[1, 0] = Color.Black;
        //    var result = _sut.MoveForward(0, 0, Color.White);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_sut.Pieces[0, 0], Is.EqualTo(Color.White));
        //    Assert.That(_sut.Pieces[1, 0], Is.EqualTo(Color.Black));
        //}

        //[Test]
        //public void It_should_not_move_piece_forward_if_not_players_piece()
        //{
        //    _sut.Pieces[1, 0] = Color.Black;
        //    var result = _sut.MoveForward(0, 0, Color.Black);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_sut.Pieces[0, 0], Is.EqualTo(Color.White));
        //    Assert.That(_sut.Pieces[1, 0], Is.EqualTo(Color.Black));
        //}

        //[Test]
        //public void It_should_not_attack_left_if_left_is_empty()
        //{
        //    _sut.Pieces[1, 1] = Color.Empty;
        //    var result = _sut.AttackLeft(0, 2, Color.White);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_sut.Pieces[0, 2], Is.EqualTo(Color.White));
        //    Assert.That(_sut.Pieces[1, 1], Is.EqualTo(Color.Empty));
        //}

        //[Test]
        //public void It_should_not_attack_left_if_left_is_same_player()
        //{
        //    _sut.Pieces[1, 1] = Color.White;
        //    var result = _sut.AttackLeft(0, 2, Color.White);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_sut.Pieces[0, 2], Is.EqualTo(Color.White));
        //    Assert.That(_sut.Pieces[1, 1], Is.EqualTo(Color.White));
        //}

        //[Test]
        //public void It_should_not_attack_left_if_out_of_board()
        //{
        //    var result = _sut.AttackLeft(0, 0, Color.White);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_sut.Pieces[0, 0], Is.EqualTo(Color.White));
        //}

        //[Test]
        //public void It_should_attack_left_if_left_is_opposite()
        //{
        //    _sut.Pieces[1, 1] = Color.Black;
        //    var result = _sut.AttackLeft(0, 2, Color.White);
        //    Assert.That(result, Is.EqualTo(true));
        //    Assert.That(_sut.Pieces[0, 2], Is.EqualTo(Color.Empty));
        //    Assert.That(_sut.Pieces[1, 1], Is.EqualTo(Color.White));
        //}

        //[Test]
        //public void It_should_not_attack_right_if_right_is_empty()
        //{
        //    _sut.Pieces[1, 1] = Color.Empty;
        //    var result = _sut.AttackRight(0, 0, Color.White);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_sut.Pieces[0, 2], Is.EqualTo(Color.White));
        //    Assert.That(_sut.Pieces[1, 1], Is.EqualTo(Color.Empty));
        //}

        //[Test]
        //public void It_should_not_attack_right_if_right_is_same_player()
        //{
        //    _sut.Pieces[1, 1] = Color.White;
        //    var result = _sut.AttackRight(0, 0, Color.White);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_sut.Pieces[0, 2], Is.EqualTo(Color.White));
        //    Assert.That(_sut.Pieces[1, 1], Is.EqualTo(Color.White));
        //}

        //[Test]
        //public void It_should_not_attack_right_if_out_of_board()
        //{
        //    var result = _sut.AttackRight(0, 2, Color.White);
        //    Assert.That(result, Is.EqualTo(false));
        //    Assert.That(_sut.Pieces[0, 2], Is.EqualTo(Color.White));
        //}

        //[TestCase(1,0)]
        //[TestCase(2,1)]
        //public void It_should_attack_right_if_right_is_opposite(int blockX, int attackX)
        //{
        //    _sut.Pieces[1, blockX] = Color.Black;
        //    var result = _sut.AttackRight(0, attackX, Color.White);
        //    Assert.That(result, Is.EqualTo(true));
        //    Assert.That(_sut.Pieces[0, attackX], Is.EqualTo(Color.Empty));
        //    Assert.That(_sut.Pieces[1, blockX], Is.EqualTo(Color.White));
        //}

        //[Test]
        //public void It_should_return_list_of_AvailableActions()
        //{
        //    var result = _sut.GetAllPlayerAvailableActions(Color.White);
        //    Assert.That(result, Is.TypeOf<List<AvailableActions>>());
        //}

        //[Test]
        //public void It_should_return_list_of_AvailableActions_with_values()
        //{
        //    var result = _sut.GetAllPlayerAvailableActions(Color.White);
        //    Assert.That(result.First().FromX, Is.EqualTo(0));
        //    Assert.That(result.First().FromY, Is.EqualTo(0));
        //    Assert.That(result.First().ToX, Is.EqualTo(0));
        //    Assert.That(result.First().ToY, Is.EqualTo(1));
        //    Assert.That(result.First().Action, Is.EqualTo(Actions.Forward));
        //}
        
    }
}
