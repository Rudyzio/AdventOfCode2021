using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_05_Solver
{
    public static class Day05Solver
    {
        public static int Part1Solution(string[] lines)
        {
            var positions = new List<Position>();

            foreach (var line in lines)
            {
                var splitted = line.Split(" ");
                var firstCoordinate = splitted[0];
                var secondCoordinate = splitted[2];

                var firstCoordinateSplitted = firstCoordinate.Split(",");
                var firstCoordinateX = int.Parse(firstCoordinateSplitted[0]);
                var firstCoordinateY = int.Parse(firstCoordinateSplitted[1]);

                var secondCoordinateSplitted = secondCoordinate.Split(",");
                var secondCoordinateX = int.Parse(secondCoordinateSplitted[0]);
                var secondCoordinateY = int.Parse(secondCoordinateSplitted[1]);

                var addedPositions = new List<Position>();
                if (firstCoordinateX == secondCoordinateX || firstCoordinateY == secondCoordinateY)
                {
                    var xDiff = firstCoordinateX - secondCoordinateX;
                    var yDiff = firstCoordinateY - secondCoordinateY;

                    for (var x = 0; x < Math.Abs(xDiff) + 1; x++)
                    {
                        for (var y = 0; y < Math.Abs(yDiff) + 1; y++)
                        {
                            var xCoord = firstCoordinateX + ((xDiff < 0) ? x : x * -1);
                            var yCoord = firstCoordinateY + ((yDiff < 0) ? y : y * -1);
                            positions.AddPosition(xCoord, yCoord, addedPositions);
                        }
                    }
                }

            }

            return positions.Count(x => x.Overlaps > 1);
        }

        public static int Part2Solution(string[] lines)
        {
            var positions = new List<Position>();

            foreach (var line in lines)
            {
                var splitted = line.Split(" ");
                var firstCoordinate = splitted[0];
                var secondCoordinate = splitted[2];

                var firstCoordinateSplitted = firstCoordinate.Split(",");
                var firstCoordinateX = int.Parse(firstCoordinateSplitted[0]);
                var firstCoordinateY = int.Parse(firstCoordinateSplitted[1]);

                var secondCoordinateSplitted = secondCoordinate.Split(",");
                var secondCoordinateX = int.Parse(secondCoordinateSplitted[0]);
                var secondCoordinateY = int.Parse(secondCoordinateSplitted[1]);

                var addedPositions = new List<Position>();
                if (firstCoordinateX == secondCoordinateX || firstCoordinateY == secondCoordinateY)
                {
                    var xDiff = firstCoordinateX - secondCoordinateX;
                    var yDiff = firstCoordinateY - secondCoordinateY;

                    for (var x = 0; x < Math.Abs(xDiff) + 1; x++)
                    {
                        for (var y = 0; y < Math.Abs(yDiff) + 1; y++)
                        {
                            var xCoord = firstCoordinateX + ((xDiff < 0) ? x : x * -1);
                            var yCoord = firstCoordinateY + ((yDiff < 0) ? y : y * -1);
                            positions.AddPosition(xCoord, yCoord, addedPositions);
                        }
                    }
                }
                else
                {
                    var diff = firstCoordinateX - secondCoordinateX;
                    var xDir = firstCoordinateX - secondCoordinateX;
                    var yDir = firstCoordinateY - secondCoordinateY;
                    for (int xy = 0; xy < Math.Abs(diff) + 1; xy++)
                    {
                        var xCoord = firstCoordinateX + ((xDir < 0) ? xy : xy * -1);
                        var yCoord = firstCoordinateY + ((yDir < 0) ? xy : xy * -1);
                        positions.AddPosition(xCoord, yCoord, addedPositions);
                    }
                }

            }

            return positions.Count(x => x.Overlaps > 1);
        }

        private static void AddPosition(this List<Position> positions, int x, int y, List<Position> addedPositions)
        {
            if (addedPositions.Any(p => p.X == x && p.Y == y))
            {
                return;
            }
            else
            {
                addedPositions.Add(new Position(x, y));
            }

            // System.Console.WriteLine($"Added position {x}:{y}");
            if (!positions.Any(p => p.X == x && p.Y == y))
            {
                positions.Add(new Position(x, y));
            }
            else
            {
                positions.Single(p => p.X == x && p.Y == y).Overlaps++;
            }
        }
    }

    public class Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
            Overlaps = 1;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Overlaps { get; set; }
    }
}
