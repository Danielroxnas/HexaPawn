using Microsoft.VisualStudio.TestTools.UnitTesting;
using HexaPawnConsole1;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPawnConsole1.Tests
{
    [TestClass()]
    public class GameServiceTests
    {
        [TestMethod()]
        public void GameServiceTest()
        {
            var sut = new GameService(null);
            sut.InitBoard();
            Assert.Fail();
        }
    }
}