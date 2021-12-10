using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day_08_Solver
{
    public static class Day08Solver
    {

        /**
         0:      1:      2:      3:      4:
        aaaa    ....    aaaa    aaaa    ....
        b    c  .    c  .    c  .    c  b    c
        b    c  .    c  .    c  .    c  b    c
        ....    ....    dddd    dddd    dddd
        e    f  .    f  e    .  .    f  .    f
        e    f  .    f  e    .  .    f  .    f
        gggg    ....    gggg    gggg    ....

        5:      6:      7:      8:      9:
        aaaa    aaaa    aaaa    aaaa    aaaa
        b    .  b    .  .    c  b    c  b    c
        b    .  b    .  .    c  b    c  b    c
        dddd    dddd    ....    dddd    dddd
        .    f  e    f  .    f  e    f  .    f
        .    f  e    f  .    f  e    f  .    f
        gggg    gggg    ....    gggg    gggg
        **/

        public static int Part1Solution(string[] lines)
        {
            var toReturn = 0;
            var outputValues = GetOutputValues(lines);
            // outputValues.Print();
            foreach (var output in outputValues)
            {
                foreach (var splitted in output.Split(" "))
                {
                    if (Enum.IsDefined(typeof(UniqueSegmentLenghts), splitted.Length))
                    {
                        toReturn++;
                    }
                }
            }

            return toReturn;
        }

        public static int Part2Solution(string[] lines)
        {
            var signalPatterns = GetSignalPatterns(lines);
            var outputValues = GetOutputValues(lines);
            var toReturnValues = new List<int>();

            for (var i = 0; i < signalPatterns.Length; i++)
            {
                var decoded = DecodeSignalPatterns(signalPatterns[i]);
                StringBuilder numberToConvert = new StringBuilder();
                foreach (var splitted in outputValues[i].Split(" "))
                {
                    numberToConvert.Append(decoded.Single(x => x.Value.Equals(splitted.Sort())).Key);
                }
                toReturnValues.Add(int.Parse(numberToConvert.ToString()));
            }

            return toReturnValues.Sum();
        }

        private static string[] GetSignalPatterns(string[] lines)
        {
            var toReturn = new string[lines.Length];
            for (var i = 0; i < lines.Length; i++)
            {
                toReturn[i] = lines[i].Split(" | ")[0];
            }

            return toReturn;
        }

        private static string[] GetOutputValues(string[] lines)
        {
            var toReturn = new string[lines.Length];
            for (var i = 0; i < lines.Length; i++)
            {
                toReturn[i] = lines[i].Split(" | ")[1];
            }

            return toReturn;
        }

        private static void Print(this string[] lines)
        {
            foreach (var line in lines)
            {
                System.Console.WriteLine(line);
            }
        }

        private static Dictionary<int, string> GetSegmentDisplay()
        {
            var toReturn = new Dictionary<int, string>();

            toReturn.Add(1, "cf");

            toReturn.Add(7, "acf");

            toReturn.Add(4, "bcdf");

            toReturn.Add(2, "acdeg");
            toReturn.Add(3, "acdfg");
            toReturn.Add(5, "abdfg");

            toReturn.Add(0, "abcefg");
            toReturn.Add(6, "abdefg");
            toReturn.Add(9, "abcdfg");

            toReturn.Add(8, "abcdefg");

            return toReturn;
        }

        private static Dictionary<int, string> DecodeSignalPatterns(string signalPattern)
        {
            var segmentDisplay = GetSegmentDisplay();
            var splitted = signalPattern.Split(" ");
            var toReturn = new Dictionary<int, string>();

            toReturn.Add(1, splitted.Single(x => x.Length == 2).Sort());
            // System.Console.WriteLine($"- {splitted.Single(x => x.Length == 2)}: 1");
            toReturn.Add(7, splitted.Single(x => x.Length == 3).Sort());
            // System.Console.WriteLine($"- {splitted.Single(x => x.Length == 3)}: 7");
            toReturn.Add(4, splitted.Single(x => x.Length == 4).Sort());
            // System.Console.WriteLine($"- {splitted.Single(x => x.Length == 4)}: 4");
            toReturn.Add(8, splitted.Single(x => x.Length == 7).Sort());
            // System.Console.WriteLine($"- {splitted.Single(x => x.Length == 7)}: 8");

            splitted = splitted.Where(x => x.Length != 2 && x.Length != 3 && x.Length != 4 && x.Length != 7).ToArray();

            int index = 0;
            while (index < splitted.Length)
            {
                var current = splitted[index];
                /*
                    if numberSequence Length is 5
                    if the numberSequence contains all items from the 1 pattern, the numberSequence must be 3
                    if the numberSequence is not 3 and contains difference of 8 and 4, the numberSequence must be 2
                    else the numberSequence must be 5
                */

                if (current.Length == 5)
                {
                    // System.Console.WriteLine($"Does {currentNotSorted} contain {toReturn[1]}?");
                    if (current.Contains(toReturn[1].ToCharArray()))
                    {
                        // System.Console.WriteLine("YES!");
                        // System.Console.WriteLine($"- {current}: {3}");
                        toReturn.Add(3, current.Sort());
                    }
                    else
                    {
                        var eightMinusFourth = toReturn[8].Remove(toReturn[4].ToCharArray());
                        // System.Console.WriteLine("NO!");
                        // System.Console.WriteLine($"Does {currentNotSorted} contain {eightMinusFourth}?");
                        // System.Console.WriteLine($"Comparing {current} contain {eightMinusFourth}?");
                        if (current.Contains(eightMinusFourth.ToCharArray()))
                        {
                            // System.Console.WriteLine("YES!");
                            // System.Console.WriteLine($"- {current}: {2}");
                            toReturn.Add(2, current.Sort());
                        }
                        else
                        {
                            // System.Console.WriteLine("NO!");
                            // System.Console.WriteLine($"- {current}: {5}");
                            toReturn.Add(5, current.Sort());
                        }
                    }
                }

                /*
                    if numberSequence Length is 6 
                    if the numberSequence contains all items from the 4 pattern, the numberSequence must be 9 
                    if the numberSequence is not 9 and contains all items from the 7 pattern, the numberSequence must be 0 
                    else the numberSequence must be 6
                */

                if (current.Length == 6)
                {
                    if (current.Contains(toReturn[4].ToCharArray()))
                    {
                        // System.Console.WriteLine($"- {current}: {9}");
                        toReturn.Add(9, current.Sort());
                    }
                    else
                    {
                        if (current.Contains(toReturn[7].ToCharArray()))
                        {
                            // System.Console.WriteLine($"- {current}: {0}");
                            toReturn.Add(0, current.Sort());
                        }
                        else
                        {
                            // System.Console.WriteLine($"- {current}: {6}");
                            toReturn.Add(6, current.Sort());
                        }
                    }

                }
                index++;
            }

            return toReturn;
        }

        private static string Sort(this string input)
        {
            char[] first = input.ToCharArray();
            Array.Sort(first);
            return new string(first);
        }

        private static string Remove(this string input, char[] toRemove)
        {
            foreach (var c in toRemove)
            {
                input = input.Replace(c.ToString(), String.Empty);
            }
            return input.Trim();
        }

        private static bool Contains(this string input, char[] toCompare)
        {
            foreach (var c in toCompare)
            {
                if (!input.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public enum UniqueSegmentLenghts
    {
        ONE = 2,
        FOUR = 4,
        SEVEN = 3,
        EIGHT = 7
    }
}
