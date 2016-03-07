using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DartsKata.Test
{
    [TestClass]
    public class ScorecardTest
    {        
        private Mock<IDartThrowResult>[] _throwResult;       

        [TestInitialize]
        public void TestInitialize()
        {
            _throwResult = new[] { new Mock<IDartThrowResult>(), new Mock<IDartThrowResult>(), new Mock<IDartThrowResult>() };
        }

        [TestMethod]
        public void Scorecard_ScoreIsInitialized()
        {
            var scorecard = new Scorecard(15);

            Assert.AreEqual(15, scorecard.Score);
        }

        [TestMethod]
        public void Scorecard_Add_UpdatesScoreWithAllThePoints()
        {
            _throwResult[0].SetupGet(r => r.TotalPoints).Returns(50);
            _throwResult[1].SetupGet(r => r.TotalPoints).Returns(25);
            _throwResult[2].SetupGet(r => r.TotalPoints).Returns(15);

            var scorecard = new Scorecard(150);
            scorecard.Add(_throwResult[0].Object, _throwResult[1].Object, _throwResult[2].Object);

            Assert.AreEqual(60, scorecard.Score);
        }

        [TestMethod]
        public void Scorecard_Add_LeavesPreviousScoreWhenTotalPointsAreBigger()
        {
            _throwResult[0].SetupGet(r => r.TotalPoints).Returns(60);
            _throwResult[1].SetupGet(r => r.TotalPoints).Returns(60);
            _throwResult[2].SetupGet(r => r.TotalPoints).Returns(60);

            var scorecard = new Scorecard(179);
            scorecard.Add(_throwResult[0].Object, _throwResult[1].Object, _throwResult[2].Object);

            Assert.AreEqual(179, scorecard.Score);
        }

        [TestMethod]
        public void Scorecard_Add_LeavesPreviousScoreWhenScoreDecreasesToOne()
        {
            _throwResult[0].SetupGet(r => r.TotalPoints).Returns(60);
            _throwResult[1].SetupGet(r => r.TotalPoints).Returns(60);
            _throwResult[2].SetupGet(r => r.TotalPoints).Returns(60);

            var scorecard = new Scorecard(181);
            scorecard.Add(_throwResult[0].Object, _throwResult[1].Object, _throwResult[2].Object);

            Assert.AreEqual(181, scorecard.Score);
        }

        [TestMethod]
        public void Scorecard_Add_LeavesPreviousScoreWhenScoreDecreasesToZero_AndLastDartIsNotDoubleNorInnerBullsEye()
        {
            _throwResult[0].SetupGet(r => r.TotalPoints).Returns(60);
            _throwResult[1].SetupGet(r => r.TotalPoints).Returns(60);

            var scorecard = new Scorecard(120);
            scorecard.Add(_throwResult[0].Object, _throwResult[1].Object, _throwResult[2].Object);

            Assert.AreEqual(120, scorecard.Score);
        }

        [TestMethod]
        public void Scorecard_Add_SetsScoreAsZero_WhenNewScoreIsZero_AndLastDartIsDouble()
        {
            _throwResult[0].SetupGet(r => r.TotalPoints).Returns(20);            
            _throwResult[1].SetupGet(r => r.TotalPoints).Returns(20);
            _throwResult[2].SetupGet(r => r.TotalPoints).Returns(4);
            _throwResult[2].SetupGet(r => r.IsDouble).Returns(true);

            var scorecard = new Scorecard(44);
            scorecard.Add(_throwResult[0].Object, _throwResult[1].Object, _throwResult[2].Object);

            Assert.AreEqual(0, scorecard.Score);
        }

        [TestMethod]
        public void Scorecard_Add_SetsScoreAsZero_WhenNewScoreIsZero_AndLastDartIsInnerBullseye()
        {
            _throwResult[0].SetupGet(r => r.TotalPoints).Returns(20);            
            _throwResult[1].SetupGet(r => r.TotalPoints).Returns(20);
            _throwResult[2].SetupGet(r => r.TotalPoints).Returns(50);
            _throwResult[2].SetupGet(r => r.IsInnerBullseye).Returns(true);

            var scorecard = new Scorecard(90);
            scorecard.Add(_throwResult[0].Object, _throwResult[1].Object, _throwResult[2].Object);

            Assert.AreEqual(0, scorecard.Score);
        }
    }
}
