using HexaPawnConsole;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPawnTests
{
    public class ComputerTests
    {
        [Test]
        public void It()
        {
            var sut = new Computer();
            sut.hej();
        }
    }
}
