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
            var player1 = new Player(dartThrower);
            var player2 = new Player(dartThrower);
            var game = new DartsGame(GameType.The301, player1, player2);

            while (!game.Finished)
            {
                game.PlayTurn();
            }

            Console.ReadLine();
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
