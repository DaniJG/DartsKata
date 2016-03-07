using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartsKata
{
    public class DartsGame
    {
        private GameType _gameType;
        private IPlayer[] _players;

        public DartsGame(GameType gameType, params IPlayer[] players)
        {
            if (players == null) throw new ArgumentNullException("players");
            if (players.Length < 2) throw new ArgumentOutOfRangeException("players");

            this._gameType = gameType;
            this._players = players;
            foreach (var player in this._players)
            {
                player.StartNewGame(new Scorecard((int)gameType));
            }
        }

        public bool Finished 
        { 
            get
            {
                return this._players.Any(p => p.HasWon);
            }
        }

        public void PlayTurn()
        {
            if (this.Finished) throw new InvalidOperationException("Cannot play another turn on a finished game");

            foreach(var player in this._players)
            {
                player.PlayTurn();
                if (player.HasWon) break;
            }
        }
    }
}
