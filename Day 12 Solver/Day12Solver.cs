using System.Collections.Generic;
using System.Linq;

namespace Day_12_Solver
{
    public static class Day12Solver
    {
        public static int Part1Solution(string[] lines)
        {
            var paths = ParseInput(lines);
            var toReturn = 0;

            FindPathsAtLeastOnceSmallCaves(paths, "start", "start", ref toReturn);

            return toReturn;
        }

        public static int Part2Solution(string[] lines)
        {
            var paths = ParseInput(lines);
            var toReturn = 0;

            FindPathsAtLeastTwiceOneSmallCaves(paths, "start", "start", ref toReturn);

            return toReturn;
        }

        private static IDictionary<string, List<string>> ParseInput(string[] lines)
        {
            var toReturn = new Dictionary<string, List<string>>();

            foreach (var line in lines)
            {
                var startPoint = line.Split("-")[0];
                var endPoint = line.Split("-")[1];

                if (!toReturn.ContainsKey(startPoint))
                {
                    toReturn.Add(startPoint, new List<string>() { endPoint });
                }
                else
                {
                    toReturn[startPoint].Add(endPoint);
                }

                if (!toReturn.ContainsKey(endPoint))
                {
                    toReturn.Add(endPoint, new List<string>() { startPoint });
                }
                else
                {
                    toReturn[endPoint].Add(startPoint);
                }
            }

            return toReturn;
        }

        private static bool IsUppercase(this string input)
        {
            return input.All(x => char.IsUpper(x));
        }

        private static void FindPathsAtLeastOnceSmallCaves(IDictionary<string, List<string>> paths, string currentPath, string currentNode, ref int count)
        {
            if (currentNode.Equals("end"))
            {
                count++;
                return;
            }

            var children = paths[currentNode];

            foreach (var child in children)
            {
                if ((child.Equals("start")) || (currentPath.Contains(child) && !child.IsUppercase() && !child.Equals("end")))
                {
                    continue;
                }
                else
                {
                    FindPathsAtLeastOnceSmallCaves(paths, currentPath + child, child, ref count);
                }
            }
        }

        private static void FindPathsAtLeastTwiceOneSmallCaves(IDictionary<string, List<string>> paths, string currentPath, string currentNode, ref int count, bool visitedTwice = false)
        {
            if (currentNode.Equals("end"))
            {
                count++;
                return;
            }

            var children = paths[currentNode];
            var alreadyVisitedTwice = false;
            if (visitedTwice)
                alreadyVisitedTwice = true;
            foreach (var child in children)
            {
                if (
                    child.Equals("start") ||
                    (currentPath.Contains(child) && !child.IsUppercase() && !child.Equals("end") && alreadyVisitedTwice)
                )
                {
                    continue;
                }
                else
                {
                    if (!alreadyVisitedTwice)
                    {
                        if (currentPath.Contains(child) && !child.IsUppercase())
                        {
                            visitedTwice = true;
                        }
                        else
                        {
                            visitedTwice = false;
                        }
                    }

                    FindPathsAtLeastTwiceOneSmallCaves(paths, currentPath + "," + child, child, ref count, visitedTwice);
                }
            }
        }

        private static void Print(this IDictionary<string, List<string>> input)
        {
            foreach (var entry in input)
            {
                System.Console.WriteLine($"{entry.Key} --> {string.Join(",", entry.Value)}");
            }
        }
    }
}
