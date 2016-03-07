using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartsKata
{
    public enum PointsModifier
    {
        Double,
        Triple
    };

    public class DartThrowResult: IDartThrowResult
    {
        private int _points;
        private bool _isDouble;
        private PointsModifier? _pointsModifier;

        public DartThrowResult(int points, PointsModifier? pointsModifier = null)
        {
            if (points == 0 && pointsModifier.HasValue) throw new ArgumentOutOfRangeException("pointsModifier", "No modified is allowed when scoring 0 points");
            if (points == 25 && pointsModifier.HasValue) throw new ArgumentOutOfRangeException("pointsModifier", "No modified is allowed with the outer bullseye");
            if (points == 50 && pointsModifier.HasValue) throw new ArgumentOutOfRangeException("pointsModifier", "No modified is allowed with the inner bullseye");
            if (points < 0) throw new ArgumentOutOfRangeException("points");
            if (points > 20 && points != 25 && points != 50) throw new ArgumentOutOfRangeException("points");

            this._points = points;
            this._pointsModifier = pointsModifier;
        }

        public bool IsDouble
        {
            get { return _pointsModifier == PointsModifier.Double; }
        }

        public bool IsInnerBullseye
        {
            get { return _points == 50; }
        }

        public int TotalPoints
        {
            get 
            {
                if (this._pointsModifier == PointsModifier.Double) return this._points * 2;
                if (this._pointsModifier == PointsModifier.Triple) return this._points * 3;
                return this._points;
            }
        }        
    }
}
