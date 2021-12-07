using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_06_Solver
{
    public static class Day06Solver
    {
        public static int Part1Solution(string[] lines)
        {
            int currentDay = 1;
            int days = 80;

            var currentLanternfishes = ParseInput(lines);

            while (currentDay <= days)
            {
                var nextLanterfishes = new List<long>();
                var appendToEnd = new List<long>();
                foreach (var fish in currentLanternfishes)
                {
                    if (fish == 0)
                    {
                        appendToEnd.Add(8);
                        nextLanterfishes.Add(6);
                    }
                    else
                    {
                        nextLanterfishes.Add(fish - 1);
                    }
                }
                currentLanternfishes = nextLanterfishes.Concat(appendToEnd).ToList();
                // currentLanternfishes.Print(currentDay);
                currentDay++;
            }
            return currentLanternfishes.Count();
        }

        public static long Part2Solution(string[] lines)
        {
            int currentDay = 1;
            int days = 256;

            var currentLanternfishes = ParseInput(lines);
            long[] fishGeneration = new long[9];
            foreach (int i in currentLanternfishes)
            {
                fishGeneration[i]++;
            }

            while (currentDay <= days)
            {
                long newFishToday = fishGeneration[0];
                for (int i = 1; i < fishGeneration.Length; i++)
                {
                    fishGeneration[i - 1] = fishGeneration[i];
                }

                fishGeneration[8] = newFishToday;
                fishGeneration[6] += newFishToday;

                // fishGeneration.Print(currentDay);

                currentDay++;
            }

            return fishGeneration.Sum();
        }

        private static List<long> ParseInput(string[] lines)
        {
            return lines[0].Split(",").Select<string, long>(x => int.Parse(x)).ToList();
        }

        private static void Print(this long[] fishes, int days)
        {
            System.Console.WriteLine($"After {days} days: {string.Join(",", fishes)}");
        }

        private static void Print(this List<long> fishes, int days)
        {
            System.Console.WriteLine($"After {days} days: {string.Join(",", fishes.ToArray())}");
        }
    }
}
