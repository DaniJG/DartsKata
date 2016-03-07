using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartsKata
{
    class Program
    {
        static void Main(string[] args)
        {
            var dartThrower = new RandomThrower();
            var player1 = new ReportingPlayer(new Player(dartThrower), "A");
            var player2 = new ReportingPlayer(new Player(dartThrower), "B");
            var game = new DartsGame(GameType.The301, player1, player2);

            var currentTurn = 0;
            while (!game.Finished)
            {
                Console.WriteLine("Playing turn {0}", ++currentTurn);
                game.PlayTurn();
            }

            Console.ReadLine();
        }

        private class ReportingPlayer: IPlayer
        {
            public ReportingPlayer(IPlayer player, string identifier)
            {
                this.RealPlayer = player;
                this.Identifier = identifier;
            }

            public IPlayer RealPlayer { get; set; }
            public string Identifier { get; set; }

            public IScorecard CurrentScorecard
            {
                get { return this.RealPlayer.CurrentScorecard; }
            }

            public bool HasWon
            {
                get { return this.RealPlayer.HasWon; }
            }

            public void StartNewGame(IScorecard scorecard)
            {
                this.RealPlayer.StartNewGame(new ReportingScorecard(scorecard));
            }

            public void PlayTurn()
            {
                Console.WriteLine(String.Format("Player-{0}. Remaining points:{1}", this.Identifier, this.RealPlayer.CurrentScorecard.Score));    
                this.RealPlayer.PlayTurn(); ;
            }
        }

        private class ReportingScorecard: IScorecard
        {
            public ReportingScorecard(IScorecard scorecard)
            {
                this.RealScorecard = scorecard;
            }

            public IScorecard RealScorecard { get; set; }

            public int Score
            {
                get { return this.RealScorecard.Score; }
            }

            public void Add(params IDartThrowResult[] dartsThrown)
            {
                Console.WriteLine("Darts Thrown:" + String.Join(",", dartsThrown.Select(d => d.TotalPoints)));
                this.RealScorecard.Add(dartsThrown);
            }
        }

        private class RandomThrower: IDartThrower
        {
            private Random randomGenerator;

            public RandomThrower()
            {
                randomGenerator = new Random();
            }

            public IDartThrowResult ThrowDart(int targetScore)
            {
                var upperTarget = targetScore > 20 ? 20 : targetScore;
                var points = randomGenerator.Next(0, upperTarget + 1);
                if (points == upperTarget + 1)
                {
                    var bullseyeProbability = randomGenerator.NextDouble();
                    if (bullseyeProbability >= 0.8) return new DartThrowResult(50);
                    if (bullseyeProbability > 0.6) return new DartThrowResult(25);
                    return new DartThrowResult(0);
                } 
                else if(points > 0)
                {
                    var modifierProbability = randomGenerator.NextDouble();
                    var modifier = modifierProbability > 0.95 ?
                        (PointsModifier?)PointsModifier.Triple :
                        modifierProbability > 0.85 ? (PointsModifier?)PointsModifier.Double : null;
                    
                    return new DartThrowResult(points, modifier);
                }
                return new DartThrowResult(0);
            }
        }
    }
}
