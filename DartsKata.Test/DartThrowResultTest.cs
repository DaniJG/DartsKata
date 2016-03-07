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
        public void DartThrowResult_CannotHaveADouble_WhenScoringOuterBullseye()
        {
            var result = new DartThrowResult(25, PointsModifier.Double);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DartThrowResult_CannotHaveATriple_WhenScoringOuterBullseye()
        {
            var result = new DartThrowResult(25, PointsModifier.Triple);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DartThrowResult_CannotHaveADouble_WhenScoringInnerBullseye()
        {
            var result = new DartThrowResult(50, PointsModifier.Double);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DartThrowResult_CannotHaveATriple_WhenScoringInnerBullseye()
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
        public void DartThrowResult_CannotHaveScoreBiggerThan20_ExceptBullseye()
        {
            var outerBullseyeResult = new DartThrowResult(25);
            var innerBullseyeResult = new DartThrowResult(50);
            Assert.IsNotNull(outerBullseyeResult);
            Assert.IsNotNull(innerBullseyeResult);

            var result = new DartThrowResult(21);
        }

        [TestMethod]
        public void DartThrowResult_IsDouble_ReturnsTrueWithTheDoubleModifier()
        {
            var nonDoubleResult1 = new DartThrowResult(20, PointsModifier.Triple);
            var nonDoubleResult2 = new DartThrowResult(20);
            var doubleResult = new DartThrowResult(10, PointsModifier.Double);

            Assert.IsFalse(nonDoubleResult1.IsDouble);
            Assert.IsFalse(nonDoubleResult2.IsDouble);
            Assert.IsTrue(doubleResult.IsDouble);
        }

        [TestMethod]
        public void DartThrowResult_IsInnerBullseye_ReturnsTrueWith50Points()
        {
            var outerBullseyeResult = new DartThrowResult(25);
            var nonBullseyeResult = new DartThrowResult(20, PointsModifier.Triple);
            var innerBullseyeResult = new DartThrowResult(50);

            Assert.IsFalse(outerBullseyeResult.IsInnerBullseye);
            Assert.IsFalse(nonBullseyeResult.IsInnerBullseye);
            Assert.IsTrue(innerBullseyeResult.IsInnerBullseye);
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
