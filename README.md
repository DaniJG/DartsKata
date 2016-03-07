## About the darts game kata

The goal of this kata is to write a small application for the 301/501 darts games.

These are games for at least 2 playes with the following rules:
- Each player starts with a score of 301 or 501
- On each turn, each player throws 3 darts. At the end of the turn, the score of each dart is added up and taken off the score.
- If after throwing a dart the remaining score would be equal to or less than 1, then they cant throw more darts on that turn and the score will remain as in the beginning of the turn.
- Players have to reach zero, either with a double or bullseye

The specific rules for scoring are:
- Board has points 1-20
- Doubles and triples can be scored on the 1-20 points
- Outer bullseye is 25 points
- Inner bullseye is 50 points
- Anything else is 0 points

## About this repo

This repo contains a sample solution to the problem described above, which is just one of the many solutions that can be found for this problem. If you repeat this kata a couple times, most likely you will come up with different solutions.

It is written in C# and the commits history can be seen as a **partial history** of the individual steps and refactorings followed in this particular solution. (Some commits contain a few individual changes and TDD cycles)

In case you are curious the kata roughly followed the steps below:

1. Used a very simple test as a starting point. Just something simple to get up and running, getting the initial objects in place. The tests actually verified that a new game is not finished, and was used to justify the creation of the `DartsGame` class
2. Started to introduce the concept of players. Used tests like _a game needs at least 2 players_ and _a game is not finished while no player has cleared his score_ to further drive the `DartsGame` class and to introduce a `IPlayer` interface.
3. Introduced the concept of a _scorecard_. The test _a game is not finished while no player has cleared his score_ was changed into a few tests like _a game is finished when a player has won_ and _a gmae is not finished while no player has won_, which were used to drive the changes to the `IPlayer` interface and the addition on the `Scorecard`.
4. Introduced the concept of a _turn_. Tests were added to make sure each player will play his turn and a turn will be finished if a player has won. This helped to drive further the `DartsGame` and `IPlayer`.
5. Then attention was switched to the `IPlayer` and tests were used to start implementing it. Trying to implement its `PlayTurn` method lead to the creation of `IDartThrower` and `IDartThrowResult`.
6. Tests were used to implement `IDartThrowResult`
7. Tests were used to finish implementing `IScorecard`
8. A manual test harness was added, as a small console application
