﻿using HexaPawnConsole;
using NUnit.Framework;
using System.Collections.Generic;

namespace HexaPawnTests
{
    public class MoveServiceTests
    {
        private MoveService _sut;
        private IBoardService _boardService;

        [SetUp]
        public void Setup()
        {
            _sut = new MoveService();

            _boardService = new BoardService(new BoardState(), _sut);
            _boardService.InitPieces();
            _boardService.InitPlayers(true, false);
        }

        [Test]
        public void It_should_move_piece_forward_direction_based_if_white()
        {

            var result = _sut.MoveForward(2, 0, Color.White, _boardService.Pieces);
            Assert.That(result, Is.EqualTo(true));
            Assert.That(_boardService.Pieces[1, 0], Is.EqualTo(Color.White));
            Assert.That(_boardService.Pieces[2, 0], Is.EqualTo(Color.Empty));
        }

        [Test]
        public void It_should_move_piece_forward_direction_based_if_black()
        {
            var result = _sut.MoveForward(0, 0, Color.Black, _boardService.Pieces);

            Assert.That(result, Is.EqualTo(true));
            Assert.That(_boardService.Pieces[1, 0], Is.EqualTo(Color.Black));
            Assert.That(_boardService.Pieces[0, 0], Is.EqualTo(Color.Empty));
        }

        private static IEnumerable<TestCaseData> PieceTestData
        {
            get
            {
                yield return new TestCaseData(Color.Black);
                yield return new TestCaseData(Color.White);
            }
        }

        [TestCaseSource(nameof(PieceTestData))]
        public void It_should_not_move_piece_forward_if_blocked(Color piece)
        {
            _boardService.Pieces[1, 0] = piece;
            var result = _sut.MoveForward(0, 0, Color.White, _boardService.Pieces);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_boardService.Pieces[2, 0], Is.EqualTo(Color.White));
            Assert.That(_boardService.Pieces[1, 0], Is.EqualTo(piece));
        }

        [Test]
        public void It_should_not_move_piece_forward_if_not_players_piece()
        {
            _boardService.Pieces[1, 0] = Color.Black;
            var result = _sut.MoveForward(2, 0, Color.Black, _boardService.Pieces);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_boardService.Pieces[2, 0], Is.EqualTo(Color.White));
            Assert.That(_boardService.Pieces[1, 0], Is.EqualTo(Color.Black));
        }

        [Test]
        public void It_should_not_attack_left_if_left_is_empty()
        {
            _boardService.Pieces[1, 1] = Color.Empty;
            var result = _sut.AttackLeft(2, 2, Color.White, _boardService.Pieces);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_boardService.Pieces[2, 2], Is.EqualTo(Color.White));
            Assert.That(_boardService.Pieces[1, 1], Is.EqualTo(Color.Empty));
        }

        [Test]
        public void It_should_not_attack_left_if_left_is_same_player()
        {
            _boardService.Pieces[1, 1] = Color.White;
            var result = _sut.AttackLeft(2, 2, Color.White, _boardService.Pieces);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_boardService.Pieces[2, 2], Is.EqualTo(Color.White));
            Assert.That(_boardService.Pieces[1, 1], Is.EqualTo(Color.White));
        }

        [Test]
        public void It_should_not_attack_left_if_out_of_board()
        {
            var result = _sut.AttackLeft(2, 0, Color.White, _boardService.Pieces);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_boardService.Pieces[2, 0], Is.EqualTo(Color.White));
        }

        [Test]
        public void It_should_attack_left_if_left_is_opposite()
        {
            _boardService.Pieces[1, 1] = Color.Black;
            var result = _sut.AttackLeft(2, 2, Color.White, _boardService.Pieces);
            Assert.That(result, Is.EqualTo(true));
            Assert.That(_boardService.Pieces[2, 2], Is.EqualTo(Color.Empty));
            Assert.That(_boardService.Pieces[1, 1], Is.EqualTo(Color.White));
        }

        [Test]
        public void It_should_not_attack_right_if_right_is_empty()
        {
            _boardService.Pieces[1, 1] = Color.Empty;
            var result = _sut.AttackRight(2, 0, Color.White, _boardService.Pieces);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_boardService.Pieces[2, 2], Is.EqualTo(Color.White));
            Assert.That(_boardService.Pieces[1, 1], Is.EqualTo(Color.Empty));
        }

        [Test]
        public void It_should_not_attack_right_if_right_is_same_player()
        {
            _boardService.Pieces[1, 1] = Color.White;
            var result = _sut.AttackRight(2, 0, Color.White, _boardService.Pieces);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_boardService.Pieces[2, 2], Is.EqualTo(Color.White));
            Assert.That(_boardService.Pieces[1, 1], Is.EqualTo(Color.White));
        }

        [Test]
        public void It_should_not_attack_right_if_out_of_board()
        {
            var result = _sut.AttackRight(2, 2, Color.White, _boardService.Pieces);
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_boardService.Pieces[2, 2], Is.EqualTo(Color.White));
        }

        [TestCase(1, 0)]
        [TestCase(2, 1)]
        public void It_should_attack_right_if_right_is_opposite(int blockX, int attackX)
        {
            _boardService.Pieces[1, blockX] = Color.Black;
            var result = _sut.AttackRight(2, attackX, Color.White, _boardService.Pieces);
            Assert.That(result, Is.EqualTo(true));
            Assert.That(_boardService.Pieces[2, attackX], Is.EqualTo(Color.Empty));
            Assert.That(_boardService.Pieces[1, blockX], Is.EqualTo(Color.White));
        }

    }
}