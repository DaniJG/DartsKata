using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartsKata
{
    public class Scorecard : IScorecard
    {
        private int _score;

        public Scorecard(int score)
        {
            this._score = score;
        }

        public int Score
        {
            get { return this._score; }
        }


        public void Add(params IDartThrowResult[] dartsThrown)
        {
            var updatedScore = _score;
            foreach(var result in dartsThrown)
            {
                updatedScore -= result.TotalPoints;
            }

            if (updatedScore > 1)
            {
                _score = updatedScore;
            }
            else if (updatedScore == 0 && verifyValidLastThrow(dartsThrown))
            {
                _score = 0;
            }
        }

        private bool verifyValidLastThrow(IDartThrowResult[] dartsThrown)
        {
            var lastDart = dartsThrown.Last();
            return lastDart.IsInnerBullseye || lastDart.IsDouble;
        }
    }
}
