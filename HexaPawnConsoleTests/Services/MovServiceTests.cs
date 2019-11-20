using Microsoft.VisualStudio.TestTools.UnitTesting;
using HexaPawnConsole;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPawnConsole.Tests
{
    [TestClass()]
    public class MovServiceTests
    {
        [TestMethod()]
        public void CanMoveForwardTest()
        {
            var sut = new MoveService();

            var service = new BoardService(null, null);
            service.InitPieces();
            service.Pieces["B3"] = Color.White;
            var result = sut.CanAttackLeft("B3", Color.White, service.Pieces);
            
        }
    }
}