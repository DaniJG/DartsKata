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

        public IScorecard CurrentScorecard
        {
            get 
            {
                if (this._scorecard == null) throw new InvalidOperationException("No game has been started yet.");
                return this._scorecard; 
            }
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
            var throwResults = new List<IDartThrowResult>();
            for(var x= 0; x<3; x++)
            {
                var result = _dartThrower.ThrowDart(score);
                throwResults.Add(result);
                score -= result.TotalPoints;

                if (score < 2) break;
            }
            _scorecard.Add(throwResults.ToArray());
        }        
    }
}
