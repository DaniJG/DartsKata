using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartsKata
{
    public class DartsGame
    {
        private IPlayer[] _players;

        public DartsGame(IPlayer[] players)
        {
            if (players == null) throw new ArgumentNullException("players");
            if (players.Length < 2) throw new ArgumentOutOfRangeException("players");

            this._players = players;
        }

        public bool Finished { get; set; }
    }
}
