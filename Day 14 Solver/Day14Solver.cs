using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day_14_Solver
{
    public static class Day14Solver
    {
        public static long Part1Solution(string[] lines)
        {
            (var polymerTemplate, var pairInsertionRules) = ParseInput(lines);

            return Steps(10, polymerTemplate, pairInsertionRules);
        }

        public static long Part2Solution(string[] lines)
        {
            (var polymerTemplate, var pairInsertionRules) = ParseInput(lines);

            return Steps(40, polymerTemplate, pairInsertionRules);
        }

        private static long Steps(int steps, string polymerTemplate, Dictionary<string, string> pairInsertionRules)
        {
            var dictionary = new Dictionary<string, long>();
            var charCount = new Dictionary<string, long>();

            for (var i = 0; i < polymerTemplate.Length; i++)
            {
                charCount.AddIfNotContains(polymerTemplate[i].ToString(), 1);

                if (i + 1 >= polymerTemplate.Length) continue;
                dictionary.AddIfNotContains(polymerTemplate.Substring(i, 2), 1);
            }

            int step = 0;
            while (step < steps)
            {
                var bufferDict = new Dictionary<string, long>(dictionary);

                foreach (var rule in pairInsertionRules)
                {
                    if (!dictionary.ContainsKey(rule.Key))
                    {
                        continue;
                    }

                    bufferDict[rule.Key] -= dictionary[rule.Key];
                    var firstPair = rule.Key[0] + rule.Value;
                    var secondPair = rule.Value + rule.Key[1];

                    bufferDict.AddIfNotContains(firstPair, dictionary[rule.Key]);
                    bufferDict.AddIfNotContains(secondPair, dictionary[rule.Key]);
                    charCount.AddIfNotContains(rule.Value, dictionary[rule.Key]);
                }
                dictionary = new Dictionary<string, long>(bufferDict);

                step++;
            }

            return charCount.Values.Max() - charCount.Values.Min();
        }

        // private static string Step(string polymerTemplate, Dictionary<string, string> pairs)
        // {
        //     StringBuilder toReturn = new StringBuilder();

        //     for (var i = 0; i < polymerTemplate.Length - 1; i++)
        //     {
        //         string pair = polymerTemplate[i].ToString() + polymerTemplate[i + 1].ToString();
        //         var adjacent = pairs[pair];

        //         if (string.IsNullOrEmpty(toReturn.ToString()))
        //         {
        //             toReturn.Append(polymerTemplate[i].ToString());
        //         }

        //         toReturn.Append(adjacent);
        //         toReturn.Append(polymerTemplate[i + 1].ToString());
        //     }

        //     return toReturn.ToString();
        // }

        private static (string, Dictionary<string, string>) ParseInput(string[] lines)
        {
            var polymerTemplate = string.Empty;
            var pairInsertionRules = new Dictionary<string, string>();

            bool insertionRules = false;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    insertionRules = true;
                    continue;
                }

                if (insertionRules)
                {
                    var pair = line.Split("->")[0].Trim();
                    var adjacent = line.Split("->")[1].Trim();
                    pairInsertionRules.Add(pair, adjacent);
                }
                else
                {
                    polymerTemplate = line;
                }
            }

            return (polymerTemplate, pairInsertionRules);
        }

        private static void AddIfNotContains(this Dictionary<string, long> dict, string key, long value)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, 0);
            }

            dict[key] += value;
        }
    }

    public class Pair
    {
        public string First { get; set; }
        public string Second { get; set; }
    }
}
