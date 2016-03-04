using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DartsKata.Test
{
    [TestClass]
    public class DartThrowResultTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DartThrowResult_CannotHaveADouble_WhenScoringZeroPoints()
        {
            var result = new DartThrowResult(0, PointsModifier.Double);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DartThrowResult_CannotHaveATriple_WhenScoringZeroPoints()
        {
            var result = new DartThrowResult(0, PointsModifier.Triple);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DartThrowResult_CannotHaveADouble_WhenScoringCenterPoints()
        {
            var result = new DartThrowResult(25, PointsModifier.Double);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DartThrowResult_CannotHaveATriple_WhenScoringCenterPoints()
        {
            var result = new DartThrowResult(25, PointsModifier.Triple);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DartThrowResult_CannotHaveADouble_WhenScoringBullseyePoints()
        {
            var result = new DartThrowResult(50, PointsModifier.Double);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DartThrowResult_CannotHaveATriple_WhenScoringBullseyePoints()
        {
            var result = new DartThrowResult(50, PointsModifier.Triple);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DartThrowResult_CannotHaveScoreSmallerThanZero()
        {
            var result = new DartThrowResult(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DartThrowResult_CannotHaveScoreBiggerThan20_ExceptCenterAndBullseye()
        {
            var centerResult = new DartThrowResult(25);
            var buulseyeResult = new DartThrowResult(50);
            Assert.IsNotNull(centerResult);
            Assert.IsNotNull(buulseyeResult);

            var result = new DartThrowResult(21);
        }

        [TestMethod]
        public void DartThrowResult_TotalPoints_ReturnsThePointsWithoutModifier()
        {
            var result = new DartThrowResult(17);

            Assert.AreEqual(17, result.TotalPoints);
        }

        [TestMethod]
        public void DartThrowResult_TotalPoints_ReturnsDoubleThePointsWithTheDoubleModifier()
        {
            var result = new DartThrowResult(15, PointsModifier.Double);

            Assert.AreEqual(30, result.TotalPoints);
        }

        [TestMethod]
        public void DartThrowResult_TotalPoints_ReturnsTripleThePointsWithTheTripleModifier()
        {
            var result = new DartThrowResult(20, PointsModifier.Triple);

            Assert.AreEqual(60, result.TotalPoints);
        }
    }
}
