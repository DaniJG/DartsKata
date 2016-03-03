using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DartsKata.Test
{
    [TestClass]
    public class DartsGameTest
    {
        private Mock<IPlayer> _player1;
        private Mock<IPlayer> _player2;
        private DartsGame _dartsGame;

        [TestInitialize]
        public void TestInitialize()
        {
            _player1 = new Mock<IPlayer>();
            _player1.SetupGet(p => p.Score).Returns(2);
            _player2 = new Mock<IPlayer>();
            _player2.SetupGet(p => p.Score).Returns(16);

            _dartsGame = new DartsGame(new IPlayer[] { _player1.Object, _player2.Object });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DartsGameTest_ANewGameNeedsPlayers()
        {
            var dartsGame = new DartsGame(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DartsGameTest_ANewGameNeedsAtLeast2Players()
        {            
            var dartsGame = new DartsGame(new IPlayer[] { _player1.Object });
        }

        [TestMethod]
        public void DartsGameTest_ANewGameIsNotFinished()
        {            
            Assert.IsFalse(_dartsGame.Finished);
        }

        [TestMethod]
        public void DartsGameTest_AGameIsNotFinished_WhileNoPlayerHasClearedHisScore()
        {
            Assert.IsFalse(_dartsGame.Finished);
        }
    }
}
