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
            throw new NotImplementedException();
        }
    }
}
