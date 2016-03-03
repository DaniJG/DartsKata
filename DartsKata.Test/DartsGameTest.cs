using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DartsKata.Test
{
    [TestClass]
    public class DartsGameTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DartsGameTest_ANewGameNeedsPlayers()
        {
            //Act
            var dartsGame = new DartsGame(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DartsGameTest_ANewGameNeedsAtLeast2Players()
        {
            //Arrange
            var player1 = new Mock<IPlayer>();

            //Act
            var dartsGame = new DartsGame(new IPlayer[] { player1.Object });
        }

        [TestMethod]
        public void DartsGameTest_ANewGameIsNotFinished()
        {
            //Arrange
            var player1 = new Mock<IPlayer>();
            player1.SetupGet(p => p.Score).Returns(2);
            var player2 = new Mock<IPlayer>();
            player2.SetupGet(p => p.Score).Returns(16);

            //Arrange
            var dartsGame = new DartsGame(new IPlayer[] { player1.Object, player2.Object });

            //Act
            Assert.IsFalse(dartsGame.Finished);
        }

        [TestMethod]
        public void DartsGameTest_AGameIsNotFinished_WhileNoPlayerHasClearedHisScore()
        {
            //Arrange
            var player1 = new Mock<IPlayer>();
            player1.SetupGet(p => p.Score).Returns(2);
            var player2 = new Mock<IPlayer>();
            player2.SetupGet(p => p.Score).Returns(16);

            //Act
            var dartsGame = new DartsGame(new IPlayer[] { player1.Object, player2.Object });

            //Assert
            Assert.IsFalse(dartsGame.Finished);
        }
    }
}
