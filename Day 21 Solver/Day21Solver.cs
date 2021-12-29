using System.Collections.Generic;
using System.Linq;

namespace Day_21_Solver
{
    public static class Day21Solver
    {
        public static int Part1Solution(string[] lines)
        {
            (var player1Pos, var player2Pos) = ParseInput(lines);

            var player1Score = 0;
            var player2Score = 0;

            var dice = new Dice();

            while (true)
            {
                var p1Roll = dice.Roll();
                (player1Pos, player1Score) = MakePlay(p1Roll, player1Pos, player1Score);
                if (player1Score >= 1000)
                    break;

                var p2Roll = dice.Roll();
                (player2Pos, player2Score) = MakePlay(p2Roll, player2Pos, player2Score);
                if (player2Score >= 1000)
                    break;
            }

            var losingPlayerScore = player1Score > player2Score ? player2Score : player1Score;

            return losingPlayerScore * dice.TimesRolled;
        }

        public static long Part2Solution(string[] lines)
        {
            (var player1Pos, var player2Pos) = ParseInput(lines);

            (var player1Wins, var player2Wins) = MakeRecursivePlay(player1Pos, player2Pos, 0, 0);

            return player1Wins > player2Wins ? player1Wins : player2Wins;
        }

        private static (int playerPos, int playerScore) MakePlay(int roll, int playerPos, int playerScore)
        {
            System.Console.WriteLine("Play");
            playerPos += roll;
            while (playerPos > 10)
            {
                playerPos -= 10;
            }

            playerScore += playerPos;

            return (playerPos, playerScore);
        }

        private static (long, long) MakeRecursivePlay(int player1Pos, int player2Pos, int player1Score, int player2Score, Dictionary<State, (long, long)> states = null)
        {
            var currentState = new State(player1Pos, player2Pos, player1Score, player2Score);

            if (states == null)
            {
                states = new Dictionary<State, (long, long)>();
            }

            if (player1Score >= 21)
            {
                return (1, 0);
            }

            if (player2Score >= 21)
            {
                return (0, 1);
            }

            if (states.Any(x => x.Key.Equals(currentState)))
            {
                return states[currentState];
            }

            var toReturn = (0L, 0L);
            for (var i = 1; i < 4; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    for (var k = 1; k < 4; k++)
                    {
                        var newPlayer1Pos = (player1Pos + i + j + k);
                        while (newPlayer1Pos > 10)
                        {
                            newPlayer1Pos -= 10;
                        }
                        var newPlayer1Score = player1Score + newPlayer1Pos;

                        (var p1, var p2) = MakeRecursivePlay(player2Pos, newPlayer1Pos, player2Score, newPlayer1Score, states);
                        toReturn = (toReturn.Item1 + p2, toReturn.Item2 + p1);
                    }
                }
            }
            states.Add(currentState, toReturn);

            return toReturn;
        }

        private static (int, int) ParseInput(string[] lines)
        {
            return (int.Parse(lines[0].Split(": ")[1]), int.Parse(lines[1].Split(": ")[1]));
        }
    }

    public class Dice
    {
        public Dice()
        {
            TimesRolled = 0;
            CurrentValue = 1;
        }

        public int TimesRolled { get; set; }

        private int CurrentValue { get; set; }

        public int Roll()
        {
            var toReturn = 0;
            for (var i = 0; i < 3; i++)
            {
                toReturn += CurrentValue;
                CurrentValue++;
                if (CurrentValue == 100)
                {
                    CurrentValue = 0;
                }
                TimesRolled++;
            }
            return toReturn;
        }
    }

    public class State
    {
        public State(int player1Position, int player2Position, int player1Score, int player2Score)
        {
            Player1Position = player1Position;
            Player2Position = player2Position;
            Player1Score = player1Score;
            Player2Score = player2Score;
        }

        public int Player1Position { get; set; }

        public int Player2Position { get; set; }

        public int Player1Score { get; set; }

        public int Player2Score { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                State p = (State)obj;
                return (Player1Position == p.Player1Position) &&
                       (Player2Position == p.Player2Position) &&
                       (Player1Score == p.Player1Score) &&
                       (Player2Score == p.Player2Score);
            }
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 23 + Player1Position.GetHashCode();
            hash = hash * 23 + Player2Position.GetHashCode();
            hash = hash * 23 + Player1Score.GetHashCode();
            hash = hash * 23 + Player2Score.GetHashCode();

            return hash;
        }
    }
}
