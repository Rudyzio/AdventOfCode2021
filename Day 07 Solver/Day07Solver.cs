using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_07_Solver
{
    public static class Day07Solver
    {
        public static long Part1Solution(string[] lines)
        {
            var positions = ParseInput(lines);
            // positions.Print();
            positions = positions.OrderBy(x => x).ToList();
            // positions.Print();

            var totalCrabs = positions.Count();
            double middle = totalCrabs / 2;
            long median = 0;

            if (IsEven(totalCrabs))
            {
                median = (positions[(int)Math.Ceiling(middle)] + positions[(int)Math.Round(middle)]) / 2;
            }
            else
            {
                median = positions[totalCrabs / 2];
            }
            // System.Console.WriteLine($"{median}");

            long toReturn = 0;

            foreach (var pos in positions)
            {
                toReturn += Math.Abs(median - pos);
            }

            return toReturn;
        }

        public static long Part2Solution(string[] lines)
        {
            var positions = ParseInput(lines);

            var totalCrabs = positions.Count();
            double averageCeiling = Math.Round(positions.Average());
            double averageFloor = Math.Floor(positions.Average());

            long toReturnCeiling = 0;
            long toReturnFloor = 0;

            foreach (var pos in positions)
            {
                toReturnCeiling += CalculateDigitSum((long)Math.Abs(pos - averageCeiling));
                toReturnFloor += CalculateDigitSum((long)Math.Abs(pos - averageFloor));
            }

            return toReturnCeiling < toReturnFloor ? toReturnCeiling : toReturnFloor;
        }

        private static List<long> ParseInput(string[] lines)
        {
            return lines[0].Split(",").Select<string, long>(x => int.Parse(x)).ToList();
        }

        private static bool IsEven(long number)
        {
            return number % 2 == 0;
        }

        private static void Print(this List<long> lines)
        {
            System.Console.WriteLine($"{string.Join(",", lines.ToArray())}");
        }

        private static long CalculateDigitSum(long n)
        {
            return n * (n + 1) / 2;
        }
    }
}
