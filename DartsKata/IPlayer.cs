using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartsKata
{
    public interface IPlayer
    {
        IScorecard CurrentScorecard { get; }

        bool HasWon { get; }

        void StartNewGame(IScorecard scorecard);

        void PlayTurn();
    }
}
