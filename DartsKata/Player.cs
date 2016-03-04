using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartsKata
{
    public class Player: IPlayer
    {
        private IScorecard _scorecard;
        private IDartThrower _dartThrower;

        public Player(IDartThrower dartThrower)
        {
            this._dartThrower = dartThrower;
        }

        public bool HasWon
        {
            get 
            {
                return this._scorecard.Score == 0;
            }
        }

        public void StartNewGame(IScorecard scorecard)
        {
            this._scorecard = scorecard;
        }

        public void PlayTurn()
        {
            if (this.HasWon) throw new InvalidOperationException("Cannot play another turn when player has already cleared the score");

            var score = _scorecard.Score;
            var darts = Enumerable.Range(0, 3)
                .Select(x => 
                {
                    var dart = _dartThrower.ThrowDart(score);
                    score -= dart.TotalPoints;
                    return dart;
                })
                .ToArray();

            _scorecard.Add(darts);
        }
    }
}
