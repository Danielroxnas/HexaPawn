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
            var sut = new MovService();

            var service = new BoardService(null, null);
            service.InitPieces();
            service.Pieces1["B3"] = 1;
            var result = sut.CanAttackLeft("B3", 1 , service.Pieces1);
            
        }
    }
}