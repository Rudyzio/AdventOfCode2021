using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day_13_Solver
{
    public static class Day13Solver
    {
        public static int Part1Solution(string[] lines)
        {
            (var points, var folds) = ParseInput(lines);

            points = Fold(points, folds.First());

            return points.Count();
        }

        public static int Part2Solution(string[] lines)
        {
            (var points, var folds) = ParseInput(lines);

            foreach (var fold in folds)
            {
                points = Fold(points, fold);
            }

            points.PrintGrid();
            return points.Count();
        }

        private static (List<Point>, List<Fold>) ParseInput(string[] lines)
        {
            var points = new List<Point>();
            var folds = new List<Fold>();

            bool readFolds = false;

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    readFolds = true;
                    continue;
                }

                if (readFolds)
                {
                    var coordinates = line.Split(" ")[2];
                    folds.Add(new Fold(coordinates.Split("=")[0], int.Parse(coordinates.Split("=")[1])));
                }
                else
                {
                    points.Add(new Point(int.Parse(line.Split(",")[0]), int.Parse(line.Split(",")[1])));
                }
            }

            return (points, folds);
        }

        private static List<Point> Fold(List<Point> points, Fold fold)
        {
            var pointsBiggerThan = new List<Point>();

            if (fold.Axis == "y")
            {
                pointsBiggerThan = points.Where(p => p.Y > fold.Value).ToList();
            }
            else
            {
                pointsBiggerThan = points.Where(p => p.X > fold.Value).ToList();
            }

            points = points.Except(pointsBiggerThan).ToList();

            foreach (var point in pointsBiggerThan)
            {
                Point pointToAdd;
                if (fold.Axis == "y")
                {
                    pointToAdd = new Point(point.X, point.Y - (point.Y - fold.Value) * 2);
                }
                else
                {
                    pointToAdd = new Point(point.X - (point.X - fold.Value) * 2, point.Y);
                }

                if (!points.Any(p => p.X == pointToAdd.X && p.Y == pointToAdd.Y))
                {
                    points.Add(pointToAdd);
                }
            }
            return points;
        }

        private static void Print(this List<Point> points)
        {
            foreach (var point in points)
            {
                System.Console.WriteLine($"{point.X},{point.Y}");
            }
        }

        private static void PrintGrid(this List<Point> points)
        {
            var maxX = points.Max(p => p.X);
            var maxY = points.Max(p => p.Y);

            for (var i = 0; i <= maxY; i++)
            {
                StringBuilder line = new StringBuilder();
                for (var j = 0; j <= maxX; j++)
                {
                    line.Append(points.Any(p => p.X == j && p.Y == i) ? "#".ToString() : ".".ToString());
                }
                System.Console.WriteLine(line.ToString());
            }
            System.Console.WriteLine();
        }
    }

    public class Point
    {
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;

        }
        public int X { get; set; }

        public int Y { get; set; }
    }

    public class Fold
    {
        public Fold(string axis, int value)
        {
            this.Value = value;
            this.Axis = axis;

        }
        public int Value { get; set; }
        public string Axis { get; set; }
    }
}
