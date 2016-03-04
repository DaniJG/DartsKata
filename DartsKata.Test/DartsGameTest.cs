using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Sequences;

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
            //_player1.SetupGet(p => p.Score).Returns(2);
            _player2 = new Mock<IPlayer>();
            //_player2.SetupGet(p => p.Score).Returns(16);

            //_dartsGame = new DartsGame(new IPlayer[] { _player1.Object, _player2.Object });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DartsGameTest_ANewGameNeedsPlayers()
        {
            var dartsGame = new DartsGame(GameType.The301, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DartsGameTest_ANewGameNeedsAtLeast2Players()
        {
            var dartsGame = new DartsGame(GameType.The301, new[] { _player1.Object });
        }        

        [TestMethod]
        public void DartsGameTest_ANewGame_InitializesPlayers()
        {
            var dartsGame = new DartsGame(GameType.The301, new[] { _player1.Object, _player2.Object });

            _player1.Verify(p => p.Initialize(It.IsAny<IScorecard>()));
            _player2.Verify(p => p.Initialize(It.IsAny<IScorecard>()));
        }

        [TestMethod]
        public void DartsGameTest_ANew501Game_InitializesPlayers_With501Score()
        {
            var dartsGame = new DartsGame(GameType.The501, new[] { _player1.Object, _player2.Object });

            _player1.Verify(p => p.Initialize(It.Is<IScorecard>(sc => sc.Score == 501)));
            _player2.Verify(p => p.Initialize(It.Is<IScorecard>(sc => sc.Score == 501)));
        }

        [TestMethod]
        public void DartsGameTest_ANew301Game_InitializesPlayers_With301Score()
        {
            var dartsGame = new DartsGame(GameType.The301, new[] { _player1.Object, _player2.Object });

            _player1.Verify(p => p.Initialize(It.Is<IScorecard>(sc => sc.Score == 301)));
            _player2.Verify(p => p.Initialize(It.Is<IScorecard>(sc => sc.Score == 301)));
        }

        [TestMethod]
        public void DartsGameTest_AGameIsNotFinished_WhileNoPlayerHasWon()
        {
            _player1.SetupGet(p => p.HasWon).Returns(false);
            _player2.SetupGet(p => p.HasWon).Returns(false);

            var dartsGame = new DartsGame(GameType.The301, new [] { _player1.Object, _player2.Object });

            Assert.IsFalse(dartsGame.Finished);
        }

        [TestMethod]
        public void DartsGameTest_AGameIsFinished_WhenAPlayerHasWon()
        {
            _player1.SetupGet(p => p.HasWon).Returns(false);
            _player2.SetupGet(p => p.HasWon).Returns(true);

            var dartsGame = new DartsGame(GameType.The301, new[] { _player1.Object, _player2.Object });

            Assert.IsTrue(dartsGame.Finished);
        }

        [TestMethod]
        public void DartsGameTest_PlayTurn_AsksPlayersInOrderToPlayHisTurn()
        {            
            var dartsGame = new DartsGame(GameType.The301, new[] { _player1.Object, _player2.Object });

            using (Sequence.Create())
            {
                _player1.Setup(p => p.PlayTurn()).InSequence();
                _player2.Setup(p => p.PlayTurn()).InSequence();

                dartsGame.PlayTurn();
            }            
        }

        [TestMethod]
        public void DartsGameTest_PlayTurn_EndsTurnWhenAPlayerWins()
        {
            var player3 = new Mock<IPlayer>();
            _player2.SetupSequence(p => p.HasWon)
                .Returns(false)
                .Returns(true);            
            var dartsGame = new DartsGame(GameType.The301, new[] { _player1.Object, _player2.Object, player3.Object });

            using (Sequence.Create())
            {
                _player1.Setup(p => p.PlayTurn()).InSequence();
                _player2.Setup(p => p.PlayTurn()).InSequence();

                dartsGame.PlayTurn();

                player3.Verify(p => p.PlayTurn(), Times.Never);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DartsGameTest_PlayTurn_ThrowsExceptionWhenTheGameIsAlreadyFinished()
        {
            _player2.SetupGet(p => p.HasWon).Returns(true);
            var dartsGame = new DartsGame(GameType.The301, new[] { _player1.Object, _player2.Object });

            dartsGame.PlayTurn();
        }
    }
}
