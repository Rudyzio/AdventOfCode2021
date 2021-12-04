using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day_03_Solver
{
    public static class Day03Solver
    {
        public static int Part1Solution(string[] lines)
        {
            (string gammaRate, string epsilonRate) = GetRates(lines);

            return Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2);
        }

        public static int Part2Solution(string[] lines)
        {
            var lineLength = lines[0].Length;

            var oxygenRating = new List<string>(lines);
            var co2Rating = new List<string>(lines);
            var index = 0;

            while (true)
            {
                if (oxygenRating.Count == 1 && co2Rating.Count == 1)
                {
                    break;
                }

                if (oxygenRating.Count > 1)
                {
                    var mostCommonOxygen = GetMostCommonCount(oxygenRating, index);
                    oxygenRating = GetWithOccurrenceAtIndex(oxygenRating, index, mostCommonOxygen);
                }

                if (co2Rating.Count > 1)
                {
                    var leastCommonCo2 = GetLeastCommonCount(co2Rating, index);
                    co2Rating = GetWithOccurrenceAtIndex(co2Rating, index, leastCommonCo2);
                }

                index++;
            }

            return Convert.ToInt32(oxygenRating.First(), 2) * Convert.ToInt32(co2Rating.First(), 2);
        }

        private static (string gammaRate, string epsilonRate) GetRates(string[] lines)
        {
            int totalLines = lines.Length;
            int[] gammaRateCounter = new int[lines[0].Length];
            StringBuilder gammaRate = new();
            StringBuilder epsilonRate = new();

            foreach (var line in lines)
            {
                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i] == '1')
                    {
                        gammaRateCounter[i]++;
                    }
                }
            }

            for (var i = 0; i < gammaRateCounter.Length; i++)
            {
                if (gammaRateCounter[i] > totalLines / 2)
                {
                    gammaRate.Append('1');
                    epsilonRate.Append('0');
                }
                else
                {
                    gammaRate.Append('0');
                    epsilonRate.Append('1');
                }
            }

            return (gammaRate.ToString(), epsilonRate.ToString());
        }

        private static char GetMostCommonCount(List<string> lines, int index)
        {
            double totalLines = lines.Count;
            double countObjective = 0;

            foreach (var line in lines)
            {
                if (line[index] == '1')
                {
                    countObjective++;
                }
            }

            if (countObjective >= totalLines / 2)
            {
                return '1';
            }
            else
            {
                return '0';
            }
        }

        private static char GetLeastCommonCount(List<string> lines, int index)
        {
            var commonCount = GetMostCommonCount(lines, index);
            if (commonCount == '1')
            {
                return '0';
            }
            else
            {
                return '1';
            }
        }

        private static List<string> GetWithOccurrenceAtIndex(List<string> lines, int index, char bit)
        {
            var toReturn = new List<string>();

            foreach (var line in lines)
            {
                if (line[index] == bit)
                {
                    toReturn.Add(line);
                }
            }

            return toReturn;
        }

        private static void Print(this List<string> lines)
        {
            foreach (var line in lines)
            {
                System.Console.WriteLine(line);
            }
            System.Console.WriteLine("------------------------------");
        }
    }
}
