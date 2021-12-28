using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_19_Solver
{
    public static class Day19Solver
    {
        public static int Part1Solution(string[] lines)
        {
            var scanners = ParseInput(lines);
            (var centers, var mainMap) = CalibrateScanners(scanners);
            return mainMap.Positions.Count();
        }

        public static int Part2Solution(string[] lines)
        {
            var scanners = ParseInput(lines);

            (var centers, var mainMap) = CalibrateScanners(scanners);


            var toReturn = 0;

            for (var i = 0; i < centers.Count(); i++)
            {
                for (var j = 0; j < centers.Count(); j++)
                {
                    if (i != j)
                    {
                        var distance = Scanner.ManhattanDistance(centers[i], centers[j]);
                        if (toReturn < distance)
                        {
                            toReturn = distance;
                        }
                    }
                }
            }

            return toReturn;


        }

        private static (List<Position> centers, Scanner mainMap) CalibrateScanners(List<Scanner> scanners)
        {
            var centers = new List<Position>();

            var mainMap = scanners.First();
            centers.Add(mainMap.Center);
            var queue = new Queue<Scanner>();
            for (var i = 1; i < scanners.Count(); i++)
            {
                queue.Enqueue(scanners[i]);
            }

            while (queue.Any())
            {
                var scanner = queue.Dequeue();

                (var newCenter, var newPositions) = Locate(mainMap, scanner);

                if (newCenter == null)
                {
                    queue.Enqueue(scanner);
                }
                else
                {
                    centers.Add(newCenter);
                    mainMap.AddPositions(newPositions);
                }
            }

            return (centers, mainMap);
        }

        private static (Position newCenter, List<Position> positionsToAdd) Locate(Scanner scannerA, Scanner scannerB)
        {
            var beacons = GetOverlappingBeacons(scannerA, scannerB).ToList();

            if (beacons.Count() >= 12)
            {
                var beaconA = beacons[0];

                for (var i = 0; i < 24; i++)
                {
                    var rotateFunc = ScannerRotations[i];
                    var beaconInBRotated = rotateFunc(beaconA.posB);
                    var newCenter = new Position(beaconA.posA.X - beaconInBRotated.X, beaconA.posA.Y - beaconInBRotated.Y, beaconA.posA.Z - beaconInBRotated.Z);
                    var newPositions = scannerB.Positions.Select(rotateFunc).Select(x => new Position(x.X + newCenter.X, x.Y + newCenter.Y, x.Z + newCenter.Z));

                    if (newPositions.Intersect(scannerA.Positions, new PositionEqualityComparer()).Count() >= 12)
                    {
                        return (newCenter, newPositions.Except(scannerA.Positions, new PositionEqualityComparer()).ToList());
                    }
                }
            }

            return (null, null);
        }

        private static IEnumerable<(Position posA, Position posB)> GetOverlappingBeacons(Scanner scannerA, Scanner scannerB)
        {
            scannerA.CalculateDistances();
            scannerB.CalculateDistances();
            foreach (var distanceA in scannerA.Distances)
            {
                foreach (var distanceB in scannerB.Distances)
                {
                    if (distanceA.Value.Intersect(distanceB.Value).Count() >= 10)
                    {
                        yield return (distanceA.Key, distanceB.Key);
                    }
                }
            }
        }

        private static readonly Dictionary<int, Func<Position, Position>> ScannerRotations = new()
        {
            { 0, p => new Position(p.X, p.Y, p.Z) },
            { 1, p => new Position(p.X, p.Z, -p.Y) },
            { 2, p => new Position(p.X, -p.Y, -p.Z) },
            { 3, p => new Position(p.X, -p.Z, p.Y) },

            { 4, p => new Position(-p.X, -p.Y, p.Z) },
            { 5, p => new Position(-p.X, p.Z, p.Y) },
            { 6, p => new Position(-p.X, p.Y, -p.Z) },
            { 7, p => new Position(-p.X, -p.Z, -p.Y) },

            { 8, p => new Position(p.Y, -p.X, p.Z) },
            { 9, p => new Position(p.Y, p.Z, p.X) },
            { 10, p => new Position(p.Y, p.X, -p.Z) },
            { 11, p => new Position(p.Y, -p.Z, -p.X) },

            { 12, p => new Position(-p.Y, p.X, p.Z) },
            { 13, p => new Position(-p.Y, p.Z, -p.X) },
            { 14, p => new Position(-p.Y, -p.Z, p.X) },
            { 15, p => new Position(-p.Y, -p.X, -p.Z) },

            { 16, p => new Position(p.Z, p.Y, -p.X) },
            { 17, p => new Position(p.Z, -p.X, -p.Y) },
            { 18, p => new Position(p.Z, -p.Y, p.X) },
            { 19, p => new Position(p.Z, p.X, p.Y) },

            { 20, p => new Position(-p.Z, p.Y, p.X) },
            { 21, p => new Position(-p.Z, p.X, -p.Y) },
            { 22, p => new Position(-p.Z, -p.Y, -p.X) },
            { 23, p => new Position(-p.Z, -p.X, p.Y) },
        };

        private static List<Scanner> ParseInput(string[] lines)
        {
            var toReturn = new List<Scanner>();

            Scanner scanner = null;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.Substring(0, 3) == "---")
                {
                    if (scanner != null)
                    {
                        toReturn.Add(scanner);
                    }
                    scanner = new();
                    continue;
                }

                var splitted = line.Split(",");
                var position = new Position(int.Parse(splitted[0]), int.Parse(splitted[1]), int.Parse(splitted[2]));
                scanner.Positions.Add(position);
            }

            toReturn.Add(scanner);
            return toReturn;
        }
    }

    public class Scanner
    {
        public Scanner()
        {
            Positions = new List<Position>();
            Center = new Position(0, 0, 0);
        }

        public void CalculateDistances()
        {
            Distances = new Dictionary<Position, List<int>>();

            for (var i = 0; i < Positions.Count(); i++)
            {
                var currentPosition = Positions[i];

                for (var j = 0; j < Positions.Count(); j++)
                {
                    if (i != j)
                    {
                        var targetPosition = Positions[j];
                        var distance = ManhattanDistance(currentPosition, targetPosition);
                        if (!Distances.ContainsKey(currentPosition))
                        {
                            Distances.Add(currentPosition, new List<int>());
                        }
                        Distances[currentPosition].Add(distance);
                    }
                }
            }
        }

        public void AddPositions(List<Position> newPositions)
        {
            // Optimize here
            Positions.AddRange(newPositions);
            CalculateDistances();
        }

        public Position Center { get; set; }

        public List<Position> Positions { get; set; }

        public Dictionary<Position, List<int>> Distances { get; set; }

        public void Print()
        {
            foreach (var pos in Positions)
            {
                System.Console.WriteLine($"  -- position {pos.X},{pos.Y},{pos.Z}");
            }
        }

        public static int ManhattanDistance(Position current, Position target)
        {
            return Math.Abs(target.X - current.X) + Math.Abs(target.Y - current.Y) + Math.Abs(target.Z - current.Z);
        }
    }

    public class Position
    {
        public Position(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public void Print()
        {
            System.Console.WriteLine($"{X},{Y},{Z}");
        }

        public string GetPrint()
        {
            return $"{X},{Y},{Z}";
        }
    }

    public class PositionEqualityComparer : IEqualityComparer<Position>
    {

        #region IEqualityComparer<Position> Members


        public bool Equals(Position x, Position y)
        {
            //no null check here, you might want to do that, or correct that to compare just one part of your object
            return x.X == y.X && x.Y == y.Y && x.Z == y.Z;
        }


        public int GetHashCode(Position obj)
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + obj.X.GetHashCode();
                hash = hash * 23 + obj.Y.GetHashCode();
                hash = hash * 23 + obj.Z.GetHashCode();

                return hash;
            }
        }

        #endregion
    }
}
