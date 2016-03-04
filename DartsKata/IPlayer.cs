using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartsKata
{
    public interface IPlayer
    {
        bool HasWon { get; }

        void Initialize(IScorecard scorecard);

        void PlayTurn();
    }
}
