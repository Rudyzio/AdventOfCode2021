using System;
using System.Collections.Generic;

namespace Day_01_Solver
{
    public static class Day01Solver
    {
        public static int Part1Solution(string[] lines)
        {
            int[] entries = Array.ConvertAll(lines, int.Parse);

            return CalculateIncreases(entries);
        }

        public static int Part2Solution(string[] lines)
        {
            int[] entries = Array.ConvertAll(lines, int.Parse);

            var newEntries = new List<int>();

            for (var i = 0; i < entries.Length; i++)
            {
                if (i + 1 >= entries.Length || i + 2 >= entries.Length)
                {
                    break;
                }

                newEntries.Add(entries[i] + entries[i + 1] + entries[i + 2]);
            }

            return CalculateIncreases(newEntries.ToArray());
        }

        private static int CalculateIncreases(int[] entries)
        {
            int previous = 0;
            int increases = -1;
            for (var i = 0; i < entries.Length; i++)
            {
                if (entries[i] > previous)
                {
                    increases++;
                }
                previous = entries[i];
            }

            return increases;
        }
    }
}
