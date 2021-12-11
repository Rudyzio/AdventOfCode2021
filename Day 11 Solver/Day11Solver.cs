using System.Collections.Generic;
using System.Linq;

namespace Day_11_Solver
{
    public static class Day11Solver
    {
        public static int Part1Solution(string[] lines)
        {
            var flashes = 0;
            int[][] octopuses = ParseInput(lines);
            int steps = 0;
            while (steps < 100)
            {
                flashes += octopuses.Step();
                steps++;
            }

            return flashes;
        }

        public static int Part2Solution(string[] lines)
        {
            var flashes = 0;
            int[][] octopuses = ParseInput(lines);
            int steps = 0;
            while (true)
            {
                flashes += octopuses.Step();
                steps++;
                if (octopuses.All(x => x.All(y => y == 0)))
                {
                    break;
                }
            }
            return steps;
        }

        private static int[][] ParseInput(string[] lines)
        {
            var toReturn = new int[lines.Length][];

            for (var i = 0; i < lines.GetLength(0); i++)
            {
                toReturn[i] = new int[lines[0].Length];
                for (var j = 0; j < lines[0].Length; j++)
                {
                    toReturn[i][j] = int.Parse(lines[i][j].ToString());
                }
            }

            return toReturn;
        }

        private static void Print(this int[][] octopuses, int step)
        {
            System.Console.WriteLine($"After step {step}");
            for (var i = 0; i < octopuses.GetLength(0); i++)
            {
                System.Console.WriteLine(string.Join("", octopuses[i].Select(x => x.ToString())));
            }
            System.Console.WriteLine();
        }

        private static List<Point> GetAdjacent(Point point, int xLength, int yLength)
        {
            var toReturn = new List<Point>();

            // Above
            if (point.X > 0)
            {
                toReturn.Add(new Point(point.X - 1, point.Y));
            }

            // Left
            if (point.Y > 0)
            {
                toReturn.Add(new Point(point.X, point.Y - 1));
            }

            // Right
            if (point.Y + 1 < yLength)
            {
                toReturn.Add(new Point(point.X, point.Y + 1));
            }

            // Top
            if (point.X + 1 < xLength)
            {
                toReturn.Add(new Point(point.X + 1, point.Y));
            }

            // Top left
            if (point.X > 0 && point.Y > 0)
            {
                toReturn.Add(new Point(point.X - 1, point.Y - 1));
            }

            // Top right
            if (point.X > 0 && point.Y + 1 < yLength)
            {
                toReturn.Add(new Point(point.X - 1, point.Y + 1));
            }

            // Bottom left
            if (point.X + 1 < xLength && point.Y > 0)
            {
                toReturn.Add(new Point(point.X + 1, point.Y - 1));
            }

            // Bottom right 
            if (point.X + 1 < xLength && point.Y + 1 < yLength)
            {
                toReturn.Add(new Point(point.X + 1, point.Y + 1));
            }

            return toReturn;
        }

        private static int Step(this int[][] octopuses)
        {
            var flashes = 0;
            var points = new List<Point>();
            // octopuses.Print(steps);

            for (var i = 0; i < octopuses.GetLength(0); i++)
            {
                for (var j = 0; j < octopuses[i].Length; j++)
                {
                    if (octopuses[i][j] == 9)
                    {
                        points.Add(new Point(i, j));

                        octopuses[i][j] = 0;
                        flashes++;
                    }
                    else
                    {
                        octopuses[i][j]++;
                    }
                }
            }

            int pointsIndex = 0;
            while (pointsIndex < points.Count)
            {
                var currentPoint = points[pointsIndex];

                var adjacents = GetAdjacent(currentPoint, octopuses.Length, octopuses[0].Length);

                foreach (var adj in adjacents)
                {
                    if (octopuses[adj.X][adj.Y] == 9)
                    {
                        octopuses[adj.X][adj.Y] = 0;
                        flashes++;
                        if (!points.Any(x => x.X == adj.X && x.Y == adj.Y))
                            points.Add(new Point(adj.X, adj.Y));
                    }
                    else
                    {
                        if (!points.Any(x => x.X == adj.X && x.Y == adj.Y))
                            octopuses[adj.X][adj.Y]++;
                    }
                }
                pointsIndex++;
            }
            return flashes;
        }
    }

    public class Point
    {
        public Point(int i, int j)
        {
            X = i;
            Y = j;
        }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
