using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DartsKata.Test
{
    [TestClass]
    public class PlayerTest
    {
        private Mock<IScorecard> _scorecard;
        private Mock<IDartThrower> _dartThrower;
        private Mock<IDartThrowResult>[] _throwResult;
        private Player _player;

        [TestInitialize]
        public void TestInitialize()
        {
            _scorecard = new Mock<IScorecard>();
            _dartThrower = new Mock<IDartThrower>();
            _throwResult = new[] { new Mock<IDartThrowResult>(), new Mock<IDartThrowResult>(), new Mock<IDartThrowResult>() };
            _player = new Player(_dartThrower.Object);
        }

        [TestMethod]
        public void Player_CurrentScorecard_ReturnsTheScorecardOfCurrentGame()
        {
            _player.StartNewGame(_scorecard.Object);

            Assert.AreEqual(_scorecard.Object, _player.CurrentScorecard);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Player_CurrentScorecard_ThrowsExceptionIfNoGameHasBeenStartedYet()
        {
            var scorecard = _player.CurrentScorecard;
        }

        [TestMethod]
        public void Player_HasWon_WhenScorecardReachesZero()
        {
            _player.StartNewGame(_scorecard.Object);
            _scorecard.SetupGet(s => s.Score).Returns(0);
            
            Assert.IsTrue(_player.HasWon);
        }

        [TestMethod]
        public void Player_HasWon_WhenScorecardIsNotZero()
        {
            _player.StartNewGame(_scorecard.Object);
            _scorecard.SetupGet(s => s.Score).Returns(54);

            Assert.IsFalse(_player.HasWon);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Player_PlayTurn_ThrowsException_WhenPlayerScoreIsAlreadyZero()
        {
            _scorecard.SetupGet(s => s.Score).Returns(0);
            
            _player.StartNewGame(_scorecard.Object);
            _player.PlayTurn();
        }

        [TestMethod]
        public void Player_PlayTurn_ThrowsThreeDartsAndUpdatesScorecard()
        {
            _scorecard.SetupGet(s => s.Score).Returns(140);
            _dartThrower.SetupSequence(t => t.ThrowDart(It.IsAny<int>()))
                .Returns(_throwResult[0].Object)
                .Returns(_throwResult[1].Object)
                .Returns(_throwResult[2].Object);  

            _player.StartNewGame(_scorecard.Object);
            _player.PlayTurn();

            _scorecard.Verify(s => s.Add(_throwResult[0].Object, _throwResult[1].Object, _throwResult[2].Object));
        }

        [TestMethod]
        public void Player_PlayTurn_StopsThrowingDartsIfReachingLessThan2Points()
        {
            _scorecard.SetupGet(s => s.Score).Returns(20);
            _throwResult[0].SetupGet(r => r.TotalPoints).Returns(19);
            _dartThrower.SetupSequence(t => t.ThrowDart(It.IsAny<int>()))
                .Returns(_throwResult[0].Object)
                .Returns(_throwResult[1].Object)
                .Returns(_throwResult[2].Object);

            _player.StartNewGame(_scorecard.Object);
            _player.PlayTurn();

            _scorecard.Verify(s => s.Add(_throwResult[0].Object));
        }

        [TestMethod]
        public void Player_PlayTurn_AimsEachDartDependingOnTheScore()
        {
            _scorecard.SetupGet(s => s.Score).Returns(140);
            _dartThrower.Setup(t => t.ThrowDart(140)).Returns(_throwResult[0].Object);
            _throwResult[0].SetupGet(r => r.TotalPoints).Returns(10);
            _dartThrower.Setup(t => t.ThrowDart(130)).Returns(_throwResult[1].Object);
            _throwResult[1].SetupGet(r => r.TotalPoints).Returns(25);
            _dartThrower.Setup(t => t.ThrowDart(105)).Returns(_throwResult[2].Object);            
            

            _player.StartNewGame(_scorecard.Object);
            _player.PlayTurn();

            _scorecard.Verify(s => s.Add(_throwResult[0].Object, _throwResult[1].Object, _throwResult[2].Object));
        }
    }
}
