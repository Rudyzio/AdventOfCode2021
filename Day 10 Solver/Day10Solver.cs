using System.Collections.Generic;

namespace Day_10_Solver
{
    public static class Day10Solver
    {
        public static long Part1Solution(string[] lines)
        {
            List<char> illegals = new();
            foreach (var line in lines)
            {
                (var hasError, var stack) = CheckInput(line, illegals);
            }

            var toReturn = 0;
            foreach (var illegal in illegals)
            {
                switch (illegal)
                {
                    case ')':
                        toReturn += 3;
                        break;
                    case ']':
                        toReturn += 57;
                        break;
                    case '}':
                        toReturn += 1197;
                        break;
                    case '>':
                        toReturn += 25137;
                        break;
                }
            }

            return toReturn;
        }

        public static long Part2Solution(string[] lines)
        {
            var incompleteScores = new List<long>();

            foreach (var line in lines)
            {
                (var foundError, var stack) = CheckInput(line);

                if (!foundError)
                {
                    long score = 0;
                    while (stack.Count > 0)
                    {
                        var current = stack.Pop();
                        switch (current)
                        {
                            case ')':
                                score = score * 5 + 1;
                                break;
                            case ']':
                                score = score * 5 + 2;
                                break;
                            case '}':
                                score = score * 5 + 3;
                                break;
                            case '>':
                                score = score * 5 + 4;
                                break;
                        }
                    }

                    if (score > 0)
                        incompleteScores.Add(score);
                }
            }

            incompleteScores.Sort();
            return incompleteScores[incompleteScores.Count / 2];
        }

        private static (bool, Stack<char>) CheckInput(string line, List<char> illegals = null)
        {
            var stack = new Stack<char>();
            bool foundError = false;
            foreach (var chunk in line)
            {
                switch (chunk)
                {
                    case '(':
                        stack.Push(')');
                        break;
                    case '[':
                        stack.Push(']');
                        break;
                    case '{':
                        stack.Push('}');
                        break;
                    case '<':
                        stack.Push('>');
                        break;

                    case ')':
                    case ']':
                    case '}':
                    case '>':
                        var closing = stack.Pop();
                        if (closing != chunk)
                        {
                            foundError = true;
                            if (illegals != null)
                                illegals.Add(chunk);
                        }
                        break;
                }

                if (foundError)
                {
                    break;
                }
            }

            return (foundError, stack);
        }
    }
}
