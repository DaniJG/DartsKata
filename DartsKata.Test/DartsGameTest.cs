using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartsKata.Test
{
    [TestClass]
    public class DartsGameTest
    {
        [TestMethod]
        public void DartsGameTest_ANewGameIsNotFinished()
        {
            var dartsGame = new DartsGame();

            Assert.IsFalse(dartsGame.Finished);
        }
    }
}
