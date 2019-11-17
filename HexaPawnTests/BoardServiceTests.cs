//using HexaPawnConsole;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Linq;

//namespace HexaPawnTests
//{
//    public class BoardServiceTests
//    {
//        private BoardService _sut;

//        [SetUp]
//        public void Setup()
//        {
//            _sut = new BoardService(new BoardState(), new MovService());
//            _sut.InitPieces();
//            _sut.InitPlayers(true, true);

//        }

//        [Test]
//        public void It_should_generate_a_3x3_board()
//        {
//            var sut = new BoardService(null, null);
//            Assert.That(sut.Pieces[0, 0], Is.EqualTo((int)Color.Empty));
//            Assert.That(sut.Pieces[2, 2], Is.EqualTo((int)Color.Empty));
//        }

//        [Test]
//        public void It_should_init_pieces_white_and_black()
//        {
//            Assert.That(_sut.Pieces[2, 0], Is.EqualTo(1));
//            Assert.That(_sut.Pieces[2, 1], Is.EqualTo(1));
//            Assert.That(_sut.Pieces[2, 2], Is.EqualTo(1));
//            Assert.That(_sut.Pieces[0, 0], Is.EqualTo(2));
//            Assert.That(_sut.Pieces[0, 1], Is.EqualTo(2));
//            Assert.That(_sut.Pieces[0, 2], Is.EqualTo(2));
//        }

//        [Test]
//        public void It_should_init_players_as_human_type_if_true()
//        {
//            Assert.That(_sut.P1.GetType(), Is.EqualTo(typeof(Human)));
//            Assert.That(_sut.P2.GetType(), Is.EqualTo(typeof(Human)));
//        }

//        [Test]
//        public void It_should_init_players_as_AI_type_if_false()
//        {
//            _sut.InitPlayers(false, false);
//            Assert.That(_sut.P1.GetType(), Is.EqualTo(typeof(AI)));
//            Assert.That(_sut.P2.GetType(), Is.EqualTo(typeof(AI)));
//        }

//        [Test]
//        public void It_should_reset_board()
//        {
//            _sut.Pieces[1, 1] = 1;
//            _sut.ResetBoard();
//            Assert.That(_sut.Pieces[1, 1], Is.EqualTo(0));
//        }

//        [Test]
//        public void It_should_return_false_if_currentPlayer_is_P1_and_not_AI()
//        {
//            _sut.InitPlayers(true, true);
//            _sut.CurrentPlayer = _sut.P1;
//            var result = _sut.CheckIfCurrentPlayerIsAI();
//            Assert.That(result, Is.False);
//        }
//        [Test]
//        public void It_should_return_false_if_currentPlayer_is_P2_and_not_AI()
//        {
//            _sut.InitPlayers(true, true);
//            _sut.CurrentPlayer = _sut.P2;
//            var result = _sut.CheckIfCurrentPlayerIsAI();
//            Assert.That(result, Is.False);
//        }

//        [Test]
//        public void It_should_return_true_if_currentPlayer_is_P2_and_AI()
//        {
//            _sut.InitPlayers(false, false);

//            _sut.CurrentPlayer = _sut.P1;
//            var result = _sut.CheckIfCurrentPlayerIsAI();
//            Assert.That(result, Is.True);
//        }
//        [Test]
//        public void It_should_return_true_if_currentPlayer_is_P1_and_AI()
//        {
//            _sut.InitPlayers(false, false);

//            _sut.CurrentPlayer = _sut.P2;
//            var result = _sut.CheckIfCurrentPlayerIsAI();
//            Assert.That(result, Is.True);
//        }

//        [Test]
//        public void It_should_return_AvailableActions()
//        {
//            var result = _sut.GetAllPlayerAvailableActions1(Color.White);
//            Assert.That(result, Is.TypeOf<List<AvailableAction>>());
//        }

//        [Test]
//        public void It_should_return_AvailableActions_with_forward_value()
//        {
//            var result = _sut.GetAllPlayerAvailableActions1(Color.White);
//            Assert.That(result.First().Action, Is.EqualTo(Actions.Forward));
//        }

//        [Test]
//        public void It_should_return_AvailableActions_with_value()
//        {
//            _sut.Pieces[1, 1] = 2;
//            var result = _sut.GetAllPlayerAvailableActions1(Color.White);
//            Assert.That(result[1].Action, Is.EqualTo(Actions.AttackRight));
//            Assert.That(result[3].Action, Is.EqualTo(Actions.AttackLeft));
//        }

//    }
//}
